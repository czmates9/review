/*
FASK - samostatne schema bez databazi Pohody
STAV: STATICKY ZKONTROLOVANY NAVRH, ZALOZENI NA SQL SERVERU ZATIM NEOTESTOVANO.
Zdroj: 20260612_zakladaciSkript.sql
SQL Server 2017+; nova databaze FASK; vychozi cesty instance.
Spustit cely soubor v SSMS nebo sqlcmd. Neni potreba SQLCMD rezim.
Existujici FASK lze pouzit jen bez uzivatelskych objektu a s opravnenim CONTROL.
Pokud obsahuje objekty, skript skonci pred upravami. Zadna DB se nemaze.
Pri chybe instalace se schema vrati transakci; nova prazdna FASK muze zustat.
Bez pocatecnich dat. Zachovane parametricke/dynamicke integracni procedury
mohou pri pouziti stale vyzadovat Pohodu nebo dalsi konfiguraci.
18 objektu zavislych na chybejicich databazich se NEZAKLADA.
Jejich puvodni kod je archivovan na konci jako radkove komentare.
Aktivni objekty: {"FUNCTION": 14, "TABLE": 146, "VIEW": 19, "PROCEDURE": 85, "TRIGGER": 26}
Neaktivni objekty:
FUNCTION dbo.fask_func_planovani_VydejFilterToSI
FUNCTION dbo.FASK_Get_CompareToIS_FromFASK
FUNCTION dbo.FASK_Get_CompareToIS_FromIS
FUNCTION dbo.FASK_Get_POHODA_LokMechMapingID
FUNCTION dbo.FASK_Get_PohybyFromPOHODA
FUNCTION dbo.FASK_GetDavkyByCarKody
FUNCTION dbo.FASK_GetDodavatelByCarKody
FUNCTION dbo.FASK_GetGroupFromSI
FUNCTION dbo.FASK_GetListPolozekPoSluzbe
PROCEDURE dbo.FASK_PrijemGetSkladExpedice
PROCEDURE dbo.FASK_proc_EXPORT_POHODA_FASK_ZASOBY_MaR
PROCEDURE dbo.FASK_proc_EXPORT_SQL_FASK_VazbaMat
PROCEDURE dbo.FASK_proc_EXPORT_SQL_FASK_VazbaMatPln
PROCEDURE dbo.FASK_proc_Insert_VyrobaTP
PROCEDURE dbo.FASK_procGetAdresa
PROCEDURE dbo.FASK_procGetAdresa_SQL
PROCEDURE dbo.FASK_procGetpolozka
PROCEDURE dbo.FASK_procPlnVyrobaTP
*/
USE [master];
SET NOCOUNT ON;
IF @@TRANCOUNT <> 0
    THROW 51000, N'Run outside an existing transaction.', 1;
IF TRY_CONVERT(int, SERVERPROPERTY('ProductMajorVersion')) < 14
    THROW 51000, N'SQL Server 2017 or later is required.', 1;
IF DB_ID(N'FASK') IS NULL
    EXEC(N'CREATE DATABASE [FASK]');
ELSE
BEGIN
    -- Permit retry only after a rollback left no user objects.
    EXEC(N'USE [FASK];
        IF HAS_PERMS_BY_NAME(DB_NAME(), ''DATABASE'', ''CONTROL'') <> 1
            THROW 51001, N''CONTROL permission required to verify an existing FASK.'', 1;
        IF EXISTS (SELECT 1 FROM sys.objects WHERE is_ms_shipped = 0)
            THROW 51001, N''FASK contains user objects. No changes were made.'', 1;');
END;
EXEC(N'ALTER DATABASE [FASK] SET COMPATIBILITY_LEVEL = 140');
EXEC(N'ALTER DATABASE [FASK] SET RECOVERY SIMPLE');
EXEC sys.sp_executesql N'USE [FASK];
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

-- Installation step 1
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS](
	[GUID_PLANOVANI] [uniqueidentifier] NOT NULL,
	[ColumnName] [nvarchar](100) NOT NULL,
	[Value] [bit] NOT NULL
) ON [PRIMARY]'';

-- Installation step 2
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS_Name](
	[Column_Name] [nvarchar](100) NOT NULL,
	[DESC] [nvarchar](200) NOT NULL,
	[dateedit] [datetime] NOT NULL
) ON [PRIMARY]'';

-- Installation step 3
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SI](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ODBER_ID] [nvarchar](12) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USER_ID] [int] NOT NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 4
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I1](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[ITEMDESC] [nvarchar](100) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[DMJ] [nvarchar](200) NULL,
	[DATEDONE] [datetime] NOT NULL,
	[IntegerValue] [smallint] NOT NULL,
	[TIMESPRT] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Find] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TerminalID] [tinyint] NOT NULL,
	[O_TID] [tinyint] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL
) ON [PRIMARY]'';

-- Installation step 5
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I2](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 6
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[DMJ] [nvarchar](200) NOT NULL,
	[TAXRATE] [numeric](4, 2) NULL,
	[PRICE0] [numeric](18, 2) NULL,
	[PRICE1] [numeric](18, 2) NULL,
	[PRICE2] [numeric](18, 2) NULL,
	[PRICE3] [numeric](18, 2) NULL,
	[PRICE4] [numeric](18, 2) NULL,
	[PRICE5] [numeric](18, 2) NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_Rez1_Track] [tinyint] NOT NULL,
	[CZ_Rez2_Track] [tinyint] NOT NULL,
	[CZ_Rez3_Track] [tinyint] NOT NULL,
	[CZ_Rez4_Track] [tinyint] NOT NULL,
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
	[REZ3] [nvarchar](50) NULL,
	[REZ4] [nvarchar](50) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[TIMEFROM] [datetime] NULL,
	[TIMETO] [datetime] NULL,
	[LSTMod] [datetime] NULL,
	[loginid] [nvarchar](20) NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL,
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 7
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I3](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[VENDORID] [nvarchar](15) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[VENDNAME] [nvarchar](31) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 8
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST093](
	[skl_id] [nvarchar](20) NOT NULL,
	[skl_desc] [nvarchar](40) NULL,
	[skl_typ] [nvarchar](3) NULL,
	[skl_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 9
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I4](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[QUANTITYMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[O_Checked] [bit] NOT NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 10
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SkladLokace_Stav](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD_DEF] [numeric](19, 5) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DATECHANGE] [datetime] NOT NULL,
	[EXPIRATION] [datetime] NULL,
	[QTYSHPPD_DEF_DATE] [datetime] NULL,
	[QTY_OWNER] [numeric](19, 5) NOT NULL,
	[PRAC_ID_OWNER] [nvarchar](30) NULL
) ON [PRIMARY]'';

-- Installation step 11
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SkladLokace_StavPohyb](
	[id] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[POHYB_TYPE] [nvarchar](2) NULL,
	[POHYB_SRC] [nvarchar](2) NULL,
	[SOURCE] [nvarchar](2) NULL,
	[CountEntries] [int] NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID_SRC] [nvarchar](20) NULL,
	[SKL_ID_DST] [nvarchar](20) NULL,
	[LOCNCODE_SRC] [nvarchar](11) NULL,
	[LOCNCODE_DST] [nvarchar](11) NULL,
	[UserID] [int] NOT NULL,
	[TermID] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[dateeveS] [datetime] NOT NULL,
	[dateeveT] [datetime] NOT NULL,
 CONSTRAINT [PK_CZMST_SkladLokace_StavPohyb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 12
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PI](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ORD] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[USER_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[Expirace] [datetime] NULL,
	[AttributeToSN] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 13
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PE](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL
) ON [PRIMARY]'';

-- Installation step 14
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_DI](
	[CountEntries] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[STR_ID] [nvarchar](30) NULL,
	[DOC_ID] [nvarchar](12) NULL,
	[DOC_ID2] [nvarchar](12) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[TAXAMPIE] [numeric](19, 5) NULL,
	[AMOUNPIE] [numeric](19, 5) NULL,
	[WITHTAX] [tinyint] NULL,
	[PRICEX] [tinyint] NULL,
	[mena_ID] [nvarchar](10) NULL,
	[TAXAMPIEM] [numeric](19, 5) NULL,
	[AMOUNPIEM] [numeric](19, 5) NULL,
	[mena_IDM] [nvarchar](10) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[LOCNCODEDEST] [nvarchar](11) NULL,
	[SKL_ID_DEST] [nvarchar](20) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[EXPIRACE] [datetime] NULL,
	[AttributeToSN] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 15
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SE](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[Note] [nvarchar](100) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[QTYPAL] [numeric](19, 5) NULL,
	[PRIORITY] [tinyint] NOT NULL,
	[PRINTED] [tinyint] NULL,
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL
) ON [PRIMARY]'';

-- Installation step 16
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_DI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[STR_ID] [nvarchar](30) NULL,
	[DOC_ID] [nvarchar](12) NULL,
	[DOC_ID2] [nvarchar](12) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[TAXAMPIE] [numeric](19, 5) NULL,
	[AMOUNPIE] [numeric](19, 5) NULL,
	[WITHTAX] [tinyint] NULL,
	[PRICEX] [tinyint] NULL,
	[mena_ID] [nvarchar](10) NULL,
	[TAXAMPIEM] [numeric](19, 5) NULL,
	[AMOUNPIEM] [numeric](19, 5) NULL,
	[mena_IDM] [nvarchar](10) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[LOCNCODEDEST] [nvarchar](11) NULL,
	[SKL_ID_DEST] [nvarchar](20) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[EXPIRACE] [datetime] NULL,
	[AttributeToSN] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 17
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SE_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[Note] [nvarchar](100) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[QTYPAL] [numeric](19, 5) NULL,
	[PRIORITY] [tinyint] NOT NULL,
	[PRINTED] [tinyint] NULL,
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL
) ON [PRIMARY]'';

-- Installation step 18
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ODBER_ID] [nvarchar](12) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USER_ID] [int] NOT NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 19
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PE_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL
) ON [PRIMARY]'';

-- Installation step 20
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ORD] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[USER_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[Expirace] [datetime] NULL,
	[AttributeToSN] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 21
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Logins](
	[USERID] [nvarchar](20) NOT NULL,
	[firstname] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[psswd] [nvarchar](50) NOT NULL,
	[CREATED] [datetime] NULL,
	[VALIDFROM] [datetime] NULL,
	[VALIDTO] [datetime] NULL,
	[RFID] [nvarchar](20) NULL,
 CONSTRAINT [PK_FASK_Logins] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 22
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZPRO_VPH](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL,
	[SOPDESC] [nvarchar](100) NULL,
	[VNDDOCNMH] [nvarchar](21) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DateProd] [smallint] NOT NULL,
	[Rez1] [nvarchar](50) NOT NULL,
	[Rez2] [nvarchar](50) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NOT NULL,
	[USERID] [int] NULL,
 CONSTRAINT [PK_CZPRO_VPH] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 23
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZPRO_VPP](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMMJ] [nvarchar](5) NULL,
	[VNDDOCNMP] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[BarcodeP] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYDOKON] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[TIMEMODE] [int] NOT NULL,
	[TIMEPREP] [real] NOT NULL,
	[TIMEUNIT] [real] NOT NULL,
	[DtProdT] [tinyint] NOT NULL,
	[DtProdL] [smallint] NOT NULL,
	[SerNumT] [tinyint] NOT NULL,
	[SerNumL] [smallint] NOT NULL,
	[VerT] [tinyint] NOT NULL,
	[VerL] [smallint] NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[BarcodeT] [tinyint] NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[CZ_REZ3_Track] [tinyint] NOT NULL,
	[CZ_REZ4_Track] [tinyint] NOT NULL,
	[CZ_REZ5_Track] [tinyint] NOT NULL,
	[WEIGHT_TARA] [numeric](19, 5) NULL,
	[WEIGHT_NETTO] [numeric](19, 5) NULL,
	[WEIGHT_TOL_PLUS] [numeric](19, 5) NULL,
	[WEIGHT_TOL_MINUS] [numeric](19, 5) NULL,
 CONSTRAINT [PK_CZPRO_VPP] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[ITEMNMBR] ASC,
	[CountEntries] ASC,
	[BarcodeP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 24
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Production](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL,
	[ITEMMJ] [nvarchar](5) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NULL,
	[TIMEMODE] [int] NULL,
	[TIMEPREPSTART] [datetime] NULL,
	[TIMEPREPSTOP] [datetime] NULL,
	[TIMEPREP] [real] NULL,
	[TIMEUNIT] [real] NULL,
	[TIMESTART] [datetime] NULL,
	[TIMESTOP] [datetime] NULL,
	[TIMECORSTART] [datetime] NULL,
	[TIMECORSTOP] [datetime] NULL,
	[TIMECOR] [real] NULL,
	[TIMECRID] [int] NULL,
	[TIMECRIDTYPE] [tinyint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[operationid] [nvarchar](16) NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[description] [nvarchar](max) NULL,
	[BarcodeP] [nvarchar](31) NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[ISOK] [datetime] NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[SOUBEHGUID] [uniqueidentifier] NULL,
	[CORRGUID] [uniqueidentifier] NULL,
	[qtyOld] [numeric](19, 5) NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[EXPIRATION] [nvarchar](50) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PackType] [nvarchar](50) NULL,
	[status] [int] NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[STORNOGUID] [uniqueidentifier] NULL,
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL,
	[WEIGHT_OLD] [numeric](19, 5) NULL,
 CONSTRAINT [PK_FASK_Production] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 25
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Logins_Auth](
	[USERID] [nvarchar](20) NOT NULL,
	[AGENDAID] [nvarchar](50) NULL,
	[AUTH] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 26
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Corrects](
	[id] [int] NOT NULL,
	[desc] [nvarchar](100) NOT NULL,
	[TMFrom] [real] NULL,
	[TMTo] [real] NULL,
	[Production] [tinyint] NOT NULL,
	[ProductionType] [tinyint] NULL,
 CONSTRAINT [PK_correct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 27
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZ_UKOL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CreatorID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[State] [nvarchar](1) NOT NULL,
	[Kind] [nvarchar](1) NULL,
	[Type] [nvarchar](2) NULL,
	[Priority] [int] NOT NULL,
	[PartnerID] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_UKOL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 28
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZ_UKOL_ServiceMan](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[DESC] [nvarchar](100) NULL,
	[OPERATOR] [nvarchar](50) NULL,
	[OPERATOR_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZ_UKOL_ServiceMan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 29
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZ_UKOL_STATE](
	[State] [nvarchar](1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[IsStart] [bit] NOT NULL,
	[IsEnd] [bit] NOT NULL,
	[Color] [nvarchar](7) NULL,
 CONSTRAINT [PK_CZ_UKOL_STATE] PRIMARY KEY CLUSTERED 
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 30
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZ_UKOL_UZIV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 31
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZ_UKOL_UZIV_HIST](
	[ID_HIST] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV_HIST] PRIMARY KEY CLUSTERED 
(
	[ID_HIST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 32
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_CountEntries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TBL] [nvarchar](20) NOT NULL,
	[CountEntries] [int] NULL,
	[BLOCKED] [bit] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[TBL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 33
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_DI_RFID](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 34
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_DIH](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 35
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_DIH_HISTORY](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 36
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_EventsTypes](
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[edesc] [nvarchar](100) NULL,
	[ebarcode] [nvarchar](21) NULL
) ON [PRIMARY]'';

-- Installation step 37
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_EventsUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eguid] [uniqueidentifier] NOT NULL,
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[etime] [datetime] NOT NULL,
	[termid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[modul] [nvarchar](20) NULL,
	[countentries] [int] NULL,
	[docnmbr] [nvarchar](30) NULL,
	[itemnmbr] [nvarchar](40) NULL,
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[eguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 38
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Buffer](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]'';

-- Installation step 39
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka](
	[ID] [uniqueidentifier] NOT NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 40
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]'';

-- Installation step 41
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Polozky](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 42
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 43
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Hlavicka](
	[ID] [uniqueidentifier] NOT NULL,
	[PrepravceID] [nvarchar](20) NULL,
	[PrepravceSPZ] [nvarchar](20) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 44
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Hlavicka_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[PrepravceID] [nvarchar](20) NULL,
	[PrepravceSPZ] [nvarchar](20) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]'';

-- Installation step 45
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Polozky](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDHB] [uniqueidentifier] NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 46
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Expedice_Polozky_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDHB] [uniqueidentifier] NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 47
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I1H](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[State] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 48
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_I1P](
	[Countentries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[TAX] [numeric](19, 5) NULL,
	[PRICE0] [numeric](19, 5) NULL,
	[PRICE1] [numeric](19, 5) NULL,
	[PRICE2] [numeric](19, 5) NULL,
	[PRICE3] [numeric](19, 5) NULL,
	[PRICE4] [numeric](19, 5) NULL,
	[PRICE5] [numeric](19, 5) NULL,
	[PRICE0H] [nvarchar](50) NULL,
	[PRICE1H] [nvarchar](50) NULL,
	[PRICE2H] [nvarchar](50) NULL,
	[PRICE3H] [nvarchar](50) NULL,
	[PRICE4H] [nvarchar](50) NULL,
	[PRICE5H] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 49
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_METAINFO](
	[DB_Version] [nvarchar](20) NOT NULL,
	[MST_Version] [nvarchar](20) NOT NULL,
	[IS_Provider] [nvarchar](50) NOT NULL,
	[Updated] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 50
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Palety](
	[ID] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 51
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PE_SN](
	[CountEntries] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 52
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 53
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PEH](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[DOKLTYPE] [nvarchar](10) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]'';

-- Installation step 54
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PEH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[DOKLTYPE] [nvarchar](10) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]'';

-- Installation step 55
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PI_F](
	[IMG_NAME] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 56
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PI_F_HISTORY](
	[IMG_NAME] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]'';

-- Installation step 57
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PIH](
	[CountEntries] [int] NOT NULL,
	[DATUMDOKLADU] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 58
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_PIH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[DATUMDOKLADU] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 59
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_RFID_ITEMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[SEQUENCENMBR] [int] NOT NULL
) ON [PRIMARY]'';

-- Installation step 60
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 61
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SE_SN](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 62
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 63
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SEH](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 64
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SEH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 65
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Cinnost](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[TYPEVALUE] [nvarchar](20) NULL,
	[Mandatory] [tinyint] NOT NULL,
	[RequiredLength] [int] NULL
) ON [PRIMARY]'';

-- Installation step 66
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_CinnostNext](
	[ID] [nvarchar](20) NOT NULL,
	[IDNext] [nvarchar](20) NULL,
	[IDValue] [nvarchar](20) NULL
) ON [PRIMARY]'';

-- Installation step 67
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Dyn_Table](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_Servis_Dyn_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 68
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Dynamic_Table_Definition](
	[FullName] [nvarchar](50) NOT NULL,
	[TypeName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_Dynamic_Table_Definition] PRIMARY KEY CLUSTERED 
(
	[FullName] ASC,
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 69
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Okruh](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[Barcode] [nvarchar](50) NULL,
	[ZdrojSeznamID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_Okruh] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 70
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Predloha](
	[CountEntries] [int] NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NOT NULL,
	[UserID] [int] NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]'';

-- Installation step 71
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Stav](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_Servis_Stav] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 72
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_StavNext](
	[ID] [nvarchar](20) NOT NULL,
	[IDNext] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_StavNext_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[IDNext] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 73
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_Zdroj](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Misto] [nvarchar](20) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Type] [nvarchar](2) NULL,
 CONSTRAINT [PK_CZMST_Servis_Zdroj] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 74
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_ZdrojPohyb](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NOT NULL,
	[IDUser] [int] NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL,
	[dateeveS] [datetime] NULL,
	[dateExported] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 75
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_ZdrojSeznam](
	[ID] [nvarchar](20) NOT NULL,
	[ZdrojID] [nvarchar](20) NOT NULL,
	[Poradi] [int] NULL,
	[IDStav] [nvarchar](20) NULL,
	[IDCinnost] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZMST_Servis_ZdrojSeznam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ZdrojID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 76
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_Servis_ZdrojStav](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NULL,
	[IDUser] [int] NULL,
	[GUID] [uniqueidentifier] NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL
) ON [PRIMARY]'';

-- Installation step 77
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SI_BV](
	[CountEntries] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[pal_WEIGHT] [numeric](19, 5) NULL,
	[pal_W] [numeric](19, 5) NULL,
	[pal_H] [numeric](19, 5) NULL,
	[pal_D] [numeric](19, 5) NULL
) ON [PRIMARY]'';

-- Installation step 78
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SI_RFID](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 79
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SIH](
	[CountEntries] [int] NOT NULL,
	[TISKARNA_NAME] [nvarchar](30) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 80
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SIH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[TISKARNA_NAME] [nvarchar](30) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]'';

-- Installation step 81
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceTypy](
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[IS_RECEIVE] [bit] NOT NULL,
	[IS_DEFAULT] [bit] NOT NULL,
	[IS_NORMAL] [bit] NOT NULL
) ON [PRIMARY]'';

-- Installation step 82
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]'';

-- Installation step 83
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SkladLokace_Mapa](
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[TYPE] [nvarchar](2) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 84
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SSCC_PARAMETERS](
	[ID_SSCC] [int] NOT NULL,
	[DESC_SSCC] [nvarchar](20) NULL,
	[LV] [numeric](1, 0) NULL,
	[GCP] [numeric](9, 0) NULL,
	[GCP_count] [numeric](9, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_SSCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 85
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_SSCC_SEQUENCE](
	[seq_id] [int] NOT NULL,
	[sequence_count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[seq_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 86
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TASK_VERIFY](
	[taskname] [nvarchar](50) NOT NULL,
	[verify_pwd] [nvarchar](50) NOT NULL,
	[taskdesc] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 87
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TERMINAL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ID_TISKARNA] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 88
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TERMINAL_AKT](
	[ID_TERMINAL] [int] NOT NULL,
	[IP] [nvarchar](100) NOT NULL,
	[DATEREQ] [datetime] NOT NULL
) ON [PRIMARY]'';

-- Installation step 89
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TERMINAL_DEFINITION](
	[ID_TERMINAL] [int] NOT NULL,
	[DB_TYPE] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CZMST_TERMINAL_DEFINITION] PRIMARY KEY CLUSTERED 
(
	[ID_TERMINAL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 90
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TISKARNA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[LOCATION] [nvarchar](50) NULL,
	[IP] [nvarchar](100) NULL,
	[PORT] [nvarchar](10) NULL,
	[COM] [nvarchar](50) NULL,
	[SOUBOR] [nvarchar](max) NULL,
	[TIMEOUT] [int] NULL,
	[BARCODE] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_TISKARNA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 91
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST_TISKARNA_zal20260428](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[LOCATION] [nvarchar](50) NULL,
	[IP] [nvarchar](100) NULL,
	[PORT] [nvarchar](10) NULL,
	[COM] [nvarchar](50) NULL,
	[SOUBOR] [nvarchar](max) NULL,
	[TIMEOUT] [int] NULL,
	[BARCODE] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 92
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST090](
	[odb_id] [nvarchar](12) NOT NULL,
	[odb_desc] [nvarchar](31) NULL,
	[odb_typ] [nvarchar](3) NULL,
	[odb_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[odb_ico] [nvarchar](20) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[odb_misto] [nvarchar](100) NULL,
	[odb_ulice] [nvarchar](100) NULL,
	[odb_cisloOr] [nvarchar](15) NULL,
	[odb_psc] [nvarchar](15) NULL,
	[odb_dic] [nvarchar](15) NULL,
	[odb_Odberatel] [bit] NULL,
	[odb_Dodavatel] [bit] NULL
) ON [PRIMARY]'';

-- Installation step 93
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST091](
	[str_id] [nvarchar](30) NOT NULL,
	[str_desc] [nvarchar](40) NULL,
	[str_typ] [nvarchar](3) NULL,
	[str_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[skl_id] [nvarchar](20) NULL,
	[odb_id] [nvarchar](12) NULL
) ON [PRIMARY]'';

-- Installation step 94
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST092](
	[doc_id] [nvarchar](12) NOT NULL,
	[doc_id2] [nvarchar](12) NOT NULL,
	[doc_desc] [nvarchar](31) NULL,
	[doc_typ] [nvarchar](3) NULL,
	[doc_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[cfg_odb] [tinyint] NOT NULL,
	[cfg_str] [tinyint] NOT NULL,
	[cfg_prac] [tinyint] NOT NULL,
	[cfg_mn2sn] [tinyint] NOT NULL,
	[cfg_disp] [tinyint] NOT NULL,
	[cfg_disp_dest] [tinyint] NOT NULL,
	[cfg_palety] [tinyint] NOT NULL,
	[cfg_paleta_id] [tinyint] NOT NULL,
	[cfg_zakazka_id] [tinyint] NOT NULL,
	[cfg_mena_id] [tinyint] NULL,
	[cfg_tisk] [tinyint] NOT NULL,
	[cfg_prevod_sklad] [tinyint] NOT NULL,
	[cfg_tisk_soupis] [tinyint] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[cfg_lokace] [tinyint] NULL,
	[cfg_lokace_ciselnik] [tinyint] NULL,
	[cfg_lokace_dest] [tinyint] NULL,
	[cfg_onl_dop_pal] [tinyint] NULL,
	[cfg_onl_over_lokace] [tinyint] NULL,
	[cfg_onl_over_lokace_dest] [tinyint] NULL,
	[cfg_mnozstvi_ze_zbozi] [tinyint] NULL,
	[cfg_predvyplnit_mnozstvi] [tinyint] NULL,
	[cfg_skl_id_dest] [tinyint] NULL,
	[predvyplnit_skl_id_dest] [nvarchar](20) NULL,
	[cfg_lok_mech] [tinyint] NULL,
	[cfg_lok_mech_pohyb_type] [nvarchar](1) NULL,
	[cfg_skl_id_dest_prevzit] [tinyint] NULL,
	[cfg_lokace_dest_ciselnik] [tinyint] NULL,
	[predvyplnit_locncodedest] [nvarchar](11) NULL,
	[cfg_sklady] [tinyint] NULL,
	[cfg_onl_dop_lokace_dest] [tinyint] NULL,
	[cfg_generovat_sn] [tinyint] NULL,
	[cfg_parsovat_ck] [tinyint] NULL,
	[cfg_sn_na_davku] [tinyint] NULL,
	[cfg_lok_mech_online_pohyby] [tinyint] NULL,
	[cfg_onl_palety_generovat] [tinyint] NULL,
	[cfg_tisk_palety] [tinyint] NULL,
	[cfg_sklady_zmena] [tinyint] NULL,
	[cfg_delka_SN] [int] NULL,
	[cfg_Navrh] [tinyint] NULL,
	[cfg_FIFO_FEFO_check] [tinyint] NULL,
	[cfg_sarze_ONOFF] [tinyint] NULL,
	[cfg_sn_ONOFF] [tinyint] NULL,
	[cfg_expirace_ONOFF] [tinyint] NULL,
	[cfg_AttributeToSN_ONOFF] [tinyint] NULL
) ON [PRIMARY]'';

-- Installation step 95
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST094](
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[Barcode] [nvarchar](50) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 96
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST096](
	[prac_id] [nvarchar](30) NOT NULL,
	[prac_desc] [nvarchar](40) NULL,
	[prac_typ] [nvarchar](3) NULL,
	[prac_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 97
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[CZMST097](
	[mena_ID] [nvarchar](10) NOT NULL,
	[mena_text] [nvarchar](30) NOT NULL,
	[mena_hlavni] [tinyint] NOT NULL,
	[mena_kurz] [numeric](19, 5) NULL,
	[mena_kurzDatum] [date] NULL,
 CONSTRAINT [PK_CZMST097] PRIMARY KEY CLUSTERED 
(
	[mena_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 98
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[EXTERNAL_POST_LOG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[CountEntries] [int] NOT NULL,
	[ItemType] [nvarchar](20) NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 99
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_AfterProcess_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[CountEntries] [int] NOT NULL,
	[ItemType] [nvarchar](20) NULL,
	[MessageText] [nvarchar](400) NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 100
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_AGENDA](
	[AGENDAID] [nvarchar](50) NULL,
	[NAME] [nvarchar](50) NULL,
	[DESCIPTION] [nvarchar](max) NULL,
	[AUTH] [tinyint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 101
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[description] [nvarchar](max) NULL,
	[barcodeReaded] [nvarchar](50) NOT NULL,
	[barcodeSended] [nvarchar](50) NOT NULL,
	[zakazka] [nvarchar](20) NULL,
	[popis] [nvarchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nvarchar](1) NOT NULL,
	[isProcessed] [datetime] NULL,
	[IDO] [nvarchar](10) NULL,
	[scan1] [nvarchar](255) NULL,
	[scan2] [nvarchar](255) NULL,
	[scan3] [nvarchar](255) NULL,
	[sensor] [nvarchar](50) NULL,
	[material] [nvarchar](255) NULL,
	[productionGuid] [uniqueidentifier] NULL,
	[VPH] [nvarchar](30) NULL,
	[VPPol] [int] NULL,
	[EAN_IS] [nvarchar](31) NULL,
	[IS_ID] [nvarchar](40) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[status] [int] NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[PackType] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[BarcodeT] [tinyint] NOT NULL,
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Events_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 102
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Events_HISTORY](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[description] [nvarchar](max) NULL,
	[barcodeReaded] [nvarchar](50) NOT NULL,
	[barcodeSended] [nvarchar](50) NOT NULL,
	[zakazka] [nvarchar](20) NULL,
	[popis] [nvarchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nvarchar](1) NOT NULL,
	[isProcessed] [datetime] NULL,
	[IDO] [nvarchar](10) NULL,
	[scan1] [nvarchar](255) NULL,
	[scan2] [nvarchar](255) NULL,
	[scan3] [nvarchar](255) NULL,
	[sensor] [nvarchar](50) NULL,
	[material] [nvarchar](255) NULL,
	[productionGuid] [uniqueidentifier] NULL,
	[VPH] [nvarchar](30) NULL,
	[VPPol] [int] NULL,
	[EAN_IS] [nvarchar](31) NULL,
	[IS_ID] [nvarchar](40) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[status] [int] NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[PackType] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[BarcodeT] [tinyint] NOT NULL,
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_Events_HISTORY] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Events_HISTORY_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 103
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_EventsErr](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NULL,
	[qtyReal] [numeric](19, 5) NULL,
	[description] [nvarchar](max) NULL,
	[barcodeReaded] [nvarchar](50) NULL,
	[barcodeSended] [nvarchar](50) NULL,
	[zakazka] [nvarchar](20) NULL,
	[popis] [nvarchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nvarchar](1) NULL,
	[isProcessed] [datetime] NULL,
	[IDO] [nvarchar](10) NULL,
	[scan1] [nvarchar](255) NULL,
	[scan2] [nvarchar](255) NULL,
	[scan3] [nvarchar](255) NULL,
	[sensor] [nvarchar](50) NULL,
	[material] [nvarchar](255) NULL,
	[productionGuid] [uniqueidentifier] NULL,
	[VPH] [nvarchar](30) NULL,
	[VPPol] [int] NULL,
	[EAN_IS] [nvarchar](31) NULL,
	[IS_ID] [nvarchar](40) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[status] [int] NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[PackType] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[BarcodeT] [tinyint] NULL,
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_EventsErr] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EventsErr_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 104
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_FORMULARE](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev_okna] [nvarchar](max) NOT NULL,
	[nazev] [nvarchar](max) NULL,
	[ord] [int] NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](max) NULL,
 CONSTRAINT [PK_FASK_FORMULARE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 105
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_FORMULARE_20250714_vzor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev_okna] [nvarchar](max) NOT NULL,
	[nazev] [nvarchar](max) NULL,
	[ord] [int] NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 106
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_FORMULARE_20250912_vzor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev_okna] [nvarchar](max) NOT NULL,
	[nazev] [nvarchar](max) NULL,
	[ord] [int] NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 107
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_FORMULARE_20250926_vzor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev_okna] [nvarchar](max) NOT NULL,
	[nazev] [nvarchar](max) NULL,
	[ord] [int] NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 108
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_FORMULARE_KS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev_okna] [nvarchar](max) NOT NULL,
	[nazev] [nvarchar](max) NULL,
	[ord] [int] NULL,
	[id_h] [int] NULL,
	[typ_h] [nvarchar](100) NULL,
	[typ] [nvarchar](2) NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[formular] [nvarchar](max) NULL,
 CONSTRAINT [PK_FASK_FORMULARE_KS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 109
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Machines](
	[id] [nvarchar](20) NOT NULL,
	[machinetype] [nvarchar](10) NOT NULL,
	[name] [nvarchar](10) NOT NULL,
	[description] [nchar](50) NOT NULL,
	[koeficient] [decimal](19, 5) NOT NULL,
 CONSTRAINT [PK_FASK_FASK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 110
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_MachineType](
	[machinetype] [nvarchar](10) NOT NULL,
	[machinetypename] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FASK_MachineType] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 111
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Operations](
	[machinetype] [nvarchar](10) NOT NULL,
	[IDO] [nvarchar](10) NOT NULL,
	[NAZEV] [nvarchar](50) NOT NULL,
	[CK] [nvarchar](30) NOT NULL,
	[SCAN1] [tinyint] NULL,
	[SCAN2] [tinyint] NULL,
	[SCAN3] [tinyint] NULL,
	[SCANZAKAZKA] [tinyint] NULL,
	[SENSOR] [tinyint] NULL,
	[VOLNA] [bit] NULL,
	[START] [bit] NULL,
	[KONEC] [bit] NULL,
	[SPHLAVICKA] [nvarchar](50) NULL,
	[SPINFO] [nvarchar](50) NULL,
	[SPZAKAZKA] [nvarchar](50) NULL,
	[KONTROLAMAT] [bit] NULL,
	[SPMATERIAL] [nvarchar](50) NULL,
	[LOGIN] [bit] NULL,
 CONSTRAINT [PK_FASK_Operations] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC,
	[IDO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 112
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Operations_Next](
	[machinetype] [nvarchar](10) NOT NULL,
	[IDO] [nvarchar](10) NOT NULL,
	[IDO_NEXT] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_FASK_Operations_Next] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC,
	[IDO] ASC,
	[IDO_NEXT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 113
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_PLANOVANI](
	[GUID] [uniqueidentifier] NOT NULL,
	[DESC] [nvarchar](200) NOT NULL
) ON [PRIMARY]'';

-- Installation step 114
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_POHODA_RESPONSE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Resp_ID] [nvarchar](50) NULL,
	[Resp_Name] [nvarchar](255) NULL,
	[Resp_DateTime] [datetime] NULL,
	[Resp_ICO] [nvarchar](50) NULL,
	[Resp_Key] [nvarchar](50) NULL,
	[Resp_Note] [nvarchar](255) NULL,
	[Resp_State] [nvarchar](20) NULL,
	[Item_ID] [nvarchar](50) NULL,
	[Item_Note] [nvarchar](255) NULL,
	[Item_State] [nvarchar](20) NULL,
	[Item_Type] [nvarchar](50) NULL,
	[Detail_State] [nvarchar](20) NULL,
	[Detail_Type_Errno] [nvarchar](20) NULL,
	[Detail_Type_Note] [nvarchar](255) NULL,
	[Detail_Type_State] [nvarchar](20) NULL,
	[Detail_Type_VProcesed] [nvarchar](255) NULL,
	[Detail_Type_VRequested] [nvarchar](255) NULL,
	[Detail_Type_XPath] [nvarchar](255) NULL,
	[Produced_Detail_ID] [nvarchar](50) NULL,
	[Produced_Detail_ActionType] [nvarchar](50) NULL,
	[Produced_Detail_Code] [nvarchar](50) NULL,
	[Produced_Detail_Number] [nvarchar](50) NULL
) ON [PRIMARY]'';

-- Installation step 115
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_RADY](
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
	[Import_Doklad_IS] [bit] NOT NULL
) ON [PRIMARY]'';

-- Installation step 116
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_FASK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 117
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_TISKOVE_ULOHY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_TISKARNY] [int] NULL,
	[ID_FORMULARE] [int] NULL,
	[POCET] [int] NULL,
	[HEIGHT_PAPER_SIZE] [int] NULL,
	[WIDTH_PAPER_SIZE] [int] NULL,
	[PAPER_KIND] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_TISKOVE_ULOHY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 118
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[rez_1] [nvarchar](50) NULL,
	[rez_2] [nvarchar](50) NULL,
 CONSTRAINT [PK_FASK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FASK_UserEvents_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 119
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Vyroba_PVH](
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL,
	[SOPDESC] [nvarchar](100) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[Active] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TypDok] [nvarchar](20) NULL
) ON [PRIMARY]'';

-- Installation step 120
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Vyroba_PVP](
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
	[ITEMCODE] [nvarchar](70) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DATE_ZAPLANOVANI] [datetime] NULL,
	[VP_PRPS] [bit] NOT NULL,
	[VP_PRPS_QTY] [numeric](19, 5) NULL,
	[VP_PRPS_SOPNUMBE] [nvarchar](100) NULL,
	[VP_PRDCT_QTY] [numeric](19, 5) NULL,
	[USERID] [int] NULL,
	[Ref_PVH] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]'';

-- Installation step 121
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_Vyroba_TP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_H] [nvarchar](50) NULL,
	[ID_L] [nvarchar](50) NULL,
	[ITEMNMBR_Def] [nvarchar](31) NULL,
	[DESC_Def] [nvarchar](100) NULL,
	[MJ_Def] [nvarchar](50) NULL,
	[ITEMNMBR_fol] [nvarchar](31) NULL,
	[DESC_Fol] [nvarchar](100) NULL,
	[MJ_Fol] [nvarchar](50) NULL,
	[koef] [nvarchar](50) NULL,
	[ID_USER] [nvarchar](50) NULL,
	[dateedit] [datetime] NULL,
	[alter] [nvarchar](1) NULL,
	[PUO] [nvarchar](1) NULL,
 CONSTRAINT [PK_FASK_Vyroba_TP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 122
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[DefDod] [bit] NOT NULL,
	[RefAg] [int] NULL,
	[IDS_SKz] [nvarchar](255) NULL,
	[ID_sSklad] [int] NULL,
	[RefAD] [int] NULL,
	[Firma] [nvarchar](255) NULL,
	[NakupC] [numeric](19, 5) NULL,
	[RefCM] [int] NULL,
	[CmKurs] [numeric](19, 5) NULL,
	[EAN] [nvarchar](20) NULL,
	[MJEAN] [varchar](10) NULL,
	[MJkoefEAN] [numeric](19, 5) NULL,
	[Pozn] [nvarchar](255) NULL,
	[Status_Err] [int] NULL
) ON [PRIMARY]'';

-- Installation step 123
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY_MENY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[mena_ID] [nvarchar](10) NOT NULL,
	[PRICE] [numeric](18, 2) NULL,
	[PRICEX] [int] NULL
) ON [PRIMARY]'';

-- Installation step 124
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY_PARAMETRY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[VPrFVTS] [bit] NOT NULL,
	[VPrFPTS] [bit] NOT NULL,
	[VPrFDTS] [bit] NOT NULL,
	[VPrFITS] [bit] NOT NULL,
	[VPrFXTS] [bit] NOT NULL,
	[RefVPrFVTS] [int] NULL,
	[RefVPrFPTS] [int] NULL,
	[RefVPrFDTS] [int] NULL,
	[RefVPrFITS] [int] NULL,
	[RefVPrFXTS] [int] NULL,
	[VPrTIMEPREP] [float] NULL,
	[VPrTIMEUNIT] [float] NULL,
	[RefVPrTIMEMODE] [int] NULL
) ON [PRIMARY]'';

-- Installation step 125
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY_STAV](
	[KOD] [nvarchar](12) NOT NULL,
	[NAZEV] [nvarchar](50) NOT NULL,
	[KOD_LOK] [nvarchar](3) NOT NULL,
	[STAV] [decimal](12, 3) NOT NULL,
	[CENA] [decimal](15, 3) NOT NULL,
	[CENA_ZUST] [decimal](10, 3) NOT NULL,
	[REZERVACE] [decimal](12, 3) NOT NULL,
	[TS] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FASK_ZASOBY_STAV] PRIMARY KEY CLUSTERED 
(
	[KOD] ASC,
	[KOD_LOK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 126
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[FASK_ZASOBY_zal_20260129](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[DMJ] [nvarchar](200) NOT NULL,
	[TAXRATE] [numeric](4, 2) NULL,
	[PRICE0] [numeric](18, 2) NULL,
	[PRICE1] [numeric](18, 2) NULL,
	[PRICE2] [numeric](18, 2) NULL,
	[PRICE3] [numeric](18, 2) NULL,
	[PRICE4] [numeric](18, 2) NULL,
	[PRICE5] [numeric](18, 2) NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_Rez1_Track] [tinyint] NOT NULL,
	[CZ_Rez2_Track] [tinyint] NOT NULL,
	[CZ_Rez3_Track] [tinyint] NOT NULL,
	[CZ_Rez4_Track] [tinyint] NOT NULL,
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
	[REZ3] [nvarchar](50) NULL,
	[REZ4] [nvarchar](50) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[TIMEFROM] [datetime] NULL,
	[TIMETO] [datetime] NULL,
	[LSTMod] [datetime] NULL,
	[loginid] [nvarchar](20) NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL,
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]'';

-- Installation step 127
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Groups](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 128
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[INVENTUR](
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
) ON [PRIMARY]'';

-- Installation step 129
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[KANCL](
	[KANCL] [nvarchar](6) NOT NULL,
	[STRE] [nvarchar](6) NULL,
	[TEXT] [nvarchar](50) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[EAN] [nvarchar](50) NULL,
 CONSTRAINT [PK__KANCL__JKR] PRIMARY KEY CLUSTERED 
(
	[KANCL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 130
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[LOKACE](
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
) ON [PRIMARY]'';

-- Installation step 131
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Machines](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
 CONSTRAINT [PK_FASK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 132
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[MachinesDefinition](
	[IP] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[MType] [nvarchar](50) NOT NULL,
	[PORT] [int] NULL,
	[ID_group] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[id] [nvarchar](20) NULL,
 CONSTRAINT [PK_MachinesDefinition] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 133
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[MachinesDefinitionMeasurement](
	[IP] [nvarchar](20) NOT NULL,
	[IP_M] [nvarchar](20) NOT NULL,
	[Description_M] [nvarchar](100) NULL,
	[MType_M] [nvarchar](50) NOT NULL,
	[PORT_M] [int] NULL,
	[ID_group_M] [int] NULL
) ON [PRIMARY]'';

-- Installation step 134
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[MachineStateSet](
	[IP] [nvarchar](20) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[S0] [int] NULL,
	[S1] [int] NULL,
	[S2] [int] NULL,
	[S3] [int] NULL,
	[S4] [int] NULL,
	[S5] [int] NULL,
	[S6] [int] NULL,
	[S7] [int] NULL,
	[S8] [int] NULL,
	[S9] [int] NULL,
	[S10] [int] NULL,
	[S11] [int] NULL,
	[counter_0] [int] NULL,
	[counter_1] [int] NULL,
	[counter_2] [int] NULL,
	[counter_3] [int] NULL,
	[counter_4] [int] NULL,
	[counter_5] [int] NULL,
	[counter_6] [int] NULL,
	[counter_7] [int] NULL,
	[counter_8] [int] NULL,
	[counter_9] [int] NULL,
	[counter_10] [int] NULL,
	[counter_11] [int] NULL,
	[LastError] [nvarchar](50) NULL,
	[ID_group] [int] NOT NULL,
 CONSTRAINT [PK_MachineStateSet] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 135
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[MachineStateSetHistory](
	[IP] [nvarchar](20) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[S0] [int] NULL,
	[S1] [int] NULL,
	[S2] [int] NULL,
	[S3] [int] NULL,
	[S4] [int] NULL,
	[S5] [int] NULL,
	[S6] [int] NULL,
	[S7] [int] NULL,
	[S8] [int] NULL,
	[S9] [int] NULL,
	[S10] [int] NULL,
	[S11] [int] NULL,
	[counter_0] [int] NULL,
	[counter_1] [int] NULL,
	[counter_2] [int] NULL,
	[counter_3] [int] NULL,
	[counter_4] [int] NULL,
	[counter_5] [int] NULL,
	[counter_6] [int] NULL,
	[counter_7] [int] NULL,
	[counter_8] [int] NULL,
	[counter_9] [int] NULL,
	[counter_10] [int] NULL,
	[counter_11] [int] NULL,
	[LastError] [nvarchar](50) NULL,
	[ID_group] [int] NOT NULL
) ON [PRIMARY]'';

-- Installation step 136
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[MAJETEK](
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
	[ID_INV] [nvarchar](20) NULL,
	[ID_TERM] [numeric](20, 0) NULL,
 CONSTRAINT [PK__MAJETEK__JKR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 137
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Operations](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 138
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[OSOBY](
	[OSOBA_ZODP] [int] NOT NULL,
	[TITUL] [nvarchar](6) NULL,
	[PRIJMENI] [nvarchar](20) NULL,
	[JMENO] [nvarchar](12) NULL,
 CONSTRAINT [PK__OSOBY__JKR] PRIMARY KEY CLUSTERED 
(
	[OSOBA_ZODP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 139
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Production_HISTORY](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL,
	[ITEMMJ] [nvarchar](5) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NULL,
	[TIMEMODE] [int] NULL,
	[TIMEPREPSTART] [datetime] NULL,
	[TIMEPREPSTOP] [datetime] NULL,
	[TIMEPREP] [real] NULL,
	[TIMEUNIT] [real] NULL,
	[TIMESTART] [datetime] NULL,
	[TIMESTOP] [datetime] NULL,
	[TIMECORSTART] [datetime] NULL,
	[TIMECORSTOP] [datetime] NULL,
	[TIMECOR] [real] NULL,
	[TIMECRID] [int] NULL,
	[TIMECRIDTYPE] [tinyint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[operationid] [nvarchar](16) NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[description] [nvarchar](max) NULL,
	[BarcodeP] [nvarchar](31) NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[ISOK] [datetime] NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[SOUBEHGUID] [uniqueidentifier] NULL,
	[CORRGUID] [uniqueidentifier] NULL,
	[qtyOld] [numeric](19, 5) NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[EXPIRATION] [nvarchar](50) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PackType] [nvarchar](50) NULL,
	[status] [int] NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[STORNOGUID] [uniqueidentifier] NULL,
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL,
	[WEIGHT_OLD] [numeric](19, 5) NULL,
 CONSTRAINT [PK_FASK_Production_HISTORY] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'';

-- Installation step 140
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Production_SN](
	[GUID_Production] [uniqueidentifier] NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[Expirace] [datetime] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]'';

-- Installation step 141
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[Production_Sources](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNAME] [nvarchar](51) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[GUID_Production] [uniqueidentifier] NULL,
	[GUID] [uniqueidentifier] NULL,
	[USER_ID] [nvarchar](10) NULL,
	[TERMINAL_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[ISOK] [datetime] NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 142
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 143
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[UCSTR](
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
) ON [PRIMARY]'';

-- Installation step 144
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[REZ1] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]'';

-- Installation step 145
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[VLoginsGroups](
	[loginid] [nvarchar](20) NOT NULL,
	[groupid] [nvarchar](10) NOT NULL
) ON [PRIMARY]'';

-- Installation step 146
EXEC sys.sp_executesql N''CREATE TABLE [dbo].[VMachinesOperations](
	[machineid] [nvarchar](20) NOT NULL,
	[operationid] [nvarchar](16) NOT NULL
) ON [PRIMARY]'';

-- Installation step 147
EXEC sys.sp_executesql N''CREATE FUNCTION [dbo].[fask_func_convert_to_datetime](@timedone nvarchar(6), @datedone nvarchar(8))
RETURNS DATETIME-- NVARCHAR(17)
AS 
BEGIN
	IF @timedone is null
	BEGIN
		return null;
	END	
	
	IF @datedone is null
	BEGIN
		return null;
	END
	
	if LEN(@timedone) < 6
	BEGIN
		return null;
	END
	
	if LEN(@datedone) < 8
	BEGIN
		return null;
	END
	/*
	DECLARE @dat varchar(14) = ''''20151003092957'''';
	declare @year varchar(4) = substring(@dat, 1, 4);
	declare @month varchar(2) = substring(@dat, 5, 2);
	declare @day varchar(2) = substring(@dat, 7, 2);
	declare @hour varchar(2) = substring(@dat, 9, 2);
	declare @min varchar(2) = substring(@dat, 11, 2);
	declare @sec varchar(2) = substring(@dat, 13, 2);
	*/
	--DECLARE @dat varchar(14) = ''''20151003092957'''';
	declare @year nvarchar(4) = substring(@datedone, 1, 4);
	declare @month nvarchar(2) = substring(@datedone, 5, 2);
	declare @day nvarchar(2) = substring(@datedone, 7, 2);
	declare @hour nvarchar(2) = substring(@timedone, 1, 2);
	declare @min nvarchar(2) = substring(@timedone, 3, 2);
	declare @sec nvarchar(2) = substring(@timedone, 5, 2);
	
	/* yyyy-mm-dd hh:mi:ss (24h) (ODBC canonical) */
	return CONVERT(datetime, @year + ''''-'''' + @month + ''''-'''' + @day + '''' '''' + @hour + '''':'''' + @min + '''':'''' + @sec, 120)
END'';

-- Installation step 148
EXEC sys.sp_executesql N''CREATE FUNCTION [dbo].[fask_func_PriznakSledovaniZasoby](@RelSKzVC [int] = null, @ID [int], @EvidenceSarzi [bit], @EvidenceVyrobnichCisel [bit], @PohodaE1 [bit])
RETURNS [int] WITH EXECUTE AS CALLER
AS 
BEGIN
	
	DECLARE @VPrFXTS int ;
	DECLARE @VPrFDTS int ;
	DECLARE @RefVPrFXTS int ;
	DECLARE @RefVPrFDTS int ;

	DECLARE @VPrCZSerNumTrIS int ;

	DECLARE @Result int;
	SET @Result = 0;

	SET @Result = ISNULL(@RelSKzVC, 0);
	
	--IF @Result = 0
	--	BEGIN

	--		IF @PohodaE1 = 1
	--			BEGIN
	--				SELECT distinct 
	--				@VPrFXTS = VPrFXTS,
	--				@VPrFDTS = VPrFDTS,
	--				@RefVPrFXTS = RefVPrFXTS,
	--				@RefVPrFDTS = RefVPrFDTS
	--				 FROM StwPh_04535667_2020.dbo.SKz where ID = @ID

	--				IF ISNULL(@VPrFXTS, 0) = 1
	--					BEGIN
	--						SET @Result = ISNULL(@RefVPrFXTS,1) - 1;

	--							IF @Result = 1
	--								BEGIN
	--									IF @EvidenceVyrobnichCisel = 0
	--										BEGIN
	--											SET @Result = 0;
	--										END
	--								END
	--							IF @Result = 2
	--								BEGIN
	--									IF @EvidenceSarzi = 0
	--										BEGIN
	--											SET @Result = 0;
	--										END
	--								END
	--					END
	--				ELSE
	--					BEGIN
	--						IF ISNULL(@VPrFDTS, 0) = 1
	--							BEGIN
	--								SET @Result = ISNULL(@RefVPrFDTS,1) - 1;

	--									IF @Result = 1
	--										BEGIN
	--											IF @EvidenceVyrobnichCisel = 0
	--												BEGIN
	--													SET @Result = 0;
	--												END
	--										END
	--									IF @Result = 2
	--										BEGIN
	--											IF @EvidenceSarzi = 0
	--												BEGIN
	--													SET @Result = 0;
	--												END
	--										END
	--							END
	--					END
	--			END
	--	END
	--ELSE IF @Result = 1
	--  BEGIN
	--	IF @PohodaE1 = 1
	--		BEGIN
			
	--			SELECT distinct 
	--			@VPrCZSerNumTrIS = VPrCZSerNumTrIS
	--			FROM StwPh_04535667_2020.dbo.SKz where ID = @ID

	--			IF ISNULL(@VPrCZSerNumTrIS, 0) = 1
	--				BEGIN
	--					SET @Result = 10;
	--				END
	--		END
	--  END


	RETURN @Result

END'';

-- Installation step 149
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 21.2.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[FASK_GeneratorRad]
(	
	-- Add the parameters for the function here
	@OD int ,
	@Pocet int,
	@N int ,
	@prefix nvarchar(50) 
)
RETURNS @Rada TABLE
(
	[ID] int,
	[Value] [nvarchar](100) not NULL

)
AS
BEGIN

	IF @OD  < 0 OR @Pocet <= 0 OR @N <= 0
		BEGIN
			insert into @Rada ([ID],[Value])
			SELECT -1, ''''ERR''''
		END
	ELSE
		BEGIN
			DECLARE @cnt INT = 0;

			WHILE @cnt < @Pocet
			BEGIN
				INSERT INTO @Rada ([ID],[Value])
				SELECT @cnt as ID, (@prefix + REPLACE(STR(@OD, @N, 0), '''' '''', ''''0'''')) as [Value]

			   SET @cnt = @cnt + 1;
			   SET @OD = @OD + 1;
			END
		END
 	return
END'';

-- Installation step 150
EXEC sys.sp_executesql N''-- =============================================
  -- Author:	Bc. Tadeas Divacky
  -- Create date: 22.01.2020
  -- Description:	Funkce která vraci uživatelske parametry pro filtrovani do načteí obchodniho požadavk pro Planovani
  -- =============================================
  CREATE FUNCTION [dbo].[FASK_Get_Planovani_NacteniUserParams]
  (	)
  RETURNS @Params TABLE
  (
 	[ORD] [int] NULL,
  	[UserParam_1] [nvarchar](100) NULL,
 	[UserParam_2] [nvarchar](100) NULL,
 	[UserParam_3] [nvarchar](100) NULL,
 	[UserParam_4] [nvarchar](100) NULL,
 	[UserParam_5] [nvarchar](100) NULL
  )
  AS
  BEGIN
  
  
   --insert into @Params ([ORD], [UserParam_1], [UserParam_2], [UserParam_3],[UserParam_4], [UserParam_5])
  	--	SELECT 
 		--op.ID as [ORD],
 		--[UserParam_1] = CASE o.VPrZapl    
 		--	WHEN 1 THEN ''''Zaplaceno''''    
 		--	ELSE ''''Nezaplaceno''''  
 		--END ,
 		--UzivSeznamPol.IDS as [UserParam_2],
 		--'''''''' as [UserParam_3],
 		--'''''''' as [UserParam_4],
 		--'''''''' as [UserParam_5]
  	--	FROM StwPh_63489040_2025.dbo.OBJ o
 		--left join StwPh_63489040_2025.dbo.OBJpol op ON op.RefAg = o.ID
  	--	left join StwPh_63489040_2025.dbo.sVPULpol UzivSeznamPol ON UzivSeznamPol.ID = o.RefVPrDoprava
   	return
  
  END'';

-- Installation step 151
EXEC sys.sp_executesql N''-- =============================================
  -- Author:	Bc. Tadeas Divacky
  -- Create date: 22.01.2020
  -- Description:	Funkce která vraci status zda lze položku zaplanovat
  -- =============================================
 CREATE FUNCTION [dbo].[FASK_Get_Planovani_Navrhar_FIFO_OBJ]
  (	
  	@ORD int ,
 	@ITEMNMBR int,
 	@QTY numeric(19,5)
  )
  RETURNS @Params TABLE
  (
 	[Flag] [bit] NULL
  )
  AS
  BEGIN
  
 --DECLARE @CNT_Nalezene int
 --DECLARE @Skladem numeric(19,5)
 --DECLARE @Vytvoreno datetime
 
 --DECLARE @Zaplacena bit
 
 
 --SELECT @Zaplacena = O.VPrZapl FROM
 --StwPh_63489040_2025.dbo.OBJ as O 
 --left join StwPh_63489040_2025.dbo.OBJpol as pol ON O.ID = pol.RefAg
 --where 1 = 1
 --AND pol.RefSKz = @ITEMNMBR
 --AND pol.ID = @ORD
 
 --IF @Zaplacena = 0
 --	BEGIN
 --		insert into @Params ([Flag]) SELECT 0 as Flag
 --		return
 --	END
 
 
 --SELECT @Vytvoreno = o.DatCreate FROM
 --StwPh_63489040_2025.dbo.OBJ as O 
 --left join StwPh_63489040_2025.dbo.OBJpol as pol ON O.ID = pol.RefAg
 --where 1 = 1
 --AND pol.ID = @ORD
 
 --SELECT @CNT_Nalezene = count(*) FROM
 --StwPh_63489040_2025.dbo.OBJ as O 
 --left join StwPh_63489040_2025.dbo.OBJpol as pol ON O.ID = pol.RefAg
 --left join StwPh_63489040_2025.dbo.SKz as zas ON zas.ID = pol.RefSKz 
 --where 1 = 1
 --AND pol.RefSKz = @ITEMNMBR
 --AND O.Vyrizeno = 0
 --AND O.VPrZapl = 0
 --AND pol.ID != @ORD
 --AND o.DatCreate < @Vytvoreno
 
 --SELECT @Skladem = StavZ FROM StwPh_63489040_2025.dbo.SKz
 --WHERE ID = @ITEMNMBR
 
 --IF @CNT_Nalezene > 0
 --	BEGIN
 
 --	declare @MN numeric(19,5)
 
 --		SELECT @MN = SUM(pol.Mnozstvi - pol.Dodano) FROM
 --		StwPh_63489040_2025.dbo.OBJ as O 
 --		left join StwPh_63489040_2025.dbo.OBJpol as pol ON O.ID = pol.RefAg
 --		left join StwPh_63489040_2025.dbo.SKz as zas ON zas.ID = pol.RefSKz 
 --		where 1 = 1
 --		AND pol.RefSKz = @ITEMNMBR
 --		AND O.Vyrizeno = 0
 --		AND O.VPrZapl = 0
 --		AND pol.ID != @ORD
 --		AND o.DatCreate < @Vytvoreno
 --		group by pol.ID
 
 --		IF (@QTY + @MN) <= @Skladem
 --			BEGIN
 --				insert into @Params ([Flag]) SELECT 1 as Flag
 --			END
 --		ELSE
 --			BEGIN
 --				insert into @Params ([Flag]) SELECT 0 as Flag
 --			END
 
 --	END
 --ELSE 
 --	BEGIN
 --		insert into @Params ([Flag]) SELECT 1 as Flag
 --	END
 
  
  return
 
 
  
  END'';

-- Installation step 152
EXEC sys.sp_executesql N''CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit NVARCHAR(MAX) , @delimiter CHAR(1)='''','''')
RETURNS
 @returnList TABLE 
 (
 [ID] int,
 [Name] [nvarchar] (500)
 )
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @ID int 
 DECLARE @pos INT
 
    SET @ID = 0;

 WHILE CHARINDEX(@delimiter, @stringToSplit) > 0
 BEGIN
 
  SELECT @pos  = CHARINDEX(@delimiter, @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @ID, @name 
  
    SET @ID = @ID + 1;

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @ID, @stringToSplit
 
 RETURN
END'';

-- Installation step 153
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[FASK_PLANOVANI_View]
AS
SELECT        N.[DESC] AS ColumnName_Lokalizace, P.Value, P.GUID_PLANOVANI, P.ColumnName AS ColumnName_Original
FROM            dbo.FASK_PLANOVANI_PARAMS AS P LEFT OUTER JOIN
                         dbo.FASK_PLANOVANI_PARAMS_Name AS N ON N.Column_Name = P.ColumnName'';

-- Installation step 154
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[CZMST_SI_GroupBy]
AS
SELECT        CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, SERLTNUM, Expirace, 
                         0 AS INPUT_MODE, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, VNDDOCNM
FROM            dbo.CZMST_SI
GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM, SERLTNUM, Expirace'';

-- Installation step 155
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[CZMST_SI_NO_GroupBy]
AS
SELECT        CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, 0 AS INPUT_MODE, 
                         CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, VNDDOCNM
FROM            dbo.CZMST_SI
GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM'';

-- Installation step 156
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[FASK_Inventura_I123_Compare]
AS
SELECT        I1.CountEntries, I1.ITEMNMBR, Z.ITEMDESC, I1.ITEMCODE, I3.VNDITNUM, I1.SKL_ID, S.skl_desc, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN SUM(I1.QUANTITY) 
	WHEN I1.CZ_SerNum_Track = 1 THEN SUM(I2.QTY) 
	WHEN I1.CZ_SerNum_Track = 2 THEN SUM(I2.QTY) 
END AS QUANTITY, 
I3.MJ, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN NULL 
	WHEN I1.CZ_SerNum_Track = 1 THEN I2.SERLNMBR 
	WHEN I1.CZ_SerNum_Track = 2 THEN I2.SERLNMBR 
END AS SERLNMBR, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN NULL
	WHEN I1.CZ_SerNum_Track = 1 THEN I2.Expirace 
	WHEN I1.CZ_SerNum_Track = 2 THEN I2.Expirace 
END AS Expirace, 
I1.CZ_SerNum_Track
FROM dbo.CZMST_I1 AS I1 
LEFT OUTER JOIN dbo.CZMST_I3 AS I3 ON I3.CountEntries = I1.CountEntries AND I3.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST_I2 AS I2 ON I2.CountEntries = I1.CountEntries AND I2.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.FASK_ZASOBY AS Z ON Z.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST093 AS S ON S.skl_id = I1.SKL_ID
GROUP BY I1.CountEntries, I1.CZ_SerNum_Track, I1.ITEMNMBR, Z.ITEMDESC, I1.ITEMCODE, I3.VNDITNUM, I1.SKL_ID, S.skl_desc, I2.SERLNMBR, I2.Expirace, I3.MJ'';

-- Installation step 157
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[FASK_Inventura_I4_Compare]
AS
SELECT        I4.CountEntries, I4.ITEMNMBR, Z.ITEMDESC, I4.ITEMCODE, I4.VNDITNUM, I4.SKL_ID, S.skl_desc, SUM(I4.QUANTITY) AS QUANTITY, I4.MJ, I4.SERLNMBR, I4.Expirace, I1.CZ_SerNum_Track
FROM            dbo.CZMST_I4 AS I4 
LEFT OUTER JOIN dbo.FASK_ZASOBY AS Z ON Z.ITEMNMBR = I4.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST093 AS S ON S.skl_id = I4.SKL_ID 
LEFT OUTER JOIN dbo.CZMST_I1 AS I1 ON I1.ITEMNMBR = I4.ITEMNMBR AND I1.CountEntries = I4.CountEntries
GROUP BY I4.CountEntries, I4.ITEMNMBR, Z.ITEMDESC, I4.ITEMCODE, I4.VNDITNUM, I4.SKL_ID, S.skl_desc, I4.SERLNMBR, I4.Expirace, I4.MJ, I1.CZ_SerNum_Track'';

-- Installation step 158
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_stav_naplneni_inventury]
AS
SELECT
CONVERT(nvarchar(2), ''''I'''') AS Type, 
'''''''' as DOC_ID,
'''''''' as  [ITEMTYPE],
zbozi.ITEMDESC, 
CONVERT(nvarchar(30), '''''''') AS DOCUMENT_NUMBER, 
stav.ITEMNMBR, 
stav.QTYSHPPD_DEF AS QTYSHPPD, 
stav.SKL_ID, 
stav.LOCNCODE, 
stav.SERLTNUM, 
NULL AS CountEntries, 
i4.CZ_CarKod, 
i4.USERID AS USER_ID, 
i4.ID_TERMINAL, 
stav.QTYSHPPD_DEF_DATE AS dateeve, 
zbozi.ITEMCODE, 
i4.VNDITNUM, 
i4.WEIGHT, 
i4.GUID, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_Stav AS stav LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = stav.ITEMNMBR LEFT OUTER JOIN
dbo.CZMST_I4 AS i4 ON stav.ITEMNMBR = i4.ITEMNMBR AND stav.LOCNCODE = i4.LOCNCODE'';

-- Installation step 159
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_lokacni_mechanismus_pohyby]
AS
SELECT 
pohyb.POHYB_TYPE AS Type, 
'''''''' as DOC_ID,
'''''''' as  [ITEMTYPE],
zbozi.ITEMDESC, 
pohyb.DOCUMENT_NUMBER, 
pohyb.ITEMNMBR, 
pohyb.QTYSHPPD, 
pohyb.SKL_ID_SRC AS SKL_ID, 
pohyb.LOCNCODE_SRC AS LOCNCODE, 
pohyb.SERLTNUM, 
pohyb.CountEntries, 
zbozi.CZ_CarKod AS CZ_CarKod, 
pohyb.UserID AS USER_ID, 
pohyb.TermID AS ID_TERMINAL, 
pohyb.dateeveT AS dateeve, 
zbozi.ITEMCODE, 
zbozi.VNDITNUM, 
zbozi.WEIGHT, 
pohyb.guid, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_StavPohyb AS pohyb LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = pohyb.ITEMNMBR'';

-- Installation step 160
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 28.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontroli HESLA dle zadaneho ID 
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverHeslo]
(	
	-- Add the parameters for the function here
	@inID nvarchar(25),
	@inHESLO nvarchar(10)

)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT psswd FROM FASK_logins WHERE USERID = @inID AND psswd = @inHESLO
)'';

-- Installation step 161
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 25.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontrol ID
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverId]
(	
	-- Add the parameters for the function here
	@id nvarchar(25) 
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT USERID FROM FASK_logins WHERE USERID = @id
)'';

-- Installation step 162
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[CZPRO_VPH_View]
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
      ,vph.[USERID]
FROM         
	CZPRO_VPH vph
WHERE ISNULL(vph.Active,1)=1'';

-- Installation step 163
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[CZPRO_VPP_View]
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
	case 
		when vpp.LSTMod > isnull(psum.LSTMod, getdate()) then vpp.LSTMod
		else isnull(psum.LSTMod, getdate())
	end as LSTMod, 
	vpp.DEX_ROW_ID, 
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO,
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO,
	vpp.BarcodeT,
	vpp.CZ_REZ1_Track,
	vpp.CZ_REZ2_Track,
        vpp.CZ_REZ3_Track,
	vpp.CZ_REZ4_Track,
	vpp.CZ_REZ5_Track,
	vpp.WEIGHT_TARA,
	vpp.WEIGHT_NETTO,
	vpp.WEIGHT_TOL_PLUS,
	vpp.WEIGHT_TOL_MINUS
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO , MAX(dateeve) as LSTMod, BarcodeP
	FROM   dbo.Production
	WHERE  dateeve > (Select TOP 1 LSTMod from CZPRO_VPP as vpp2 where dbo.Production.CountEntries = vpp2.CountEntries and dbo.Production.SOPNUMBE = vpp2.SOPNUMBE AND dbo.Production.ITEMNMBR = vpp2.ITEMNMBR and dbo.Production.BarcodeP = vpp2.BarcodeP)
	GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP
) AS psum ON psum.CountEntries = vpp.CountEntries and psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR AND psum.BarcodeP = vpp.BarcodeP 
LEFT JOIN CZPRO_VPH AS vph ON vph.CountEntries=vpp.CountEntries and vph.SOPNUMBE=vpp.SOPNUMBE
WHERE ISNULL(vph.Active,1)=1'';

-- Installation step 164
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[FASK_Logins_View_Prava]
AS
SELECT        L.USERID, L.firstname, L.surname, L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, A.AGENDAID, L.RFID
FROM            dbo.FASK_Logins AS L LEFT OUTER JOIN
                         dbo.FASK_Logins_Auth AS A ON A.USERID = L.USERID'';

-- Installation step 165
EXEC sys.sp_executesql N''CREATE FUNCTION [dbo].[CalculateCheckDigitModulo10]
(
    @StringToCheck NVARCHAR(max)
)
RETURNS TABLE WITH SCHEMABINDING
RETURN
-- Calculate the check digit for a UPC
WITH Tally (n) AS
(
    SELECT TOP (LEN(@StringToCheck))
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL))
    -- 8,000 row tally table
    FROM (VALUES (0),(0),(0),(0),(0),(0),(0),(0)) a(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) b(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) c(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) d(n)
)
SELECT StringToCheck=@StringToCheck
    ,CheckDigit = (10 -
        SUM(CASE n%2 
            WHEN 1 THEN 3 
            ELSE 1 END * SUBSTRING(@StringToCheck, n, 1))
        % 10
        ) % 10 -- When check digit is 10 (remainder=0) use 0 as the check digit
FROM Tally;'';

-- Installation step 166
EXEC sys.sp_executesql N''-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE FUNCTION [dbo].[CZMST_get_sscc_func] 
(
	@seq_id int,
	@sscc_count int 
)
RETURNS nvarchar(20)
AS
BEGIN

		DECLARE @LV numeric(1,0);
		declare @GCP numeric(9,0);
		declare @GCP_count numeric(9,0);
				
		select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		from CZMST_SSCC_PARAMETERS as par
		where par.ID_SSCC = @seq_id
		
		declare @sscc nvarchar(20)
					
		set @sscc = ''''00''''
		set @sscc = @sscc + convert(nvarchar(1),@LV)
		set @sscc = @sscc + RIGHT(''''000000000'''' + convert(nvarchar(9),@GCP), @GCP_count)
		--set @sscc = @sscc + convert(nvarchar(9),@sscc_count)
		set @sscc = @sscc + RIGHT(''''000000000'''' + CONVERT(nvarchar(20), @sscc_count), (20-1-LEN(@sscc)))
		
		set @sscc = @sscc + convert(nvarchar(1),(select CheckDigit from CalculateCheckDigitModulo10(@sscc)))
		
		RETURN @sscc
		
END'';

-- Installation step 167
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data 
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_InventuraCompare_I123] 
(
	-- Add the parameters for the function here
		@CountEntries int,
		@V_0 bit,
		@V_1 bit,
		@V_2 bit,
		@V_3 bit,
		@V_4 bit
)
RETURNS
 @returnList TABLE 
 (
    [CountEntries] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[SKL_DESC] [nvarchar](40) NULL,
	[QUANTITY] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[Expirace] [datetime] NULL,
	[status] int null
 )
AS
BEGIN

----------------------------------------
-- Varianta 0  -------------------------
----------------------------------------

-- Tahle varianta v I4 neexistuje

IF @V_0 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		0 as status
		from FASK_Inventura_I123_Compare as I123
		left join FASK_Inventura_I4_Compare as I4 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		AND I123.CZ_SerNum_Track in (1,2)
		AND I4.ITEMNMBR is null
		AND I123.CountEntries = @CountEntries


				INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		0 as status
		from FASK_Inventura_I123_Compare as I123
		left join FASK_Inventura_I4_Compare as I4 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		where 1 = 1
		AND I123.CZ_SerNum_Track = 0
		AND I4.ITEMNMBR is null
		AND I123.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 1  -------------------------
----------------------------------------

IF @V_1 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		1 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I123.CZ_SerNum_Track in (1,2)
AND I4.QUANTITY = I123.QUANTITY
AND I4.CountEntries = @CountEntries

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		1 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
where 1 = 1
AND I123.CZ_SerNum_Track = 0
AND I4.QUANTITY = I123.QUANTITY
and I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 2  -------------------------
----------------------------------------

IF @V_2 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		2 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I123.CZ_SerNum_Track in (1,2)
AND I4.QUANTITY < I123.QUANTITY
and I4.CountEntries = @CountEntries

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		2 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
where 1 = 1
AND I123.CZ_SerNum_Track = 0
AND I4.QUANTITY < I123.QUANTITY
and I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 3  -------------------------
----------------------------------------

IF @V_3 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		3 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I123.CZ_SerNum_Track in (1,2)
AND I4.QUANTITY > I123.QUANTITY
and I4.CountEntries = @CountEntries

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		3 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
where 1 = 1
AND I123.CZ_SerNum_Track = 0
AND I4.QUANTITY > I123.QUANTITY
and I4.CountEntries = @CountEntries

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 4  -------------------------
----------------------------------------

-- Tato varianta pro I123 neexistuje

--IF @V_4 = 1
--	BEGIN 


--END

----------------------------------------
----------------------------------------
----------------------------------------

return 
END'';

-- Installation step 168
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data 
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_InventuraCompare_I4] 
(
	-- Add the parameters for the function here
		@CountEntries int,
		@V_0 bit,
		@V_1 bit,
		@V_2 bit,
		@V_3 bit,
		@V_4 bit
)
RETURNS
 @returnList TABLE 
 (
    [CountEntries] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[SKL_DESC] [nvarchar](40) NULL,
	[QUANTITY] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[Expirace] [datetime] NULL,
	[status] int null
 )
AS
BEGIN

----------------------------------------
-- Varianta 0  -------------------------
----------------------------------------

-- Tahle varianta v I4 neexistuje

--IF @V_0 = 1
--	BEGIN 
--	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 1  -------------------------
----------------------------------------

IF @V_1 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		1 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		AND I4.CZ_SerNum_Track in (1,2)
		AND I4.QUANTITY = I123.QUANTITY
		AND I4.CountEntries = @CountEntries


		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		1 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		where 1 = 1
		AND I4.CZ_SerNum_Track = 0
		AND I4.QUANTITY = I123.QUANTITY
		AND I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 2  -------------------------
----------------------------------------

IF @V_2 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		2 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		AND I4.CZ_SerNum_Track in (1,2)
		AND I4.QUANTITY < I123.QUANTITY
		and I4.CountEntries = @CountEntries

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		2 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		where 1 = 1
		AND I4.CZ_SerNum_Track = 0
		AND I4.QUANTITY < I123.QUANTITY
		and I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 3  -------------------------
----------------------------------------

IF @V_3 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		3 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		AND I4.CZ_SerNum_Track in (1,2)
		AND I4.QUANTITY > I123.QUANTITY
		and I4.CountEntries = @CountEntries

				INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		3 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		where 1 = 1
		AND I4.CZ_SerNum_Track = 0
		AND I4.QUANTITY > I123.QUANTITY
		and I4.CountEntries = @CountEntries

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 4  -------------------------
----------------------------------------

IF @V_4 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		4 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		and I4.CZ_SerNum_Track in (1,2)
		AND I123.QUANTITY is null
		and I4.CountEntries = @CountEntries

				INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I4.[CountEntries],
		I4.[ITEMNMBR],
		I4.[ITEMDESC],
		I4.[ITEMCODE],
		I4.[VNDITNUM],
		I4.[SKL_ID],
		I4.[SKL_DESC],
		I4.[QUANTITY],
		I4.[MJ],
		I4.[SERLNMBR],
		I4.[Expirace],
		4 as status
			from FASK_Inventura_I4_Compare as I4
		left join FASK_Inventura_I123_Compare as I123 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		where 1 = 1
		and I4.CZ_SerNum_Track = 0
		AND I123.QUANTITY is null
		and I4.CountEntries = @CountEntries

END

----------------------------------------
----------------------------------------
----------------------------------------

return 
END'';

-- Installation step 169
EXEC sys.sp_executesql N''CREATE FUNCTION [dbo].[FASK_ParseGS1] ( @stringToParse NVARCHAR(MAX))
RETURNS
 @returnList TABLE 
 (
	 [ORIGINAL] nvarchar(MAX)
	 ,[EAN] nvarchar(500)
	 ,[CISTAHMOTNOST] numeric(19,5)
	 ,[MNOZSTVI] int
	 ,[EXPIRACE] nvarchar(500)
	 ,[SARZE] nvarchar(500)
 )
AS
BEGIN

	declare 
		@id int
		,@name nvarchar(max)
		,@strGS1part nvarchar(max)
		,@strGS1partLength int
		,@strTmp nvarchar(max)

	declare @EAN nvarchar(30)
	declare @CISTAHMOTNOST nvarchar(30)
	declare @MNOZSTVI nvarchar(30)
	declare @EXPIRACE nvarchar(30)
	declare @SARZE nvarchar(30)

	--SELECT 
	--@EAN = Substring(Name,3,14)
	--, @CISTAHMOTNOST = Substring(Name,19, LEN(Name) - 18)
	--FROM dbo.splitstring(@stringToParse, CHAR(29)) where ID = 0


	--SELECT 
	--@EXPIRACE = Substring(Name,3,6)
	--, @SARZE = Substring(Name,11, LEN(Name) - 10 )
	--FROM dbo.splitstring(@stringToParse, CHAR(29)) where ID = 1

	-- JiS musi se to upravit ...
	--select * into #parts from dbo.splitstring(@stringToParse, CHAR(29))

	declare cParse cursor for
	Select p.ID, p.Name from dbo.splitstring(@stringToParse, CHAR(29)) p
	
	open cParse
	fetch next from cParse into @id, @name
	while @@FETCH_STATUS = 0
	begin
		set @strGS1part = @name
		set @strGS1partLength = LEN(@strGS1part)
		while LEN(@strGS1part) > 0
		begin
			set @strTmp = @strGS1part
			
			IF SUBSTRING(@strGS1part, 1, 2) = ''''02'''' --02 = GTIN n2 + n14
			BEGIN
				set @EAN = SUBSTRING(@strGS1part, 1 + 2, 14)					
				--nastavit zbytek ...
				set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 14, LEN(@strGS1part))	--celkem 16 znaku
				set @strGS1partLength = LEN(@strGS1part)
				CONTINUE --pokracuji od zacatku
			END
			
			IF SUBSTRING(@strGS1part, 1, 2) = ''''15''''	--15 = Expiration n2+n6
			BEGIN
				set @EXPIRACE = SUBSTRING(@strGS1part, 1+2, 6)				
				set @strGS1part = SUBSTRING(@strGS1part,1 + 2 + 6, LEN(@strGS1part))	--celkem 8 znaku
				set @strGS1partLength = LEN(@strGS1part)
				CONTINUE --pokracuji od zacatku
			END			
			
			IF SUBSTRING(@strGS1part, 1, 2) = ''''10''''	--10 = LOT/SARZE : n2+an..20 (promenny pocet, musi byt na konci, takze ctu vse az do konce, pripadne max 20 znaku a koncim)
			BEGIN
				set @SARZE = SUBSTRING(@strGS1part, 1+2, 20)
				--set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 20, LEN(@strGS1part))	--celkem max 20 znaku
				BREAK --koncim, protoze dal jiz nic byt nesmi ...
			END			

			IF SUBSTRING(@strGS1part, 1, 2) = ''''37''''	--37 = promenne mnozstvi n2+n..8 (promenny pocet, musi byt na konci, takze ctu vse az do konce, pripadne max 8 znaku a koncim)
			BEGIN
				set @MNOZSTVI = CONVERT(int,SUBSTRING(@strGS1part, 1+2, 8))
				--set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 8, LEN(@strGS1part))	--celkem max 8 znaku
				BREAK --koncim, protoze dal jiz nic byt nesmi ...
			END			
			
			IF SUBSTRING(@strGS1part, 1, 3) = ''''310''''	--310X = 310X čistá hmotnost / kg / n4+n6
			BEGIN
				declare 
					@descarka int
				set @descarka = CONVERT(int, SUBSTRING(@strGS1part, 1 + 3, 1))
				set @CISTAHMOTNOST = CONVERT(numeric(19,5), SUBSTRING(@strGS1part, 1 + 3 + 1, 6)) / @descarka
				set @strGS1part = SUBSTRING(@strGS1part, 1 + 3 + 1 + 6, LEN(@strGS1part))	--celkem max 10 znaku
				CONTINUE --pokracuji od zacatku
			END			
			
			-- Test jestli je jeste co parsovat ...			
			IF LEN(@strTmp) = LEN(@strGS1part)
			BEGIN
				BREAK
			END					
		end					
				
		fetch next from cParse into @id, @name
	end
	
	close cParse	

 INSERT INTO @returnList
 SELECT @stringToParse, @EAN , @CISTAHMOTNOST, @MNOZSTVI , @EXPIRACE , @SARZE
 
 RETURN
END'';

-- Installation step 170
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_prijem]
AS
SELECT
CONVERT(nvarchar(2), ''''PP'''') AS Type, 
'''''''' as DOC_ID,
'''''''' as  [ITEMTYPE],
pe.ITEMDESC, 
pi.PONUMBER AS DOCUMENT_NUMBER, 
pi.ITEMNMBR, 
pi.QTYSHPPD, 
pi.SKL_ID, 
pi.LOCNCODE, 
pi.SERLTNUM, 
pi.CountEntries, 
pi.CZ_CarKod, 
pi.USER_ID, 
pi.ID_TERMINAL,
dbo.fask_func_convert_to_datetime(pi.TIMEDONE, pi.DATEDONE) AS dateeve, 
pi.ITEMCODE, 
pi.VNDITNUM, 
pi.WEIGHT, 
pi.GUID, 
pi.VNDDOCNM
FROM
dbo.CZMST_PI AS pi LEFT OUTER JOIN
dbo.CZMST_PE AS pe ON pe.ITEMNMBR = pi.ITEMNMBR AND pe.ORD = pi.ORD AND pe.CountEntries = pi.CountEntries'';

-- Installation step 171
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_prodej]
AS
SELECT     
CONVERT(nvarchar(2), ''''V'''') as [Type], 
di.DOC_ID,
'''''''' as  [ITEMTYPE],
zbozi.ITEMDESC as ITEMDESC, 
CONVERT(nvarchar(30), '''''''') as [DOCUMENT_NUMBER], 
di.ITEMNMBR, 
di.QTYSHPPD, 
di.SKL_ID, 
di.LOCNCODE, 
di.SERLTNUM, 
di.CountEntries, 
di.CZ_CarKod, 
di.USER_ID, 
di.ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
di.ITEMCODE,
di.VNDITNUM,
di.WEIGHT,
--null as WEIGHT,
di.GUID,
convert(nvarchar(21),null) as VNDDOCNM
FROM         CZMST_DI di
LEFT JOIN FASK_ZASOBY zbozi on zbozi.itemnmbr = di.itemnmbr
;'';

-- Installation step 172
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_vydej]
AS
SELECT     
CONVERT(nvarchar(2), ''''P'''') as [Type],
'''''''' as DOC_ID,
se.ITEMTYPE as [ITEMTYPE],
se.ITEMDESC as ITEMDESC, 
si.SOPNUMBE as [DOCUMENT_NUMBER], 
si.ITEMNMBR, 
si.QTYSHPPD as QTYSHPPD, 
si.SKL_ID, si.LOCNCODE, 
si.SERLTNUM, 
si.CountEntries, 
si.CZ_CarKod, 
USER_ID, 
ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
si.ITEMCODE,
si.VNDITNUM,
si.WEIGHT,
si.GUID,
si.VNDDOCNM

FROM         dbo.CZMST_SI si
left join CZMST_SE se on se.ITEMNMBR = si.itemnmbr and se.ORD = si.ORD and se.CountEntries = si.CountEntries
;'';

-- Installation step 173
EXEC sys.sp_executesql N''/* View do prodeje */
CREATE VIEW [dbo].[fask_view_prodej_history]
AS
SELECT     
CONVERT(nvarchar(2), ''''V'''') as [Type],
di.DOC_ID, 
'''''''' as  [ITEMTYPE],
zbozi.ITEMDESC as ITEMDESC, 
CONVERT(nvarchar(30), '''''''') as [DOCUMENT_NUMBER], 
di.ITEMNMBR, 
di.QTYSHPPD, 
di.SKL_ID, 
di.LOCNCODE, 
di.SERLTNUM, 
di.CountEntries, 
di.CZ_CarKod, 
di.USER_ID, 
di.ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
di.ITEMCODE,
di.VNDITNUM,
di.WEIGHT as WEIGHT,
--null as WEIGHT,
di.GUID,
convert(nvarchar(21),null) as VNDDOCNM
FROM         CZMST_DI_HISTORY di
LEFT JOIN FASK_ZASOBY zbozi on zbozi.itemnmbr = di.itemnmbr
;'';

-- Installation step 174
EXEC sys.sp_executesql N''/* View do vydeje */
CREATE VIEW [dbo].[fask_view_vydej_history]
AS
SELECT     
CONVERT(nvarchar(2), ''''P'''') as [Type], 
'''''''' as DOC_ID,
se.ITEMTYPE as  [ITEMTYPE],
se.ITEMDESC as ITEMDESC, 
si.SOPNUMBE as [DOCUMENT_NUMBER], 
si.ITEMNMBR, 
si.QTYSHPPD as QTYSHPPD, 
si.SKL_ID, 
si.LOCNCODE, 
si.SERLTNUM, 
si.CountEntries, 
si.CZ_CarKod, 
USER_ID, ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
si.ITEMCODE,
si.VNDITNUM, 
si.WEIGHT,
si.GUID,
si.VNDDOCNM
FROM         dbo.CZMST_SI_history si
left join CZMST_SE_history se on se.ITEMNMBR = si.itemnmbr and se.ORD = si.ORD and se.CountEntries = si.CountEntries
;'';

-- Installation step 175
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_prijem_history]
AS
SELECT
CONVERT(nvarchar(2), ''''PP'''') AS Type, 
'''''''' as DOC_ID,
'''''''' as  [ITEMTYPE],
pe.ITEMDESC, 
pi.PONUMBER AS DOCUMENT_NUMBER, 
pi.ITEMNMBR, 
pi.QTYSHPPD, 
pi.SKL_ID, 
pi.LOCNCODE, 
pi.SERLTNUM, 
pi.CountEntries, 
pi.CZ_CarKod, 
pi.USER_ID, 
pi.ID_TERMINAL,
dbo.fask_func_convert_to_datetime(pi.TIMEDONE, pi.DATEDONE) AS dateeve, 
pi.ITEMCODE, 
pi.VNDITNUM, 
pi.WEIGHT, 
pi.GUID, 
pi.VNDDOCNM
FROM
dbo.CZMST_PI_HISTORY AS pi LEFT OUTER JOIN
dbo.CZMST_PE_HISTORY AS pe ON pe.ITEMNMBR = pi.ITEMNMBR AND pe.ORD = pi.ORD AND pe.CountEntries = pi.CountEntries'';

-- Installation step 176
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 5.6.2019
-- Description:	Dekoduje z retezce pozadovany gs1 ai hodnotu
-- =============================================
CREATE FUNCTION [dbo].[fask_GS1_AI_GET] 
(
	-- Add the parameters for the function here
	@ai nvarchar(max),
	@barcode nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(max)
	SET @Result = null

	IF (ltrim(rtrim(ISNULL(@barcode, ''''''''))) = '''''''')
	BEGIN
		Return @Result
	END

	-- Add the T-SQL statements to compute the return value here
	IF (@ai = ''''EAN'''')
	begin
		SELECT @Result = gs1.EAN from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = ''''EXPIRACE'''')
	begin
		SELECT @Result = gs1.EXPIRACE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = ''''SARZE'''')
	begin
		SELECT @Result = gs1.SARZE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = ''''VAHA'''')
	begin
		SELECT @Result = gs1.CISTAHMOTNOST from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = ''''MNOZSTVI'''')
	begin
		SELECT @Result = gs1.MNOZSTVI from FASK_ParseGS1(@barcode) gs1
	end

	-- Return the result of the function
	RETURN @Result

END'';

-- Installation step 177
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_pohyby_aktualni]
AS
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_vydej
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prodej
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_lokacni_mechanismus_pohyby
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_stav_naplneni_inventury
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prijem'';

-- Installation step 178
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_pohyby_archivni]
AS
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_vydej_history
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_prodej_history
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_prijem_history'';

-- Installation step 179
EXEC sys.sp_executesql N''CREATE VIEW [dbo].[fask_view_pohyby_aktualni_a_archivni]
AS
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_aktualni
UNION
SELECT        [Type], [DOC_ID], [ITEMTYPE], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_archivni'';

-- Installation step 180
EXEC sys.sp_executesql N''/****** Object:  Index [czmstpe_idx_prijemky_01]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [czmstpe_idx_prijemky_01] ON [dbo].[CZMST_PE]
(
	[QTYPACK] ASC
)
INCLUDE([CountEntries],[PONUMBER],[ITEMNMBR]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 181
EXEC sys.sp_executesql N''/****** Object:  Index [czmstpe_idx_prijemky_02]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [czmstpe_idx_prijemky_02] ON [dbo].[CZMST_PE]
(
	[QTYPACK] ASC
)
INCLUDE([CountEntries],[PONUMBER],[QTYSHPPD]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 182
EXEC sys.sp_executesql N''/****** Object:  Index [czmstpe_idx_prijemky_03]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [czmstpe_idx_prijemky_03] ON [dbo].[CZMST_PE]
(
	[CZ_Doslo] ASC
)
INCLUDE([CountEntries],[PONUMBER],[SKL_ID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 183
EXEC sys.sp_executesql N''/****** Object:  Index [czmstpe_idx_prijemky_04]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [czmstpe_idx_prijemky_04] ON [dbo].[CZMST_PE]
(
	[CountEntries] ASC,
	[PONUMBER] ASC,
	[QTYPACK] ASC
)
INCLUDE([QTYSHPPD]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 184
EXEC sys.sp_executesql N''/****** Object:  Index [idx_fask_czmst_se_getvydejky]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [idx_fask_czmst_se_getvydejky] ON [dbo].[CZMST_SE]
(
	[QTYPACK] ASC,
	[CZ_Doslo] ASC
)
INCLUDE([CountEntries],[SOPNUMBE],[ITEMTYPE],[SKL_ID],[QTYSHPPD]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 185
EXEC sys.sp_executesql N''/****** Object:  Index [idx_fask_czmst_se_getvydejky2]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [idx_fask_czmst_se_getvydejky2] ON [dbo].[CZMST_SE]
(
	[CZ_Doslo] ASC
)
INCLUDE([CountEntries],[SOPNUMBE],[ITEMTYPE],[SKL_ID],[PRIORITY]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 186
EXEC sys.sp_executesql N''/****** Object:  Index [IX_FASK_CZMST_Servis_ZdrojPohyb_Guid]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_Servis_ZdrojPohyb_Guid] ON [dbo].[CZMST_Servis_ZdrojPohyb]
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 187
EXEC sys.sp_executesql N''/****** Object:  Index [IX_FASK_CZMST_SkladLokace_StavPohyb_Guid]    Script Date: 12.06.2026 11:00:44 ******/
CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_SkladLokace_StavPohyb_Guid] ON [dbo].[CZMST_SkladLokace_StavPohyb]
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]'';

-- Installation step 188
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((0)) FOR [TMFrom]'';

-- Installation step 189
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((1)) FOR [Production]'';

-- Installation step 190
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N''''Nezadáno'''') FOR [Description]'';

-- Installation step 191
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (getdate()) FOR [DateCreated]'';

-- Installation step 192
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N''''N'''') FOR [State]'';

-- Installation step 193
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT ((3)) FOR [Priority]'';

-- Installation step 194
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL_UZIV] ADD  DEFAULT (N''''N'''') FOR [State]'';

-- Installation step 195
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_CountEntries] ADD  DEFAULT ((0)) FOR [BLOCKED]'';

-- Installation step 196
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DI] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 197
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 198
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DI_RFID] ADD  DEFAULT (getdate()) FOR [Created_S]'';

-- Installation step 199
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka] ADD  DEFAULT ((0)) FOR [Rozpracovano]'';

-- Installation step 200
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka] ADD  DEFAULT (getdate()) FOR [DateCreated]'';

-- Installation step 201
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY] ADD  DEFAULT ((0)) FOR [Rozpracovano]'';

-- Installation step 202
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Polozky] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 203
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 204
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka] ADD  DEFAULT ((0)) FOR [Rozpracovano]'';

-- Installation step 205
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka] ADD  DEFAULT (getdate()) FOR [DateCreated]'';

-- Installation step 206
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka_HISTORY] ADD  DEFAULT ((0)) FOR [Rozpracovano]'';

-- Installation step 207
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Polozky] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 208
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Polozky_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 209
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_I1] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 210
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_I1] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 211
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_I1] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 212
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_I4] ADD  DEFAULT ((0)) FOR [O_Checked]'';

-- Installation step 213
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_METAINFO] ADD  DEFAULT (getdate()) FOR [Updated]'';

-- Installation step 214
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ADD  DEFAULT (N'''''''') FOR [SERLTNUM]'';

-- Installation step 215
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 216
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 217
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 218
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT (N'''''''') FOR [SERLTNUM]'';

-- Installation step 219
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 220
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 221
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 222
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PEH] ADD  DEFAULT ('''''''') FOR [DOKLTYPE]'';

-- Installation step 223
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PEH_HISTORY] ADD  DEFAULT ('''''''') FOR [DOKLTYPE]'';

-- Installation step 224
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_RFID_ITEMS] ADD  DEFAULT ((0)) FOR [SEQUENCENMBR]'';

-- Installation step 225
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS] ADD  DEFAULT (getdate()) FOR [Created_S]'';

-- Installation step 226
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 227
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ((3)) FOR [PRIORITY]'';

-- Installation step 228
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 229
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 230
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 231
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT (getdate()) FOR [Realization_Start]'';

-- Installation step 232
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT (getdate()) FOR [Realization_Stop]'';

-- Installation step 233
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 234
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 235
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ((3)) FOR [PRIORITY]'';

-- Installation step 236
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 237
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 238
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 239
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 240
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_SN] ADD  DEFAULT ((1)) FOR [QTY]'';

-- Installation step 241
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_SN_HISTORY] ADD  CONSTRAINT [DF__CZMST_SE_SN__QTY__40058253]  DEFAULT ((1)) FOR [QTY]'';

-- Installation step 242
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Servis_Cinnost] ADD  DEFAULT ((1)) FOR [Mandatory]'';

-- Installation step 243
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT ((0)) FOR [Rozpracovano]'';

-- Installation step 244
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT (getdate()) FOR [DateCreated]'';

-- Installation step 245
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Servis_ZdrojPohyb] ADD  DEFAULT (getdate()) FOR [dateeveS]'';

-- Installation step 246
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 247
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 248
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI_RFID] ADD  DEFAULT (getdate()) FOR [Created_S]'';

-- Installation step 249
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment] ADD  DEFAULT (getdate()) FOR [DateCreated]'';

-- Installation step 250
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SkladLokace_Stav] ADD  DEFAULT ((0)) FOR [QTYSHPPD_DEF]'';

-- Installation step 251
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SkladLokace_Stav] ADD  DEFAULT ((0)) FOR [QTY_OWNER]'';

-- Installation step 252
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_TERMINAL_DEFINITION] ADD  DEFAULT (''''-'''') FOR [DB_TYPE]'';

-- Installation step 253
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ('''''''') FOR [odb_misto]'';

-- Installation step 254
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ('''''''') FOR [odb_ulice]'';

-- Installation step 255
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ('''''''') FOR [odb_cisloOr]'';

-- Installation step 256
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ('''''''') FOR [odb_psc]'';

-- Installation step 257
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ('''''''') FOR [odb_dic]'';

-- Installation step 258
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ((0)) FOR [odb_Odberatel]'';

-- Installation step 259
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST090] ADD  DEFAULT ((0)) FOR [odb_Dodavatel]'';

-- Installation step 260
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ('''''''') FOR [doc_id2]'';

-- Installation step 261
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ('''''''') FOR [LOCNCODE]'';

-- Installation step 262
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_odb]'';

-- Installation step 263
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_str]'';

-- Installation step 264
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_prac]'';

-- Installation step 265
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_mn2sn]'';

-- Installation step 266
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_disp]'';

-- Installation step 267
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_disp_dest]'';

-- Installation step 268
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_palety]'';

-- Installation step 269
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_paleta_id]'';

-- Installation step 270
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_zakazka_id]'';

-- Installation step 271
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_tisk]'';

-- Installation step 272
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_prevod_sklad]'';

-- Installation step 273
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_FIFO_FEFO_check]'';

-- Installation step 274
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((1)) FOR [cfg_sarze_ONOFF]'';

-- Installation step 275
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((1)) FOR [cfg_sn_ONOFF]'';

-- Installation step 276
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((1)) FOR [cfg_expirace_ONOFF]'';

-- Installation step 277
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST092] ADD  DEFAULT ((0)) FOR [cfg_AttributeToSN_ONOFF]'';

-- Installation step 278
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST093] ADD  DEFAULT ('''''''') FOR [skl_typ]'';

-- Installation step 279
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST097] ADD  DEFAULT ((0)) FOR [mena_hlavni]'';

-- Installation step 280
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST097] ADD  DEFAULT ((0)) FOR [mena_kurz]'';

-- Installation step 281
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPH] ADD  DEFAULT (''''1'''') FOR [CountEntries]'';

-- Installation step 282
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPH] ADD  DEFAULT ('''''''') FOR [SOPTYPE]'';

-- Installation step 283
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPH] ADD  DEFAULT ((1)) FOR [Active]'';

-- Installation step 284
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT (''''1'''') FOR [CountEntries]'';

-- Installation step 285
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 286
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [ORD]'';

-- Installation step 287
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [QTYDOKON]'';

-- Installation step 288
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [TIMEMODE]'';

-- Installation step 289
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((1)) FOR [BarcodeT]'';

-- Installation step 290
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]'';

-- Installation step 291
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]'';

-- Installation step 292
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [CZ_REZ3_Track]'';

-- Installation step 293
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [CZ_REZ4_Track]'';

-- Installation step 294
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPP] ADD  DEFAULT ((0)) FOR [CZ_REZ5_Track]'';

-- Installation step 295
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[EXTERNAL_POST_LOG] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]'';

-- Installation step 296
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_AfterProcess_Log] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]'';

-- Installation step 297
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events] ADD  CONSTRAINT [DF_Events_reportType]  DEFAULT (N''''D'''') FOR [reportType]'';

-- Installation step 298
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events] ADD  DEFAULT ((0)) FOR [QTYPACK]'';

-- Installation step 299
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events] ADD  DEFAULT ((1)) FOR [BarcodeT]'';

-- Installation step 300
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events_HISTORY] ADD  DEFAULT ((0)) FOR [QTYPACK]'';

-- Installation step 301
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events_HISTORY] ADD  DEFAULT ((1)) FOR [BarcodeT]'';

-- Installation step 302
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_EventsErr] ADD  DEFAULT ((0)) FOR [qty]'';

-- Installation step 303
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_EventsErr] ADD  DEFAULT ((0)) FOR [qtyReal]'';

-- Installation step 304
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_EventsErr] ADD  DEFAULT ((0)) FOR [QTYPACK]'';

-- Installation step 305
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_EventsErr] ADD  DEFAULT ((1)) FOR [BarcodeT]'';

-- Installation step 306
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Logins] ADD  DEFAULT (getdate()) FOR [CREATED]'';

-- Installation step 307
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Logins] ADD  DEFAULT (getdate()) FOR [VALIDFROM]'';

-- Installation step 308
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_VOLNA]  DEFAULT ((0)) FOR [VOLNA]'';

-- Installation step 309
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_START]  DEFAULT ((0)) FOR [START]'';

-- Installation step 310
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations] ADD  CONSTRAINT [DF_FASK_Operations_END]  DEFAULT ((0)) FOR [KONEC]'';

-- Installation step 311
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_PLANOVANI_PARAMS] ADD  DEFAULT ((0)) FOR [Value]'';

-- Installation step 312
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_PLANOVANI_PARAMS_Name] ADD  DEFAULT (getdate()) FOR [dateedit]'';

-- Installation step 313
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_RADY] ADD  DEFAULT ((1)) FOR [Import_Doklad_IS]'';

-- Installation step 314
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Vyroba_PVH] ADD  DEFAULT ('''''''') FOR [SOPTYPE]'';

-- Installation step 315
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Vyroba_PVH] ADD  DEFAULT ((1)) FOR [Active]'';

-- Installation step 316
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Vyroba_PVP] ADD  DEFAULT ((0)) FOR [VP_PRPS]'';

-- Installation step 317
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  CONSTRAINT [DF__FASK_ZASOBY__DMJ__20CCCE1C]  DEFAULT ('''''''') FOR [DMJ]'';

-- Installation step 318
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__21C0F255]  DEFAULT ((0)) FOR [CZ_Rez1_Track]'';

-- Installation step 319
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__22B5168E]  DEFAULT ((0)) FOR [CZ_Rez2_Track]'';

-- Installation step 320
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__23A93AC7]  DEFAULT ((0)) FOR [CZ_Rez3_Track]'';

-- Installation step 321
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__249D5F00]  DEFAULT ((0)) FOR [CZ_Rez4_Track]'';

-- Installation step 322
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY] ADD  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 323
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC] ADD  CONSTRAINT [DF_FASK_ZASOBY_IMPORT_POHODA_SKzNC_DefDod]  DEFAULT ((0)) FOR [DefDod]'';

-- Installation step 324
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_IMPORT_POHODA_SKzNC] ADD  CONSTRAINT [DF_FASK_ZASOBY_IMPORT_POHODA_SKzNC_Status_Err]  DEFAULT ((0)) FOR [Status_Err]'';

-- Installation step 325
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFVTS]  DEFAULT ((0)) FOR [VPrFVTS]'';

-- Installation step 326
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFPTS]  DEFAULT ((0)) FOR [VPrFPTS]'';

-- Installation step 327
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFDTS]  DEFAULT ((0)) FOR [VPrFDTS]'';

-- Installation step 328
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFITS]  DEFAULT ((0)) FOR [VPrFITS]'';

-- Installation step 329
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFXTS]  DEFAULT ((0)) FOR [VPrFXTS]'';

-- Installation step 330
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ('''''''') FOR [KOD]'';

-- Installation step 331
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ('''''''') FOR [NAZEV]'';

-- Installation step 332
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ('''''''') FOR [KOD_LOK]'';

-- Installation step 333
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ((0)) FOR [STAV]'';

-- Installation step 334
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ((0)) FOR [CENA]'';

-- Installation step 335
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ((0)) FOR [CENA_ZUST]'';

-- Installation step 336
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT ((0)) FOR [REZERVACE]'';

-- Installation step 337
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_STAV] ADD  DEFAULT (getdate()) FOR [TS]'';

-- Installation step 338
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASOBY__DMJ__20CCCE1C_zal]  DEFAULT ('''''''') FOR [DMJ]'';

-- Installation step 339
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__21C0F255_zal]  DEFAULT ((0)) FOR [CZ_Rez1_Track]'';

-- Installation step 340
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__22B5168E_zal]  DEFAULT ((0)) FOR [CZ_Rez2_Track]'';

-- Installation step 341
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__23A93AC7_zal]  DEFAULT ((0)) FOR [CZ_Rez3_Track]'';

-- Installation step 342
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Re__249D5F00_zal]  DEFAULT ((0)) FOR [CZ_Rez4_Track]'';

-- Installation step 343
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_ZASOBY_zal_20260129] ADD  CONSTRAINT [DF__FASK_ZASO__CZ_Ex__3552E9B6_zal]  DEFAULT ((0)) FOR [CZ_Expirace_Track]'';

-- Installation step 344
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[MachinesDefinition] ADD  DEFAULT ((1)) FOR [ID_group]'';

-- Installation step 345
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[MachineStateSet] ADD  DEFAULT ((1)) FOR [ID_group]'';

-- Installation step 346
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[MachineStateSetHistory] ADD  DEFAULT ((1)) FOR [ID_group]'';

-- Installation step 347
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 348
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production] ADD  DEFAULT ((0)) FOR [TIMEMODE]'';

-- Installation step 349
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production_HISTORY] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 350
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production_HISTORY] ADD  DEFAULT ((0)) FOR [TIMEMODE]'';

-- Installation step 351
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production_SN] ADD  DEFAULT ((1)) FOR [QTY]'';

-- Installation step 352
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production_Sources] ADD  DEFAULT ('''''''') FOR [ITEMTYPE]'';

-- Installation step 353
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[Production_Sources] ADD  DEFAULT ((0)) FOR [PRINTED]'';

-- Installation step 354
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Machines]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Machines_FASK_MachineType] FOREIGN KEY([machinetype])
REFERENCES [dbo].[FASK_MachineType] ([machinetype])'';

-- Installation step 355
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Machines] CHECK CONSTRAINT [FK_FASK_Machines_FASK_MachineType]'';

-- Installation step 356
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_FASK_MachineType] FOREIGN KEY([machinetype])
REFERENCES [dbo].[FASK_MachineType] ([machinetype])'';

-- Installation step 357
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations] CHECK CONSTRAINT [FK_FASK_Operations_FASK_MachineType]'';

-- Installation step 358
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations_Next]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations] FOREIGN KEY([machinetype], [IDO])
REFERENCES [dbo].[FASK_Operations] ([machinetype], [IDO])'';

-- Installation step 359
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations_Next] CHECK CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations]'';

-- Installation step 360
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations_Next]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations1] FOREIGN KEY([machinetype], [IDO_NEXT])
REFERENCES [dbo].[FASK_Operations] ([machinetype], [IDO])'';

-- Installation step 361
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Operations_Next] CHECK CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations1]'';

-- Installation step 362
EXEC sys.sp_executesql N''-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_get_sscc_sequence_proc] 
 @sequence int  ,
 @count int , 
 @terminal int ,
 @endSSCC int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
	BEGIN TRANSACTION
	BEGIN TRY
	
		DECLARE @sequence_count int;
		
		--DECLARE @LV numeric(1,0);
		--declare @GCP numeric(9,0);
		--declare @GCP_count numeric(9,0);
		-- return an error if sequence does not exist
		-- so we will know if someone truncates the table
		
		-- set @sequence_count = -1
		
		select @sequence_count = seq.sequence_count  
		from CZMST_SSCC_SEQUENCE as seq  
		where seq_id = @sequence
		
		IF @sequence_count is NULL 
		BEGIN
			set @sequence_count = 0
			insert into CZMST_SSCC_SEQUENCE (seq_id, sequence_count) values (@sequence, @sequence_count)
		END		
		
		--select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		--from CZMST_SSCC_PARAMETERS as par
		--where par.ID_SSCC = @sequence
		
		--SET @endSSCC = ''''00'''' + convert(nvarchar(1),@LV) + convert(nvarchar(9),@GCP) + convert(nvarchar(9),@sequence_count) ;
		SET @endSSCC = @sequence_count + @count
	
		UPDATE CZMST_SSCC_SEQUENCE
		SET    sequence_count = @endSSCC
		WHERE  seq_id = @sequence

		COMMIT TRANSACTION

	RETURN @endSSCC		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @endSSCC = NULL
		RETURN @endSSCC
	END CATCH
END'';

-- Installation step 363
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky CZMST_I4
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_I4_insert]

	@CountEntries [int],
	@CE_Orig [int],
	@ITEMNMBR [nvarchar](40),
	@CZ_CarKod [nvarchar](70),
	@LOCNCODE [nvarchar](11),
	@SKL_ID [nvarchar](20),
	@VNDITNUM [nvarchar](60),
	@MJ [nvarchar](10),
	@QUANTITY [numeric](19, 5),
	@QUANTITYMJ [numeric](19, 5),
	@QTYPACK [numeric](19, 5),
	@SERLNMBR [nvarchar](50),
	@DATEDONE [nvarchar](8),
	@TIMEDONE [nvarchar](6),
	@USERID [int],
	@DEX_ROW_ID [int],
	@GUID [uniqueidentifier],
	@O_Checked [bit],
	@INPUT_MODE [tinyint],
	@ID_TERMINAL [int],
	@ITEMCODE [nvarchar](70),
	@REZ_1 [nvarchar](50),
	@REZ_2 [nvarchar](50),
	@WEIGHT [numeric](19, 5),
	@Expirace [datetime]



AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_I4]
           ([CountEntries]
           ,[CE_Orig]
           ,[ITEMNMBR]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[VNDITNUM]
           ,[MJ]
           ,[QUANTITY]
           ,[QUANTITYMJ]
           ,[QTYPACK]
           ,[SERLNMBR]
           ,[DATEDONE]
           ,[TIMEDONE]
           ,[USERID]
           ,[GUID]
           ,[O_Checked]
           ,[INPUT_MODE]
           ,[ID_TERMINAL]
           ,[ITEMCODE]
           ,[REZ_1]
           ,[REZ_2]
           ,[WEIGHT]
           ,[Expirace])
     VALUES
           (
	@CountEntries,
	@CE_Orig,
	@ITEMNMBR,
	@CZ_CarKod,
	@LOCNCODE,
	@SKL_ID,
	@VNDITNUM,
	@MJ,
	@QUANTITY,
	@QUANTITYMJ,
	@QTYPACK,
	@SERLNMBR,
	@DATEDONE,
	@TIMEDONE,
	@USERID,
	--@DEX_ROW_ID IDENTITY(1,1),
	@GUID,
	@O_Checked,
	@INPUT_MODE,
	@ID_TERMINAL,
	@ITEMCODE,
	@REZ_1,
	@REZ_2,
	@WEIGHT,
	@Expirace

		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
		BEGIN
			--Chyba nenastala => potvrzeni transakce
			PRINT ''''ZAZNAM BYL USPESNE VLOZEN''''
			COMMIT TRAN
		END
		ELSE
		BEGIN
			--Nastala chyba zruseni transakce
			PRINT ''''NASTALA CHYBA PRI VKLADANI HODNOT DO DB''''
			ROLLBACK TRAN
		END
END


SET ANSI_NULLS ON'';

-- Installation step 364
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 18.4.2016
-- Description:	Generuje nasledujici cislo davky pro modul transakcne
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Next_CountEntries]
 @module nvarchar(20),
 @CountEntries int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
		DECLARE @ce int;
				
		select @ce = CountEntries
		from CZMST_CountEntries
		where TBL = @module

		IF @ce is NULL 
		BEGIN
			SET @CountEntries = 1
			INSERT INTO CZMST_CountEntries (TBL, CountEntries) VALUES (@module, @CountEntries)
		END
		ELSE
		BEGIN
			SET @CountEntries = @ce + 1
			UPDATE CZMST_CountEntries
			SET    CountEntries = @CountEntries
			WHERE  TBL = @module
		END
END'';

-- Installation step 365
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 5.3. 2023
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky CZMST_PI
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_PI_insert]

 @CountEntries [int] ,
 @PONUMBER [nvarchar](30) ,
 @ORD [int] ,
 @ITEMNMBR [nvarchar](40) ,
 @VNDDOCNM [nvarchar](21) ,
 @VNDITNUM [nvarchar](60) ,
 @SKL_ID [nvarchar](20) ,
 @LOCNCODE [nvarchar](11) ,
 @MJ [nvarchar](10) ,
 @QTYSHPPD [numeric](19, 5) ,
 @QTYSHPPDMJ [numeric](19, 5) ,
 @QTYPACK [numeric](19, 5) ,
 @SERLTNUM [nvarchar](50) ,
 @KOD_SW [nvarchar](11) ,
 @DAT_VYROBY [nvarchar](11) ,
 @DATEDONE [nvarchar](8) ,
 @TIMEDONE [nvarchar](6) ,
 @CZ_CarKod [nvarchar](70) ,
 @REZ_1 [nvarchar](50) ,
 @REZ_2 [nvarchar](50) ,
 @USER_ID [int] ,
 @DEX_ROW_ID [int] ,
 @GUID [uniqueidentifier] ,
 @INPUT_MODE [tinyint] ,
 @ID_TERMINAL [int] ,
 @WEIGHT [numeric](19, 5) ,
 @NMBRPAL [nvarchar](50) ,
 @TYPEPAL [nvarchar](10) ,
 @ITEMCODE [nvarchar](70) ,
 @Expirace [datetime] ,
 @AttributeToSN [nvarchar](50)



AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_PI]
           ([CountEntries]
           ,[PONUMBER]
           ,[ORD]
           ,[ITEMNMBR]
           ,[VNDDOCNM]
           ,[VNDITNUM]
           ,[SKL_ID]
           ,[LOCNCODE]
           ,[MJ]
           ,[QTYSHPPD]
           ,[QTYSHPPDMJ]
           ,[QTYPACK]
           ,[SERLTNUM]
           ,[KOD_SW]
           ,[DAT_VYROBY]
           ,[DATEDONE]
           ,[TIMEDONE]
           ,[CZ_CarKod]
           ,[REZ_1]
           ,[REZ_2]
           ,[USER_ID]
           ,[GUID]
           ,[INPUT_MODE]
           ,[ID_TERMINAL]
           ,[WEIGHT]
           ,[NMBRPAL]
           ,[TYPEPAL]
           ,[ITEMCODE]
           ,[Expirace]
           ,[AttributeToSN])
     VALUES
           (
	@CountEntries,
@PONUMBER,
@ORD,
@ITEMNMBR,
@VNDDOCNM,
@VNDITNUM,
@SKL_ID,
@LOCNCODE,
@MJ,
@QTYSHPPD,
@QTYSHPPDMJ,
@QTYPACK,
@SERLTNUM,
@KOD_SW,
@DAT_VYROBY,
@DATEDONE,
@TIMEDONE,
@CZ_CarKod,
@REZ_1,
@REZ_2,
@USER_ID,
---@DEX_ROW_ID,
@GUID,
@INPUT_MODE,
@ID_TERMINAL,
@WEIGHT,
@NMBRPAL,
@TYPEPAL,
@ITEMCODE,
@Expirace,
@AttributeToSN


		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
		BEGIN
			--Chyba nenastala => potvrzeni transakce
			PRINT ''''ZAZNAM BYL USPESNE VLOZEN''''
			COMMIT TRAN
		END
		ELSE
		BEGIN
			--Nastala chyba zruseni transakce
			PRINT ''''NASTALA CHYBA PRI VKLADANI HODNOT DO DB''''
			ROLLBACK TRAN
		END
END

SET ANSI_NULLS ON'';

-- Installation step 366
EXEC sys.sp_executesql N''-- ========================================================================================================================================================
--
--
--												MaR 22.4.2024 Vytvorene trigry pro android
--
--
--																Začátek
-- ========================================================================================================================================================


-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	Vytvorene trigry pro android insert z CZMST_PE do tabulky CZMST_SE
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_SE_insert]

@CountEntries [int],
@SOPNUMBE [nvarchar](30),
@ITEMNMBR [nvarchar](40),
@ITEMTYPE [nvarchar](11),
@ITEMDESC [nvarchar](100),
@VNDDOCNM [nvarchar](21),
@VNDITNUM [nvarchar](60),
@ORD [int],
@CZ_CarKod [nvarchar](70),
@SKL_ID [nvarchar](20),
@LOCNCODE [nvarchar](11),
@MJ [nvarchar](10),
@QTYSHPPD [numeric](19, 5),
@QTYPACK [numeric](19, 5),
@CZ_DatVyr_Track [tinyint],
@CZ_DatVyr_Delka [smallint],
@CZ_SerNum_Track [tinyint],
@CZ_SerNum_Delka [smallint],
@CZ_SW_Track [tinyint],
@CZ_SW_Delka [smallint],
@CZ_Doslo [tinyint],
@Note [nvarchar](100),
@TYPEPAL [nvarchar](10),
@QTYPAL [numeric](19, 5),
@PRIORITY [tinyint],
@PRINTED [tinyint],
@USERID [int],
@DEX_ROW_ID [int],
@CZ_REZ1_Track [tinyint],
@CZ_REZ2_Track [tinyint],
@ITEMCODE [nvarchar](70),
@WEIGHT [numeric](19, 5),
@Realization_Start [datetime],
@Realization_Stop [datetime],
@CZ_Expirace_Track [tinyint]


AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[CZMST_SE]
           ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMDESC],[VNDDOCNM],[VNDITNUM],[ORD],[CZ_CarKod],[SKL_ID],[LOCNCODE],[MJ],[QTYSHPPD],[QTYPACK],[CZ_DatVyr_Track],[CZ_DatVyr_Delka]
           ,[CZ_SerNum_Track],[CZ_SerNum_Delka],[CZ_SW_Track],[CZ_SW_Delka],[CZ_Doslo],[Note],[TYPEPAL],[QTYPAL],[PRIORITY],[PRINTED],[USERID],[CZ_REZ1_Track],[CZ_REZ2_Track],[ITEMCODE]
           ,[WEIGHT],[Realization_Start],[Realization_Stop],[CZ_Expirace_Track])
     VALUES
           (
        @CountEntries,
        @SOPNUMBE,
        @ITEMNMBR,
        @ITEMTYPE,
        @ITEMDESC,
        @VNDDOCNM,
        @VNDITNUM,
        @ORD,
        @CZ_CarKod,
        @SKL_ID,
        @LOCNCODE,
        @MJ,
        @QTYSHPPD,
        @QTYPACK,
        @CZ_DatVyr_Track,
        @CZ_DatVyr_Delka,
        @CZ_SerNum_Track,
        @CZ_SerNum_Delka,
        @CZ_SW_Track,
        @CZ_SW_Delka,
        @CZ_Doslo,
        @Note,
        @TYPEPAL,
        @QTYPAL,
        @PRIORITY,
        @PRINTED,
        @USERID,
        --@DEX_ROW_ID,
        @CZ_REZ1_Track,
        @CZ_REZ2_Track,
        @ITEMCODE,
        @WEIGHT,
        @Realization_Start,
        @Realization_Stop,
        @CZ_Expirace_Track
		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
		BEGIN
			--Chyba nenastala => potvrzeni transakce
			PRINT ''''ZAZNAM BYL USPESNE VLOZEN''''
			COMMIT TRAN
		END
		ELSE
		BEGIN
			--Nastala chyba zruseni transakce
			PRINT ''''NASTALA CHYBA PRI VKLADANI HODNOT DO DB''''
			ROLLBACK TRAN
		END
END



SET ANSI_NULLS ON'';

-- Installation step 367
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 22.4.2020
-- Description:	Autentifikace operace, přeneseno z colorprofi
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Verify_Operation] 
	-- Add the parameters for the stored procedure here
	@operation nvarchar(20),	-- operation to verify
	@pwdhash nvarchar(50),		-- pwd hash
	@verified bit OUTPUT		-- 0 = not verified, 1=verified
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	set @verified = 0

	IF EXISTS(SELECT TOP 1 1 FROM CZMST_TASK_VERIFY WHERE taskname=@operation and verify_pwd=@pwdhash) BEGIN
		set @verified = 1
		return
	end

	Return 0
END'';

-- Installation step 368
EXEC sys.sp_executesql N''--IF OBJECT_ID(''''dbo.FASK_AfterProcess_2P'''', ''''P'''') IS NOT NULL
--    DROP PROCEDURE dbo.FASK_AfterProcess_2P
--GO

CREATE   PROCEDURE [dbo].[FASK_AfterProcess_2P]
    @table        NVARCHAR(20),
    @countentries NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @cnt INT = TRY_CONVERT(INT, @countentries);
        DECLARE @rowCount INT = 0;
        DECLARE @message NVARCHAR(400);

        IF @cnt IS NULL
            THROW 50001, ''''Parametr @countentries není platné číslo.'''', 1;

        IF OBJECT_ID(''''dbo.FASK_AfterProcess_Log'''', ''''U'''') IS NULL
        BEGIN
            CREATE TABLE dbo.FASK_AfterProcess_Log
            (
                ID           INT IDENTITY(1,1) PRIMARY KEY,
                TableName    NVARCHAR(50)  NOT NULL,
                CountEntries INT           NOT NULL,
                ItemType     NVARCHAR(20)  NULL,
                MessageText  NVARCHAR(400) NULL,
                CreatedAt    DATETIME2(0)  NOT NULL DEFAULT SYSDATETIME()
            );
        END

        IF @table = N''''CZMST_SI''''
        BEGIN
            SELECT @rowCount = COUNT(*)
            FROM dbo.CZMST_SI
            WHERE CountEntries = @cnt;

            IF @rowCount = 0
                THROW 50002, ''''Pro danou dávku neexistují data v CZMST_SI.'''', 1;
        END
        ELSE IF @table = N''''CZMST_DI''''
        BEGIN
            SELECT @rowCount = COUNT(*)
            FROM dbo.CZMST_DI
            WHERE CountEntries = @cnt;

            IF @rowCount = 0
                THROW 50003, ''''Pro danou dávku neexistují data v CZMST_DI.'''', 1;
        END
        ELSE
        BEGIN
            THROW 50004, ''''Nepodporovaná tabulka v @table.'''', 1;
        END

        SET @message = N''''POST akce 2P spuštěna, počet záznamů v dávce: '''' + CAST(@rowCount AS NVARCHAR(20));

        INSERT INTO dbo.FASK_AfterProcess_Log
        (
            TableName,
            CountEntries,
            ItemType,
            MessageText
        )
        VALUES
        (
            @table,
            @cnt,
            NULL,
            @message
        );

        -- sem můžeš doplnit další business logiku

    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();

        RAISERROR(''''FASK_AfterProcess_2P failed. %s'''', 16, 1, @ErrMsg);
        RETURN;
    END CATCH
END'';

-- Installation step 369
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_AfterProcess_2P_CS_Vedlejsi]
    @table        NVARCHAR(20),
    @countentries NVARCHAR(20)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @cnt INT = TRY_CONVERT(INT, @countentries);
    DECLARE @sql NVARCHAR(MAX);

    IF @cnt IS NULL
        THROW 50101, ''''Parametr @countentries není platné číslo.'''', 1;

    IF @table <> N''''CZMST_DI''''
        THROW 50102, ''''Tato procedura aktuálně podporuje pouze tabulku CZMST_DI.'''', 1;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.CZMST_DI
        WHERE CountEntries = @cnt
    )
        THROW 50103, ''''Pro danou dávku neexistují data v dbo.CZMST_DI.'''', 1;


		--Logovani do nasi tabulky START
IF OBJECT_ID(''''dbo.EXTERNAL_POST_LOG'''', ''''U'''') IS NULL
BEGIN
    CREATE TABLE dbo.EXTERNAL_POST_LOG
    (
        ID           INT IDENTITY(1,1) PRIMARY KEY,
        TableName    NVARCHAR(50) NOT NULL,
        CountEntries INT NOT NULL,
        ItemType     NVARCHAR(20) NULL,
        CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSDATETIME()
    );
END;

INSERT INTO dbo.EXTERNAL_POST_LOG
(
    TableName,
    CountEntries,
    ItemType,
    CreatedAt
)
VALUES
(
    @table,
    @cnt,
    NULL,
    SYSDATETIME()
);

		--Logovani do nasi tabulky END


    ;WITH D AS
    (
        SELECT
            DI.CountEntries,
            DI.VNDITNUM,
            DI.CZ_CarKod,
            DI.ODB_ID,
            DI.STR_ID,
            DI.DOC_ID,
            DI.DOC_ID2,
            DI.SKL_ID,
            DI.PRAC_ID,
            DI.ITEMNMBR,
            DI.ITEMCODE,
            DI.LOCNCODE,
            DI.MJ,
            DI.QTYSHPPD,
            DI.QTYSHPPDMJ,
            DI.QTYPACK,
            DI.SERLTNUM,
            DI.TAXAMPIE,
            DI.AMOUNPIE,
            DI.WITHTAX,
            DI.PRICEX,
            DI.mena_ID,
            DI.TAXAMPIEM,
            DI.AMOUNPIEM,
            DI.mena_IDM,
            DI.REZ_1,
            DI.REZ_2,
            DI.REZ_3,
            DI.REZ_4,
            DI.USER_ID,
            DI.DATEDONE,
            DI.TIMEDONE,
            DI.[GUID],
            DI.INPUT_MODE,
            DI.ID_TERMINAL,
            DI.LOCNCODEDEST,
            DI.SKL_ID_DEST,
            DI.WEIGHT,
            DI.NMBRPAL,
            DI.TYPEPAL,
            DI.PRINTED,
            DI.EXPIRACE,
            DI.AttributeToSN
        FROM dbo.CZMST_DI DI
        WHERE DI.CountEntries = @cnt
    )
    SELECT
        @sql =
            N''''SET NOCOUNT ON;'''' + CHAR(13) + CHAR(10) +
            N''''BEGIN TRY'''' + CHAR(13) + CHAR(10) +
            N''''    BEGIN TRAN;'''' + CHAR(13) + CHAR(10) +
            N''''    DELETE FROM dbo.CZMST_DI WHERE CountEntries = '''' + CAST(@cnt AS NVARCHAR(20)) + N'''';'''' + CHAR(13) + CHAR(10) +
            STUFF
            (
                (
                    SELECT
                        CHAR(13) + CHAR(10) +
                        N''''    INSERT INTO dbo.CZMST_DI ('''' +
                        N''''[CountEntries],[VNDITNUM],[CZ_CarKod],[ODB_ID],[STR_ID],[DOC_ID],[DOC_ID2],[SKL_ID],[PRAC_ID],[ITEMNMBR],'''' +
                        N''''[ITEMCODE],[LOCNCODE],[MJ],[QTYSHPPD],[QTYSHPPDMJ],[QTYPACK],[SERLTNUM],[TAXAMPIE],[AMOUNPIE],[WITHTAX],'''' +
                        N''''[PRICEX],[mena_ID],[TAXAMPIEM],[AMOUNPIEM],[mena_IDM],[REZ_1],[REZ_2],[REZ_3],[REZ_4],[USER_ID],'''' +
                        N''''[DATEDONE],[TIMEDONE],[GUID],[INPUT_MODE],[ID_TERMINAL],[LOCNCODEDEST],[SKL_ID_DEST],[WEIGHT],[NMBRPAL],[TYPEPAL],'''' +
                        N''''[PRINTED],[EXPIRACE],[AttributeToSN]'''' +
                        N'''') VALUES ('''' +

                        CAST(D.CountEntries AS NVARCHAR(20)) + N'''','''' +

                        CASE WHEN D.VNDITNUM IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.VNDITNUM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.CZ_CarKod IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.CZ_CarKod, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.ODB_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.ODB_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.STR_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.STR_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.DOC_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.DOC_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.DOC_ID2 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.DOC_ID2, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.SKL_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.SKL_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.PRAC_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.PRAC_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.ITEMNMBR, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CASE WHEN D.ITEMCODE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.ITEMCODE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.LOCNCODE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.LOCNCODE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.MJ, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CONVERT(NVARCHAR(50), D.QTYSHPPD) + N'''','''' +
                        CONVERT(NVARCHAR(50), D.QTYSHPPDMJ) + N'''','''' +
                        CASE WHEN D.QTYPACK IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.QTYPACK) END + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.SERLTNUM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CASE WHEN D.TAXAMPIE IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.TAXAMPIE) END + N'''','''' +
                        CASE WHEN D.AMOUNPIE IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.AMOUNPIE) END + N'''','''' +
                        CASE WHEN D.WITHTAX IS NULL THEN N''''NULL'''' ELSE CAST(D.WITHTAX AS NVARCHAR(10)) END + N'''','''' +
                        CASE WHEN D.PRICEX IS NULL THEN N''''NULL'''' ELSE CAST(D.PRICEX AS NVARCHAR(10)) END + N'''','''' +
                        CASE WHEN D.mena_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.mena_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.TAXAMPIEM IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.TAXAMPIEM) END + N'''','''' +
                        CASE WHEN D.AMOUNPIEM IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.AMOUNPIEM) END + N'''','''' +
                        CASE WHEN D.mena_IDM IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.mena_IDM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_1 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_1, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_2 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_2, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_3 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_3, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_4 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_4, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.USER_ID IS NULL THEN N''''NULL'''' ELSE CAST(D.USER_ID AS NVARCHAR(20)) END + N'''','''' +
                        CASE WHEN D.DATEDONE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.DATEDONE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.TIMEDONE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.TIMEDONE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.[GUID] IS NULL THEN N''''NULL'''' ELSE N'''''''''''''''' + CONVERT(NVARCHAR(36), D.[GUID]) + N'''''''''''''''' END + N'''','''' +
                        CAST(D.INPUT_MODE AS NVARCHAR(20)) + N'''','''' +
                        CAST(D.ID_TERMINAL AS NVARCHAR(20)) + N'''','''' +
                        CASE WHEN D.LOCNCODEDEST IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.LOCNCODEDEST, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.SKL_ID_DEST IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.SKL_ID_DEST, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.WEIGHT IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.WEIGHT) END + N'''','''' +
                        CASE WHEN D.NMBRPAL IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.NMBRPAL, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.TYPEPAL IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.TYPEPAL, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.PRINTED IS NULL THEN N''''NULL'''' ELSE CAST(D.PRINTED AS NVARCHAR(10)) END + N'''','''' +
                        CASE WHEN D.EXPIRACE IS NULL THEN N''''NULL'''' ELSE N'''''''''''''''' + CONVERT(NVARCHAR(33), D.EXPIRACE, 126) + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.AttributeToSN IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.AttributeToSN, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END +

                        N'''');''''
                    FROM D
                    ORDER BY D.CountEntries, D.ITEMNMBR, D.SERLTNUM, D.[GUID]
                    FOR XML PATH(''''''''), TYPE
                ).value(''''.'''', ''''nvarchar(max)'''')
            , 1, 2, N'''''''') +
            CHAR(13) + CHAR(10) +
            N''''    COMMIT;'''' + CHAR(13) + CHAR(10) +
            N''''END TRY'''' + CHAR(13) + CHAR(10) +
            N''''BEGIN CATCH'''' + CHAR(13) + CHAR(10) +
            N''''    IF @@TRANCOUNT > 0 ROLLBACK;'''' + CHAR(13) + CHAR(10) +
            N''''    THROW;'''' + CHAR(13) + CHAR(10) +
            N''''END CATCH;'''';

    SELECT @sql;
END'';

-- Installation step 370
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_AfterProcess_3P]
    @table        NVARCHAR(20),
    @countentries NVARCHAR(20),
    @itemtype     NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @cnt INT = TRY_CONVERT(INT, @countentries);
        DECLARE @rowCount INT = 0;
        DECLARE @message NVARCHAR(400);

        IF @cnt IS NULL
            THROW 50011, ''''Parametr @countentries není platné číslo.'''', 1;

        IF OBJECT_ID(''''dbo.FASK_AfterProcess_Log'''', ''''U'''') IS NULL
        BEGIN
            CREATE TABLE dbo.FASK_AfterProcess_Log
            (
                ID           INT IDENTITY(1,1) PRIMARY KEY,
                TableName    NVARCHAR(50)  NOT NULL,
                CountEntries INT           NOT NULL,
                ItemType     NVARCHAR(20)  NULL,
                MessageText  NVARCHAR(400) NULL,
                CreatedAt    DATETIME2(0)  NOT NULL DEFAULT SYSDATETIME()
            );
        END

        IF @table = N''''CZMST_SI''''
        BEGIN
            SELECT @rowCount = COUNT(*)
            FROM dbo.CZMST_SI
            WHERE CountEntries = @cnt;

            IF @rowCount = 0
                THROW 50012, ''''Pro danou dávku neexistují data v CZMST_SI.'''', 1;
        END
        ELSE IF @table = N''''CZMST_DI''''
        BEGIN
            SELECT @rowCount = COUNT(*)
            FROM dbo.CZMST_DI
            WHERE CountEntries = @cnt;

            IF @rowCount = 0
                THROW 50013, ''''Pro danou dávku neexistují data v CZMST_DI.'''', 1;
        END
        ELSE
        BEGIN
            THROW 50014, ''''Nepodporovaná tabulka v @table.'''', 1;
        END

        SET @message =
            N''''POST akce 3P spuštěna, počet záznamů v dávce: '''' +
            CAST(@rowCount AS NVARCHAR(20));

        INSERT INTO dbo.FASK_AfterProcess_Log
        (
            TableName,
            CountEntries,
            ItemType,
            MessageText
        )
        VALUES
        (
            @table,
            @cnt,
            NULLIF(@itemtype, N''''''''),
            @message
        );

        -- ==========================================
        -- ukázková business logika podle ITEMTYPE
        -- ==========================================
        IF @table = N''''CZMST_SI''''
        BEGIN
            IF @itemtype = N''''P''''
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_SI'''',
                    @cnt,
                    @itemtype,
                    N''''Zpracováno pro ITEMTYPE=P, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
            ELSE IF @itemtype = N''''I''''
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_SI'''',
                    @cnt,
                    @itemtype,
                    N''''Zpracováno pro ITEMTYPE=I, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
            ELSE
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_SI'''',
                    @cnt,
                    NULLIF(@itemtype, N''''''''),
                    N''''Běžný výdej / jiný ITEMTYPE, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
        END
        ELSE IF @table = N''''CZMST_DI''''
        BEGIN
            IF @itemtype = N''''P''''
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_DI'''',
                    @cnt,
                    @itemtype,
                    N''''Prodej s ITEMTYPE=P, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
            ELSE IF @itemtype = N''''I''''
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_DI'''',
                    @cnt,
                    @itemtype,
                    N''''Prodej s ITEMTYPE=I, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
            ELSE
            BEGIN
                INSERT INTO dbo.FASK_AfterProcess_Log
                (
                    TableName,
                    CountEntries,
                    ItemType,
                    MessageText
                )
                VALUES
                (
                    N''''CZMST_DI'''',
                    @cnt,
                    NULLIF(@itemtype, N''''''''),
                    N''''Prodej bez speciální předlohy, počet záznamů: '''' + CAST(@rowCount AS NVARCHAR(20))
                );
            END
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(''''FASK_AfterProcess_3P failed. %s'''', 16, 1, @ErrMsg);
        RETURN;
    END CATCH
END'';

-- Installation step 371
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_AfterProcess_3P_CS_Vedlejsi]
    @table        NVARCHAR(20),
    @countentries NVARCHAR(20),
    @itemtype     NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @cnt INT = TRY_CONVERT(INT, @countentries);
    DECLARE @sql NVARCHAR(MAX);

    IF @cnt IS NULL
        THROW 50111, ''''Parametr @countentries není platné číslo.'''', 1;

    IF @table <> N''''CZMST_SI''''
        THROW 50112, ''''Tato procedura aktuálně podporuje pouze tabulku CZMST_SI.'''', 1;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.CZMST_SI
        WHERE CountEntries = @cnt
    )
        THROW 50113, ''''Pro danou dávku neexistují data v dbo.CZMST_SI.'''', 1;


		--Logovani do nasi tabulky START
IF OBJECT_ID(''''dbo.EXTERNAL_POST_LOG'''', ''''U'''') IS NULL
BEGIN
    CREATE TABLE dbo.EXTERNAL_POST_LOG
    (
        ID           INT IDENTITY(1,1) PRIMARY KEY,
        TableName    NVARCHAR(50) NOT NULL,
        CountEntries INT NOT NULL,
        ItemType     NVARCHAR(20) NULL,
        CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSDATETIME()
    );
END;

INSERT INTO dbo.EXTERNAL_POST_LOG
(
    TableName,
    CountEntries,
    ItemType,
    CreatedAt
)
VALUES
(
    @table,
    @cnt,
    NULLIF(@itemtype, N''''''''),
    SYSDATETIME()
);
		--Logovani do nasi tabulky END



    ;WITH D AS
    (
        SELECT
            SI.CountEntries,
            SI.SOPNUMBE,
            SI.ITEMNMBR,
            SI.ORD,
            SI.VNDDOCNM,
            SI.VNDITNUM,
            SI.CZ_CarKod,
            SI.SKL_ID,
            SI.LOCNCODE,
            SI.MJ,
            SI.QTYSHPPD,
            SI.QTYPACK,
            SI.QTYSHPPDMJ,
            SI.SERLTNUM,
            SI.KOD_SW,
            SI.DAT_VYROBY,
            SI.REZ_1,
            SI.REZ_2,
            SI.ODBER_ID,
            SI.DATEDONE,
            SI.TIMEDONE,
            SI.USER_ID,
            SI.TYPEPAL,
            SI.NMBRPAL,
            SI.PRINTED,
            SI.[GUID],
            SI.INPUT_MODE,
            SI.ID_TERMINAL,
            SI.ITEMCODE,
            SI.WEIGHT,
            SI.Expirace
        FROM dbo.CZMST_SI SI
        WHERE SI.CountEntries = @cnt
    )
    SELECT
        @sql =
            N''''SET NOCOUNT ON;'''' + CHAR(13) + CHAR(10) +
            N''''BEGIN TRY'''' + CHAR(13) + CHAR(10) +
            N''''    BEGIN TRAN;'''' + CHAR(13) + CHAR(10) +
            N''''    DELETE FROM dbo.CZMST_SI WHERE CountEntries = '''' + CAST(@cnt AS NVARCHAR(20)) + N'''';'''' + CHAR(13) + CHAR(10) +
            STUFF
            (
                (
                    SELECT
                        CHAR(13) + CHAR(10) +
                        N''''    INSERT INTO dbo.CZMST_SI ('''' +
                        N''''[CountEntries],[SOPNUMBE],[ITEMNMBR],[ORD],[VNDDOCNM],[VNDITNUM],[CZ_CarKod],[SKL_ID],[LOCNCODE],[MJ],'''' +
                        N''''[QTYSHPPD],[QTYPACK],[QTYSHPPDMJ],[SERLTNUM],[KOD_SW],[DAT_VYROBY],[REZ_1],[REZ_2],[ODBER_ID],[DATEDONE],'''' +
                        N''''[TIMEDONE],[USER_ID],[TYPEPAL],[NMBRPAL],[PRINTED],[GUID],[INPUT_MODE],[ID_TERMINAL],[ITEMCODE],[WEIGHT],[Expirace]'''' +
                        N'''') VALUES ('''' +

                        CAST(D.CountEntries AS NVARCHAR(20)) + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.SOPNUMBE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CASE WHEN D.ITEMNMBR IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.ITEMNMBR, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +

                        CAST(D.ORD AS NVARCHAR(20)) + N'''','''' +

                        CASE WHEN D.VNDDOCNM IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.VNDDOCNM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.VNDITNUM IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.VNDITNUM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.CZ_CarKod IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.CZ_CarKod, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.SKL_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.SKL_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.LOCNCODE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.LOCNCODE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.MJ, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CONVERT(NVARCHAR(50), D.QTYSHPPD) + N'''','''' +
                        CONVERT(NVARCHAR(50), D.QTYPACK) + N'''','''' +
                        CASE WHEN D.QTYSHPPDMJ IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.QTYSHPPDMJ) END + N'''','''' +

                        N''''N'''''''''''' + REPLACE(D.SERLTNUM, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''','''' +

                        CASE WHEN D.KOD_SW IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.KOD_SW, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.DAT_VYROBY IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.DAT_VYROBY, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_1 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_1, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.REZ_2 IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.REZ_2, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.ODBER_ID IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.ODBER_ID, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.DATEDONE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.DATEDONE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.TIMEDONE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.TIMEDONE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +

                        CAST(D.USER_ID AS NVARCHAR(20)) + N'''','''' +

                        CASE WHEN D.TYPEPAL IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.TYPEPAL, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.NMBRPAL IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.NMBRPAL, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.PRINTED IS NULL THEN N''''NULL'''' ELSE CAST(D.PRINTED AS NVARCHAR(10)) END + N'''','''' +
                        CASE WHEN D.[GUID] IS NULL THEN N''''NULL'''' ELSE N'''''''''''''''' + CONVERT(NVARCHAR(36), D.[GUID]) + N'''''''''''''''' END + N'''','''' +
                        CAST(D.INPUT_MODE AS NVARCHAR(20)) + N'''','''' +
                        CAST(D.ID_TERMINAL AS NVARCHAR(20)) + N'''','''' +
                        CASE WHEN D.ITEMCODE IS NULL THEN N''''NULL'''' ELSE N''''N'''''''''''' + REPLACE(D.ITEMCODE, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''' END + N'''','''' +
                        CASE WHEN D.WEIGHT IS NULL THEN N''''NULL'''' ELSE CONVERT(NVARCHAR(50), D.WEIGHT) END + N'''','''' +
                        CASE WHEN D.Expirace IS NULL THEN N''''NULL'''' ELSE N'''''''''''''''' + CONVERT(NVARCHAR(33), D.Expirace, 126) + N'''''''''''''''' END +

                        N'''');''''
                    FROM D
                    ORDER BY D.ORD, D.SOPNUMBE, D.ITEMNMBR, D.SERLTNUM
                    FOR XML PATH(''''''''), TYPE
                ).value(''''.'''', ''''nvarchar(max)'''')
            , 1, 2, N'''''''') +
            CHAR(13) + CHAR(10) +
            N''''    COMMIT;'''' + CHAR(13) + CHAR(10) +
            N''''END TRY'''' + CHAR(13) + CHAR(10) +
            N''''BEGIN CATCH'''' + CHAR(13) + CHAR(10) +
            N''''    IF @@TRANCOUNT > 0 ROLLBACK;'''' + CHAR(13) + CHAR(10) +
            N''''    THROW;'''' + CHAR(13) + CHAR(10) +
            N''''END CATCH;'''';

    SELECT @sql;
END'';

-- Installation step 372
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 7.7.2014
-- Description:	Last user production
-- =============================================
CREATE PROCEDURE [dbo].[fask_CZPRO_LastUserAction] 
	-- Add the parameters for the stored procedure here
	@loginid nvarchar(20), 
	@machineid nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 * from Production
	where loginid=@loginid and machineid=@machineid
	order by dateeve desc
END'';

-- Installation step 373
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Matouš Rathouzský
-- Create date: 25.7.2023
-- Description:	presun zaznamu z FASK_Events do tabulky FASK_Events_HISTORY
-- =============================================
CREATE PROCEDURE [dbo].[FASK_Events_ArchivaceZaznamu]
    @DatumOd DATETIME,
    @DatumDo DATETIME,
    @PocetPresunutych INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Začneme transakci, abychom zajistili celistvost datových změn.
        BEGIN TRANSACTION;

        -- Přesuneme data z "FASK_Events" do "FASK_Events_HISTORY" pro záznamy v zadaném rozmezí datumů.
        INSERT INTO FASK_Events_HISTORY 
		(
		[loginid],
	[machineid],
	[dateeve],
	[qty],
	[qtyReal],
	[description],
	[barcodeReaded],
	[barcodeSended],
	[zakazka],
	[popis],
	[faskGUID],
	[reportType],
	[isProcessed],
	[IDO],
	[scan1],
	[scan2],
	[scan3],
	[sensor],
	[material],
	[productionGuid],
	[VPH],
	[VPPol],
	[EAN_IS],
	[IS_ID],
	[NMBRPAL],
	[status],
	[QTYPACK],
	[PackType],
	[WEIGHT],
	[BarcodeT],
	[REZ_1],
	[REZ_2],
	[REZ_3],
	[REZ_4],
	[REZ_5]
	)
        SELECT 
		[loginid],
	[machineid],
	[dateeve],
	[qty],
	[qtyReal],
	[description],
	[barcodeReaded],
	[barcodeSended],
	[zakazka],
	[popis],
	[faskGUID],
	[reportType],
	[isProcessed],
	[IDO],
	[scan1],
	[scan2],
	[scan3],
	[sensor],
	[material],
	[productionGuid],
	[VPH],
	[VPPol],
	[EAN_IS],
	[IS_ID],
	[NMBRPAL],
	[status],
	[QTYPACK],
	[PackType],
	[WEIGHT],
	[BarcodeT],
	[REZ_1],
	[REZ_2],
	[REZ_3],
	[REZ_4],
	[REZ_5]
	
        FROM FASK_Events
        WHERE dateeve BETWEEN @DatumOd AND @DatumDo;

        -- Zjistíme počet přesunutých záznamů.
        SET @PocetPresunutych = @@ROWCOUNT;

        -- Odstraníme data z "FASK_Events" pro záznamy v zadaném rozmezí datumů.
        DELETE FROM FASK_Events
        WHERE dateeve BETWEEN @DatumOd AND @DatumDo;

        -- Pokud přesun proběhne úspěšně, provedeme COMMIT, aby byla transakce dokončena.
        COMMIT TRANSACTION;

        -- Vypíšeme úspěšné provedení procedury.
        PRINT ''''Data byla úspěšně přesunuta do tabulky FASK_Events_HISTORY a odstraněna z tabulky FASK_Events.'''';
    END TRY
    BEGIN CATCH
        -- Pokud dojde k chybě, provedeme ROLLBACK, aby byly vráceny všechny změny v transakci.
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Vypíšeme chybu.
        PRINT ''''Došlo k chybě při přesunu dat: '''' + ERROR_MESSAGE();
    END CATCH;
END;


/****** Object:  StoredProcedure [dbo].[Production_ArchivaceZaznamu]    Script Date: 04.08.2023 13:10:36 ******/
SET ANSI_NULLS ON'';

-- Installation step 374
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 5.6.2019
-- Description:	Procedura pro prevod vyroby ze sledovani do Production
-- =============================================
CREATE PROCEDURE [dbo].[fask_Events2Production_Confirm] 
	-- Add the parameters for the stored procedure here
	@from datetime = null, 
	@to datetime = null
AS
BEGIN
	
	/*
	1. do temp struktury vytahnout data z Events, se kterymi budu pracovat
	2. z temp struktury vytvorit sumaci za klic
	3. tyto sumy vlozit do Production, guid, ktery je pridelen zaznamenat do temp struktury k polozkam dle klice
	4. zaznamu z temp strukutry promitnout zpet do Events (vazebni guid dle id)
	*/

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare
		@datetimemin datetime,
		@datetimemax datetime

	set @datetimemin = CONVERT(datetime, 0)
	set @datetimemax = GETDATE()

    -- Insert statements for procedure here
	-- SELECT @from, @to

	IF OBJECT_ID(''''tempdb..#EventsTemp'''', ''''U'''') IS NOT NULL
		DROP TABLE #EventsTemp;

	SELECT 
		GETDATE() [GeneratedDatetime]
		,dbo.fask_GS1_AI_GET(''''EAN'''', e.barcodeReaded) EAN
		,dbo.fask_GS1_AI_GET(''''SARZE'''', e.barcodeReaded) SARZE
		,dbo.fask_GS1_AI_GET(''''EXPIRACE'''', e.barcodeReaded) EXPIRACE
		,dbo.fask_GS1_AI_GET(''''VAHA'''', e.barcodeReaded) VAHA
		,* 
	INTO #EventsTemp
	from FASK_Events e 
	where 1=1
	and ((e.isProcessed is NULL) OR (e.productionGuid is null))
	and e.dateeve between ISNULL(@from, @datetimemin) and ISNULL(@to, @datetimemax)

	-- priprava dat k vlozeni do production
	IF OBJECT_ID(''''tempdb..#Procution2Insert'''', ''''U'''') IS NOT NULL
		DROP TABLE #Procution2Insert;

	select 
		et.EAN
		, et.SARZE
		, et.EXPIRACE
		, et.material
		, count(*) pocetPolozek
		, sum(convert(numeric(18,5),isnull(et.VAHA, ''''0'''')) / 100) vaha
		, NEWID() productionGuid
	into #Procution2Insert
	from #EventsTemp et
	group by et.EAN, et.SARZE, et.EXPIRACE, et.material

	-- vlozeni do Production
	insert into Production (
		CountEntries
		,ITEMNMBR	-- EAN
		,BarcodeP	-- EAN taky
		,[description] -- popis obsahujici informaci o sarzi zadane na stroji
		,loginid	-- cislo, urcujici server(stanici)? mel byt prihlaseny uzivatel
		,dateeve	-- aktualni cas 
		,qty		-- pocet polozek
		,qtyReal	-- celkova vaha
		,UserID		-- cislo, urcujici server(stanici)?
		,TermID		-- cislo, urcujici server(stanici)?
		,[GUID]		-- guid noveho zaznamu
		,SERLTNUM	-- sarze (doplnit do struktury)
		,Expiration -- expirace (doplnit do struktury)
	)
	select 
		1	CountEntries
		,p.EAN ITEMNMBR	-- EAN
		,p.EAN BarcodeP	-- EAN taky
		,p.material [description] -- popis obsahujici informaci o sarzi zadane na stroji
		,0 loginid	-- cislo, urcujici server(stanici)? mel byt prihlaseny uzivatel
		,getdate() dateeve	-- aktualni cas 
		,p.pocetPolozek qty		-- pocet polozek
		,p.vaha qtyReal	-- celkova vaha
		,0 UserID		-- cislo, urcujici server(stanici)?
		,0 TermID		-- cislo, urcujici server(stanici)?
		,p.productionGuid [GUID]		-- guid noveho zaznamu
		,p.SARZE Serltnum	-- sarze (doplnit do struktury)
		,p.EXPIRACE Expiration -- expirace (doplnit do struktury)
		from #Procution2Insert p

	-- aktualizace #EventsTemp
	update #EventsTemp
	set productionGuid = p.productionGuid
	from #Procution2Insert p
	where 1=1
		and #EventsTemp.EAN = p.EAN
		and #EventsTemp.SARZE = p.Sarze
		and #EventsTemp.EXPIRACE = p.EXPIRACE
		and #EventsTemp.material = p.material

	-- update Fask_Events
	update FASK_Events
	set 
		fask_events.productionGuid = et.productionGuid
		,fask_events.isProcessed = et.GeneratedDatetime
	from #EventsTemp et
	where 1=1
		and FASK_Events.id = et.id

	-- konec zpracovani procedury
	drop table #EventsTemp
	drop table #Procution2Insert
	--select 0 as OK
	--select 0 as OKddd
	RETURN 0

END'';

-- Installation step 375
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Matous Rathouzsky a Tadeas Divacky
-- Create date: 4.10.2021
-- Description:	Procedura pro prevod vyroby z FASK_Events do Production
-- =============================================

CREATE PROCEDURE [dbo].[fask_Events2Production_Trigger] 
	-- Add the parameters for the stored procedure here

@id [int],
@VPH [nvarchar] (30) ,
@IS_ID [nvarchar] (40) ,
@loginid [nvarchar] (20) ,
@machineid [nvarchar] (20) ,
@qty [numeric](19, 5) ,
@qtyReal [numeric](19, 5),
@BarcodeP [nvarchar] (50) ,
@dateeve [datetime],
@NMBRPAL [nvarchar](50),
@PackType [nvarchar](10),
@status [int],
@WEIGHT [numeric](19,5),
@qtypack [numeric](19,5),
@rez1 [nvarchar](100),
@rez2 [nvarchar](100),
@rez3 [nvarchar](100),
@rez4 [nvarchar](100),
@rez5 [nvarchar](100)

AS

--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int, @ins2_error int 

IF    (((@qty = 0) AND (@qtyReal = 0)) OR (@qty + @qtyReal = 0))
	BEGIN
		PRINT ''''VYBER SPATNE ZAZNAMY NE''''
	END
ELSE
	BEGIN




-------------------------------------------deklarace tabulky *obraz FASK_Events-----------------------------------------------
DECLARE @LOCAL_TABLEVARIABLE TABLE
(       id int 
       ,loginid nvarchar (50) 
      ,[machineid] nvarchar (50) 
      ,[dateeve] datetime
      ,[qty] [numeric](19, 5)
      ,[qtyReal] [numeric](19, 5)
      ,[barcodeReaded] nvarchar (50)
      ,[VPH] nvarchar (50)
      ,[IS_ID] nvarchar (50)
      ,productionGuid_new nvarchar (50) --,[GUID] -- identifikator radku, budu pouzivat metodu new identifier najit v procedure!!
      ,SOUBEHGUID_NEW nvarchar (50)
      ,NMBRPAL [nvarchar](50)
      ,PackType [nvarchar](10)
      ,status [int]
      ,WEIGHT [numeric](19,5)
      ,qtypack [numeric](19,5)
      ,rez1 [nvarchar](100)
      ,rez2 [nvarchar](100)
      ,rez3 [nvarchar](100)
      ,rez4 [nvarchar](100)
      ,rez5 [nvarchar](100)
)

----------------------------------naplneni pomocne tabulky----------------------------------------

INSERT INTO @LOCAL_TABLEVARIABLE 
(
	   id
      ,[loginid]
      ,[machineid]
      ,[dateeve] 
      ,[qty]
      ,[qtyReal]
      ,[barcodeReaded]
      ,[VPH]
      ,[IS_ID]
      ,productionGuid_new--,[GUID] -- identifikator radku, budu pouzivat metodu new identifier najit v procedure!!      
      ,SOUBEHGUID_NEW 
      ,NMBRPAL
      ,PackType 
      ,status 
      ,WEIGHT 
      ,qtypack
      ,rez1
      ,rez2
      ,rez3
      ,rez4
      ,rez5
)
VALUES(
@id       ,
@loginid  ,
@machineid,
GETDATE() ,
@qty      ,
@qtyReal  ,
@BarcodeP ,
@VPH      ,
@IS_ID    ,
NEWID()   ,
NEWID()   ,
@NMBRPAL  ,
@PackType ,
@status   ,
@WEIGHT   ,
@qtypack  ,
@rez1     ,
@rez2     ,
@rez3     ,
@rez4     ,
@rez5     
)
	

-------------------------------------------- Dotazeni s FASK_ZASOBY ---------------------------------------------------------------------------------------------


declare @MJ nvarchar(10)
declare @ITEMDESC nvarchar(100)


select 
@MJ = MJ,
@ITEMDESC = ITEMDESC
from FASK_ZASOBY
where 1=1
and ITEMNMBR = @IS_ID
and VNDITNUM = @BarcodeP


-------------------------------------------- vlozeni do Production---------------------------------------------------------------------------------------------



	insert into Production (
		[CountEntries] --cislo davky, zapis z FASK_Event sloupec VPH  **
		,[SOPNUMBE] -- cislo VP, zapis z FASK_Event sloupec VPH**
		,[ITEMNMBR] -- ID polozky, zapis z FASK_Events sloupe IS_ID**
		,[ITEMTYPE] --typ polozky, davat prazdny retezec**
		,[ITEMMJ] --merna jednotka polozky, davat prazdny retezec**
		,[ITEMDESC] --nazev polozky y FAKS_Events sloupec IS_ID**
		,[ORD] --poradi, ID radku informacniho systemu
		,[TIMEMODE] -- zpusob sledovani vyroby, 3zpusoby stop, star-stop, nebo start-start-stop, --budu davat 0
		,[TIMEPREPSTART] --cas pripravy start, budu davat null
		,[TIMEPREPSTOP] --cas pripravy konec, budu davat null
		,[TIMEPREP] -- priznak, budu davat 0
		,[TIMEUNIT] -- jednotka, budu psat 0
		,[TIMESTART] -- casy start vyroby, protoze je varyante stop -- budu davat null
		,[TIMESTOP] -- budu davat dateeve z FASK_Events
		,[TIMECORSTART] --casy korekce, cas vyplnuje pracovni dobu vyroby,treba prestavka na koureni --budu psat null
		,[TIMECORSTOP] --budu psat null
		,[TIMECOR] --budu psat null
		,[TIMECRID] -- ciselnik korekci, napr. 10min na zachode --budu psat null
		,[TIMECRIDTYPE] --budu psat null
		,[loginid] -- id uzivatele bere z FASK_Event sloupec loginid
		,[machineid] --id stroje bere z FASK_Event sloupec machineid
		,[operationid] -- budu davat null
		,[dateeve] -- cas kdy vznikl zaznam, budu pouzivat metodu GETDATE
		,[qty] -- mnozstvi bere z FASK_Event sloupec qty
		,[qtyReal] -- mnozstvi bere z FASK_Event sloupec qtyReal
		,[QTYPACK] -- cislo, prepoctovy koeficient mezi mernyma jednotkama napr. krabice ma v sobe 4ks tak se rovna 4, budu psat 0
		,[QTYPACKMJ] -- vyjadreni prepoctovy koeficient, budu davat prazdny retezec -- ''''''''
		,[description] --popis, budu davat prazdny retezec ''''''''
		,[BarcodeP] -- carovy kod, identifikator polozky z VP, budu davat z tabulky FAK_Events sloupec barcodereaded 
		,[UserID] -- id uzivatele bere z FASK_Event sloupec loginid
		,[TermID] -- id terminalu, budu davat machine ID z FASK_Events
		,[ISOK] -- casovy priznak kdy byly data zpracovana z IS, davat budu null
		,[GUID] -- identifikator radku, budu pouzivat metodu new identifier najit v procedure!!
		,[SOUBEHGUID] --guid, vayba do productionSources, doplni se, optat se, budu pouzivat metodu new identifier najit v procedure!!
		,[CORRGUID] --guid odkayujici do korekci // budu davat null
		,[qtyOld] -- logika se yadavanim mnoystvi, budu davat null
		,[idVS] -- id vedouciho smeny, specialni uzivatel ktery schvaluje zaznamy, budu davat null
		,[dateedit] -- cas posledni editace, davat null
		,[SKL_ID] -- id skladu na ktery sklad vyrabim, budu davat null
		,[LOCNCODE] --lokace na kterou vyrabim, budu davat null
		,[SERLTNUM] -- vyjedreni sarze nebo seriove cislo, budu davat null
		,[EXPIRATION] -- expirace, budu davat null
		,[NMBRPAL] -- cislo palety
		,[TYPEPAL] -- typ palety
		,[PackType] -- typ baleni
		,[status] -- informativnz status
		,[WEIGHT] -- vaha
		,[STORNOGUID] -- ???
		,[REZ_1] 
		,[REZ_2] 
		,[REZ_3] 
		,[REZ_4] 
		,[REZ_5] 
	    ,[WEIGHT_OLD]
 	)
	select 
		p.VPH--[CountEntries] --cislo davky, zapis z FASK_Event sloupec VPH  **
	        ,p.VPH--,[SOPNUMBE] -- cislo VP, zapis z FASK_Event sloupec VPH**
	        ,p.IS_ID--,[ITEMNMBR] -- ID polozky, zapis z FASK_Events sloupe IS_ID**
		,''''false''''--,[ITEMTYPE] --typ polozky, davat prazdny retezec**
		,Left(ISNULL(@MJ, ''''''''),5)--,[ITEMMJ] --merna jednotka polozky, davat prazdny retezec**
		,ISNULL(@ITEMDESC, p.IS_ID)--,[ITEMDESC] --nazev polozky y FAKS_Events sloupec IS_ID**
		,1--,[ORD] --poradi, ID radku informacniho systemu
		,0--,[TIMEMODE] -- zpusob sledovani vyroby, 3zpusoby stop, star-stop, nebo start-start-stop, --budu davat 0
		,null--,[TIMEPREPSTART] --cas pripravy start, budu davat null
		,null--,[TIMEPREPSTOP] --cas pripravy konec, budu davat null
		,0--,[TIMEPREP] -- priznak, budu davat 0
		,0--,[TIMEUNIT] -- jednotka, budu psat 0
		,null--,[TIMESTART] -- casy start vyroby, protoze je varyante stop -- budu davat null
		,p.dateeve--,[TIMESTOP] -- budu davat dateeve z FASK_Events
		,null--,[TIMECORSTART] --casy korekce, cas vyplnuje pracovni dobu vyroby,treba prestavka na koureni --budu psat null
		,null--,[TIMECORSTOP] --budu psat null
		,null--,[TIMECOR] --budu psat null
		,null--,[TIMECRID] -- ciselnik korekci, napr. 10min na zachode --budu psat null
		,null--,[TIMECRIDTYPE] --budu psat null
		,p.loginid--,[loginid] -- id uzivatele bere z FASK_Event sloupec loginid
		,p.machineid--,[machineid] --id stroje bere z FASK_Event sloupec machineid
		,null--,[operationid] -- budu davat null
		,p.dateeve--,[dateeve] -- cas kdy vznikl zaznam, budu pouzivat metodu GETDATE
		,p.qtyReal + p.qty--,[qty] -- mnozstvi bere z FASK_Event sloupec qty
		,p.qtyReal + p.qty--,[qtyReal] -- mnozstvi bere z FASK_Event sloupec qtyReal
		,p.qtypack --,[QTYPACK] -- cislo, prepoctovy koeficient mezi mernyma jednotkama napr. krabice ma v sobe 4ks tak se rovna 4, budu psat 0
		,'''''''' --,[QTYPACKMJ] -- vyjadreni prepoctovy koeficient, budu davat prazdny retezec -- ''''''''
		,'''''''' --,[description] --popis, budu davat prazdny retezec ''''''''
		,p.barcodeReaded--,[BarcodeP] -- carovy kod, identifikator polozky z VP, budu davat z tabulky FAK_Events sloupec barcodereaded 
		,p.loginid--,[UserID] -- id uzivatele bere z FASK_Event sloupec loginid
		,p.machineid--,[TermID] -- id terminalu, budu davat machine ID z FASK_Events
		,null--,[ISOK] -- casovy priznak kdy byly data zpracovana z IS, davat budu null
		,p.productionGuid_new --,[GUID] -- identifikator radku, budu pouzivat metodu new identifier najit v procedure!!
		,p.SOUBEHGUID_NEW --,[SOUBEHGUID] --guid, vayba do productionSources, doplni se, optat se, budu pouzivat metodu new identifier najit v procedure!!
		,null--,[CORRGUID] --guid odkayujici do korekci // budu davat null
		,null--,[qtyOld] -- logika se yadavanim mnoystvi, budu davat null
		,null--,[idVS] -- id vedouciho smeny, specialni uzivatel ktery schvaluje zaznamy, budu davat null
		,null--,[dateedit] -- cas posledni editace, davat null
		,null--,[SKL_ID] -- id skladu na ktery sklad vyrabim, budu davat null
		,null--,[LOCNCODE] --lokace na kterou vyrabim, budu davat null
		,null--,[SERLTNUM] -- vyjedreni sarze nebo seriove cislo, budu davat null
		,null--,[EXPIRATION] -- expirace, budu davat null
		,p.NMBRPAL -- ,[NMBRPAL] -- cislo palety
		,null -- ,[TYPEPAL]-- typ palety
		,p.PackType --,[PackType]-- typ baleni
		,p.status --,[status]-- informativny status
		,p.WEIGHT --,[WEIGHT] -- vaha
		,null -- ,[STORNOGUID]--???
		,@rez1 --[REZ_1] 
		,@rez2 --[REZ_2] 
		,@rez3 --[REZ_3] 
		,@rez4 --[REZ_4] 
		,@rez5 --[REZ_5] 
	        ,null --[WEIGHT_OLD]
		from @LOCAL_TABLEVARIABLE p
		

------------------------------ update Fask_Events---------------------------------------------
	
	update FASK_Events
	set 
		fask_events.productionGuid = et.productionGuid_new
		,fask_events.isProcessed = et.dateeve
	from @LOCAL_TABLEVARIABLE et
	where 1=1
		and FASK_Events.id = et.id

	RETURN 0

END

/**************************************************************************************/
/****** Object:  Trigger [dbo].[InsertEvents]  ******/
SET ANSI_NULLS ON'';

-- Installation step 376
EXEC sys.sp_executesql N''CREATE   PROCEDURE [dbo].[FASK_GENERATE_DAVKA_VYDEJ]
    @CisExpPrik       NVARCHAR(20),
    @CounterEntries   INT OUTPUT,
    @StatusToReturn   INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

 

    -- Inicializace výstupů
    SET @CounterEntries = 0;
    SET @StatusToReturn = 0;


END'';

-- Installation step 377
EXEC sys.sp_executesql N''-- =============================================
-- Author: Tadeas Divacky
-- Create date: ??? Leta paně buhví kdy....
-- Description:	Generator lokaci, pro Vlacha.... nidky nepoužito...
-- =============================================
CREATE PROCEDURE [dbo].[FASK_GeneratorLokaci]
	-- Add the parameters for the stored procedure here
	@SKL_ID nvarchar(20), 
	@TYPE nvarchar(2),
	@OD int ,
	@Pocet int,
	@N int ,
	@prefix nvarchar(50) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO CZMST_SkladLokace_Mapa
([SKL_ID]
,[LOCNCODE]
,[TYPE]
,[Barcode]
,[Description])
SELECT
@SKL_ID as [SKL_ID], 
[Value] as [LOCNCODE],
@TYPE as [TYPE],
[Value] as [Barcode],
[Value] as [Description]
 FROM [dbo].[FASK_GeneratorRad] ( @OD ,@Pocet ,@N ,@prefix)

END'';

-- Installation step 378
EXEC sys.sp_executesql N''-- =============================================
-- Author: Tadeas Divacky
-- Create date: 9.2.2023
-- Description:	Generator SN pro CZPRO_VPP
-- =============================================

-- =============================================
-- Author: Matous Rathouzsky
-- Editace date: 9.1.2025
-- Description:	Generator SN pro CZPRO_VPP
-- =============================================

CREATE PROCEDURE [dbo].[FASK_GeneratorSN_Pro_CZPRO_VPP]
	-- Add the parameters for the stored procedure here
	@CountEntries int,
    @SOPNUMBE nvarchar(30),
    @ITEMNMBR nvarchar(40),
    @ITEMDESC nvarchar(100),

	--FASK_GeneratorSN_Pro_CZPRO_VPP
	 @QTYPACKMJ nvarchar(5),

	 @ITEMMJ nvarchar(5),
    @VNDITNUM nvarchar(60),
    @QTYSHPPD numeric(19,5),
    @QTYPACK numeric(19,5),
    @TIMEMODE int,
    @TIMEPREP real,
	@TIMEUNIT real,
    @SerNumT tinyint,
    @BarcodeT tinyint,
	@OD int ,
	@Pocet int,
	@N int ,
	@prefix nvarchar(50),
	@CZ_Rez1_Track tinyint,
	@CZ_Rez2_Track tinyint,
	@CZ_Rez3_Track tinyint,
	@CZ_Rez4_Track tinyint,
	@CZ_Rez5_Track tinyint,
	@WEIGHT_TARA numeric(19,5),
	@WEIGHT_NETTO numeric(19,5),
	@WEIGHT_TOL_PLUS numeric(19,5),
	@WEIGHT_TOL_MINUS numeric(19,5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    -- Insert statements for procedure here
INSERT INTO [dbo].[CZPRO_VPP]
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ITEMTYPE]
           ,[ITEMDESC]
           ,[ITEMMJ]
           ,[VNDDOCNMP]
           ,[VNDITNUM]
           ,[ORD]
           ,[BarcodeP]
           ,[LOCNCODE]
           ,[QTYSHPPD]
           ,[QTYDOKON]
           ,[QTYPACK]
           ,[QTYPACKMJ]
           ,[TIMEMODE]
           ,[TIMEPREP]
           ,[TIMEUNIT]
           ,[DtProdT]
           ,[DtProdL]
           ,[SerNumT]
           ,[SerNumL]
           ,[VerT]
           ,[VerL]
           ,[TermID]
           ,[LSTMod]
           ,[Realization_Start]
           ,[Realization_Stop]
           ,[BarcodeT]
	   ,[CZ_REZ1_Track]
	   ,[CZ_REZ2_Track]
	   ,[CZ_REZ3_Track]
	   ,[CZ_REZ4_Track]
	   ,[CZ_REZ5_Track]
	   ,[WEIGHT_TARA]
	   ,[WEIGHT_NETTO]
	   ,[WEIGHT_TOL_PLUS]
	   ,[WEIGHT_TOL_MINUS]
)
		   SELECT 
		   @CountEntries as CountEntries
      , @SOPNUMBE as SOPNUMBE
      ,@ITEMNMBR as ITEMNMBR
      ,'''''''' as ITEMTYPE
      ,@ITEMDESC as ITEMDESC
      ,@ITEMMJ as ITEMMJ
      ,'''''''' as VNDDOCNMP
      ,@VNDITNUM as VNDITNUM
      ,''''1'''' as ORD
      ,[value] as BarcodeP
      ,'''''''' as LOCNCODE
      ,1 as QTYSHPPD
      ,0 as QTYDOKON
      ,@QTYPACK as QTYPACK
      ,@QTYPACKMJ as QTYPACKMJ
      ,@TIMEMODE as TIMEMODE
      ,@TIMEPREP as TIMEPREP
      ,@TIMEUNIT as TIMEUNIT
      ,0 as DtProdT
      ,0 as DtProdL
      ,@SerNumT as SerNumT
      ,0 as SerNumL
      ,0 as VerT
      ,0 as VerL
      ,0 as TermID
      ,GETDATE() as LSTMod
      ,null as Realization_Start
      ,null as Realization_Stop
      ,@BarcodeT as BarcodeT
      ,@CZ_Rez1_Track as CZ_REZ1_Track
      ,@CZ_Rez2_Track as CZ_REZ2_Track
      ,@CZ_Rez3_Track as CZ_REZ3_Track
      ,@CZ_Rez4_Track as CZ_REZ4_Track
      ,@CZ_Rez5_Track as CZ_REZ5_Track
      ,@WEIGHT_TARA as [WEIGHT_TARA]
      ,@WEIGHT_NETTO as [WEIGHT_NETTO]
      ,@WEIGHT_TOL_PLUS as [WEIGHT_TOL_PLUS]
      ,@WEIGHT_TOL_MINUS as [WEIGHT_TOL_MINUS]
 FROM [dbo].[FASK_GeneratorRad] ( @OD ,@Pocet ,@N ,@prefix)

END'';

-- Installation step 379
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Matouš Rathouzský
-- Create date: 12.3.2023
-- Description:	Analyza odvodu, rozsahle vypocty nad stroji a zakazkami
-- =============================================
CREATE PROCEDURE [dbo].[FASK_MachineStateSetHistory_Analyza_Odvodu]
    @DateModifiedOD      datetime      = NULL,
    @DateModifiedDO      datetime      = NULL,
    @DateModified_TV     datetime      = NULL,
    @CisloSluzby         nvarchar(max) = NULL,
    @Description         nvarchar(100) = NULL,
    @S0                  int           = NULL,
    @S1                  int           = NULL,
    @S2                  int           = NULL,
    @S3                  int           = NULL,
    @S4                  int           = NULL,
    @S5                  int           = NULL,
    @S6                  int           = NULL,
    @S7                  int           = NULL,
    @S8                  int           = NULL,
    @S9                  int           = NULL,
    @S10                 int           = NULL,
    @S11                 int           = NULL,
    @Filtr_Zdroj         nvarchar(max) = NULL,
    @Filtr_StrojSklad    nvarchar(max) = NULL,
    @Filtr_StrojLokace   nvarchar(max) = NULL,
    @Filtr_users         nvarchar(max) = NULL
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    SET @Filtr_Zdroj       = NULLIF(LTRIM(RTRIM(@Filtr_Zdroj)), '''''''');
    SET @Filtr_StrojSklad  = NULLIF(LTRIM(RTRIM(@Filtr_StrojSklad)), '''''''');
    SET @Filtr_StrojLokace = NULLIF(LTRIM(RTRIM(@Filtr_StrojLokace)), '''''''');
    SET @CisloSluzby       = NULLIF(LTRIM(RTRIM(@CisloSluzby)), '''''''');
    SET @Description       = NULLIF(LTRIM(RTRIM(@Description)), '''''''');
    SET @Filtr_users       = NULLIF(LTRIM(RTRIM(@Filtr_users)), '''''''');

    IF OBJECT_ID(''''tempdb..#FiltrSklad'''') IS NOT NULL DROP TABLE #FiltrSklad;
    CREATE TABLE #FiltrSklad (SKL_ID int NOT NULL PRIMARY KEY);

    IF @Filtr_StrojSklad IS NOT NULL
    BEGIN
        INSERT INTO #FiltrSklad (SKL_ID)
        SELECT DISTINCT TRY_CAST(LTRIM(RTRIM(value)) AS int)
        FROM STRING_SPLIT(@Filtr_StrojSklad, '''','''')
        WHERE TRY_CAST(LTRIM(RTRIM(value)) AS int) IS NOT NULL;
    END

    IF OBJECT_ID(''''tempdb..#FiltrLokace'''') IS NOT NULL DROP TABLE #FiltrLokace;
    CREATE TABLE #FiltrLokace (LOCNCODE nvarchar(50) NOT NULL PRIMARY KEY);

    IF @Filtr_StrojLokace IS NOT NULL
    BEGIN
        INSERT INTO #FiltrLokace (LOCNCODE)
        SELECT DISTINCT LTRIM(RTRIM(value))
        FROM STRING_SPLIT(@Filtr_StrojLokace, '''','''')
        WHERE NULLIF(LTRIM(RTRIM(value)), '''''''') IS NOT NULL;
    END

    IF OBJECT_ID(''''tempdb..#FiltrZdroj'''') IS NOT NULL DROP TABLE #FiltrZdroj;
    CREATE TABLE #FiltrZdroj (ZDROJ char(1) NOT NULL PRIMARY KEY);

    IF @Filtr_Zdroj IS NOT NULL
    BEGIN
        INSERT INTO #FiltrZdroj (ZDROJ)
        SELECT DISTINCT LEFT(LTRIM(RTRIM(value)), 1)
        FROM STRING_SPLIT(@Filtr_Zdroj, '''','''')
        WHERE NULLIF(LTRIM(RTRIM(value)), '''''''') IS NOT NULL;
    END

    IF OBJECT_ID(''''tempdb..#CisloSluzby'''') IS NOT NULL DROP TABLE #CisloSluzby;
    CREATE TABLE #CisloSluzby (ID_group int NOT NULL PRIMARY KEY);

    IF @CisloSluzby IS NOT NULL
    BEGIN
        INSERT INTO #CisloSluzby (ID_group)
        SELECT DISTINCT TRY_CAST(LTRIM(RTRIM(value)) AS int)
        FROM STRING_SPLIT(@CisloSluzby, '''','''')
        WHERE TRY_CAST(LTRIM(RTRIM(value)) AS int) IS NOT NULL;
    END

    DECLARE @sql nvarchar(max);

    SET @sql = N''''
;WITH A AS
(
    SELECT 
        ''''''''A'''''''' AS ZDROJ,
        MSH.DateModified AS DATUM,
        MSH.S1 AS S1,
        MSH.S2 AS S2,
        MSH.S3 AS S3,
        MSH.S4 AS S4,
        MSH.S5 AS S5,
        MSH.S6 AS S6,
        MSH.S7 AS S7,
        MSH.LastError AS LastError,
        MSH.S8 AS S8,
        MSH.S9 AS S9,
        MSH.S10 AS S10,
        MSH.S11 AS S11,
        MSH.S0 AS S0,
        MSH.counter_0 AS counter_0,
        MSH.counter_1 AS counter_1,
        MSH.counter_2 AS counter_2,
        MSH.counter_3 AS counter_3,
        MSH.counter_4 AS counter_4,
        MSH.counter_5 AS counter_5,
        MSH.counter_6 AS counter_6,
        MSH.counter_7 AS counter_7,
        MSH.counter_8 AS counter_8,
        MSH.counter_9 AS counter_9,
        MSH.counter_10 AS counter_10,
        MSH.counter_11 AS counter_11,
        M.name AS STROJ,
        MSH.ID_group AS ID_group,
        NULL AS Popis_pol,
        CAST(NULL AS decimal(18,4)) AS Mnozstvi,
        NULL AS [CountEntries],
        NULL AS [SOPNUMBE],
        NULL AS [ITEMNMBR],
        NULL AS [ITEMTYPE],
        NULL AS [ITEMMJ],
        NULL AS [ORD],
        NULL AS [TIMEMODE],
        NULL AS [TIMEPREPSTART],
        NULL AS [TIMEPREP],
        NULL AS [TIMEUNIT],
        NULL AS [TIMESTART],
        NULL AS [TIMESTOP],
        NULL AS [TIMECORSTART],
        NULL AS [TIMECORSTOP],
        NULL AS [TIMECOR],
        NULL AS [TIMECRID],
        NULL AS [TIMECRIDTYPE],
        9000000 + ROW_NUMBER() OVER (ORDER BY MSH.DateModified, MSH.ID_group) AS [id],
        NULL AS [loginid],
        NULL AS [machineid],
        NULL AS [operationid],
        NULL AS [dateeve],
        NULL AS [qtyReal],
        NULL AS [QTYPACK],
        NULL AS [QTYPACKMJ],
        NULL AS [description],
        NULL AS [BarcodeP],
        NULL AS [UserID],
        NULL AS [TermID],
        NULL AS [ISOK],
        NULL AS [GUID],
        NULL AS [SOUBEHGUID],
        NULL AS [CORRGUID],
        NULL AS [qtyOld],
        NULL AS [idVS],
        NULL AS [dateedit],
        NULL AS [SKL_ID],
        NULL AS [LOCNCODE],
        NULL AS [SERLTNUM],
        NULL AS [EXPIRATION],
        NULL AS [NMBRPAL],
        NULL AS [TYPEPAL],
        NULL AS [PackType],
        NULL AS [status],
        NULL AS [WEIGHT],
        NULL AS [STORNOGUID],
        NULL AS [REZ_1],
        NULL AS [REZ_2],
        NULL AS [REZ_3],
        NULL AS [REZ_4],
        NULL AS [REZ_5],
        NULL AS [WEIGHT_OLD],
        NULL AS [TIMEPREPSTOP],
        MD.SKL_ID AS StrojSklad,
        MD.LOCNCODE AS StrojLokace,
        CAST(NULL AS decimal(18,3)) AS TIMEOPER
    FROM [dbo].[MachineStateSetHistory] MSH
    LEFT JOIN [dbo].[MachinesDefinition] MD ON MD.IP = MSH.IP
    LEFT JOIN [dbo].[Machines] M ON MD.id = M.id
    WHERE 1 = 1
      AND (@DateModifiedOD IS NULL OR MSH.DateModified >= @DateModifiedOD)
      AND (@DateModifiedDO IS NULL OR MSH.DateModified <= @DateModifiedDO)
      AND (@DateModified_TV IS NULL OR MSH.DateModified > @DateModified_TV)
      AND (
            @CisloSluzby IS NULL
            OR EXISTS (SELECT 1 FROM #CisloSluzby cs WHERE cs.ID_group = MD.ID_group)
          )
      AND (@Description IS NULL OR MD.Description = @Description)
      AND (@S0  IS NULL OR MSH.S0  = @S0)
      AND (@S1  IS NULL OR MSH.S1  = @S1)
      AND (@S2  IS NULL OR MSH.S2  = @S2)
      AND (@S3  IS NULL OR MSH.S3  = @S3)
      AND (@S4  IS NULL OR MSH.S4  = @S4)
      AND (@S5  IS NULL OR MSH.S5  = @S5)
      AND (@S6  IS NULL OR MSH.S6  = @S6)
      AND (@S7  IS NULL OR MSH.S7  = @S7)
      AND (@S8  IS NULL OR MSH.S8  = @S8)
      AND (@S9  IS NULL OR MSH.S9  = @S9)
      AND (@S10 IS NULL OR MSH.S10 = @S10)
      AND (@S11 IS NULL OR MSH.S11 = @S11)
      AND (
            @Filtr_StrojSklad IS NULL
            OR EXISTS (SELECT 1 FROM #FiltrSklad fs WHERE fs.SKL_ID = MD.SKL_ID)
          )
      AND (
            @Filtr_StrojLokace IS NULL
            OR EXISTS (SELECT 1 FROM #FiltrLokace fl WHERE fl.LOCNCODE = MD.LOCNCODE)
          )
),
B AS
(
    SELECT
        ''''''''B'''''''' AS ZDROJ,
        CASE
            WHEN P.[TIMESTART] IS NOT NULL AND P.[TIMESTOP] IS NOT NULL THEN P.[TIMESTOP]
            WHEN P.[TIMESTART] IS NOT NULL AND P.[TIMESTOP] IS NULL THEN P.[TIMESTART]
            ELSE P.[dateeve]
        END AS DATUM,
        CAST(NULL AS int) AS S1,
        CAST(NULL AS int) AS S2,
        CAST(NULL AS int) AS S3,
        CAST(NULL AS int) AS S4,
        CAST(NULL AS int) AS S5,
        CAST(NULL AS int) AS S6,
        CAST(NULL AS int) AS S7,
        NULL AS LastError,
        CAST(NULL AS int) AS S8,
        CAST(NULL AS int) AS S9,
        CAST(NULL AS int) AS S10,
        CAST(NULL AS int) AS S11,
        CAST(NULL AS int) AS S0,
        NULL AS counter_0,
        NULL AS counter_1,
        NULL AS counter_2,
        NULL AS counter_3,
        NULL AS counter_4,
        NULL AS counter_5,
        NULL AS counter_6,
        NULL AS counter_7,
        NULL AS counter_8,
        NULL AS counter_9,
        NULL AS counter_10,
        NULL AS counter_11,
        M.name AS STROJ,
        NULL AS ID_group,
        P.ITEMDESC AS Popis_pol,
        CAST(P.qty AS decimal(18,4)) AS Mnozstvi,
        P.[CountEntries],
        P.[SOPNUMBE],
        P.[ITEMNMBR],
        P.[ITEMTYPE],
        P.[ITEMMJ],
        P.[ORD],
        P.[TIMEMODE],
        P.[TIMEPREPSTART],
        P.[TIMEPREP],
        P.[TIMEUNIT],
        P.[TIMESTART],
        P.[TIMESTOP],
        P.[TIMECORSTART],
        P.[TIMECORSTOP],
        P.[TIMECOR],
        P.[TIMECRID],
        P.[TIMECRIDTYPE],
        8000000 + P.[id] AS [id],
        P.[loginid],
        P.[machineid],
        P.[operationid],
        P.[dateeve],
        P.[qtyReal],
        P.[QTYPACK],
        P.[QTYPACKMJ],
        P.[description],
        P.[BarcodeP],
        P.[UserID],
        P.[TermID],
        P.[ISOK],
        P.[GUID],
        P.[SOUBEHGUID],
        P.[CORRGUID],
        P.[qtyOld],
        P.[idVS],
        P.[dateedit],
        P.[SKL_ID],
        P.[LOCNCODE],
        P.[SERLTNUM],
        P.[EXPIRATION],
        P.[NMBRPAL],
        P.[TYPEPAL],
        P.[PackType],
        P.[status],
        P.[WEIGHT],
        P.[STORNOGUID],
        P.[REZ_1],
        P.[REZ_2],
        P.[REZ_3],
        P.[REZ_4],
        VPP.ITEMDESC AS REZ_5,
        P.[WEIGHT_OLD],
        P.[TIMEPREPSTOP],
        M.SKL_ID AS StrojSklad,
        M.LOCNCODE AS StrojLokace,
        CASE
            WHEN P.TIMESTART IS NOT NULL AND P.TIMESTOP IS NOT NULL
            THEN CAST(DATEDIFF_BIG(second, P.TIMESTART, P.TIMESTOP) / 60.0 AS decimal(18,3))
            ELSE NULL
        END AS TIMEOPER
    FROM [dbo].[Production] P
    LEFT JOIN [dbo].[Machines] M ON M.id = P.machineid
    LEFT JOIN [dbo].[CZPRO_VPP] VPP
           ON VPP.CountEntries = P.CountEntries
          AND VPP.SOPNUMBE = P.SOPNUMBE
          AND VPP.ORD = P.ORD
		  AND VPP.ITEMNMBR = P.ITEMNMBR
    WHERE 1 = 1
      AND (@Description IS NULL OR M.name = @Description)
      AND (@DateModifiedOD IS NULL OR P.dateeve >= @DateModifiedOD)
      AND (@DateModifiedDO IS NULL OR P.dateeve <= @DateModifiedDO)
      AND (@DateModified_TV IS NULL OR P.dateeve > @DateModified_TV)
      AND (
            @Filtr_StrojSklad IS NULL
            OR EXISTS (SELECT 1 FROM #FiltrSklad fs WHERE fs.SKL_ID = M.SKL_ID)
          )
      AND (
            @Filtr_StrojLokace IS NULL
            OR EXISTS (SELECT 1 FROM #FiltrLokace fl WHERE fl.LOCNCODE = M.LOCNCODE)
          )
),
U AS
(
    SELECT * FROM A
    UNION ALL
    SELECT * FROM B
),
ORD AS
(
    SELECT
        U.*,
        ROW_NUMBER() OVER
        (
            PARTITION BY U.STROJ
            ORDER BY
                U.DATUM ASC,
                CASE WHEN U.ZDROJ = ''''''''A'''''''' THEN 0 ELSE 1 END,
                ISNULL(U.[id], 0),
                ISNULL(U.[CountEntries], 0),
                ISNULL(CONVERT(nvarchar(36), U.[GUID]), N''''''''''''''''),
                ISNULL(U.[BarcodeP], N'''''''''''''''')
        ) AS RN_ASC
    FROM U
),
BASE AS
(
    SELECT
        O.*,
        SUM(CASE WHEN O.ZDROJ = ''''''''B'''''''' THEN ISNULL(O.Mnozstvi, 0) ELSE 0 END) OVER
        (
            PARTITION BY O.STROJ
            ORDER BY O.RN_ASC
            ROWS UNBOUNDED PRECEDING
        ) AS RunMnozstviGlobal
    FROM ORD O
),
ANCHORS AS
(
    SELECT
        B.STROJ,
        B.RN_ASC AS AnchorRN,
        CAST(B.counter_11 AS decimal(18,3)) AS AnchorCounter11,
        B.RunMnozstviGlobal AS AnchorRunMnoz
    FROM BASE B
    WHERE B.ZDROJ = ''''''''A''''''''
      AND B.counter_11 IS NOT NULL
),
V AS
(
    SELECT
        B.*,

        ROW_NUMBER() OVER
        (
            ORDER BY
                B.DATUM DESC,
                B.STROJ DESC,
                CASE WHEN B.ZDROJ = ''''''''B'''''''' THEN 1 ELSE 0 END DESC,
                ISNULL(B.[id], 0) DESC,
                ISNULL(B.[CountEntries], 0) DESC,
                ISNULL(CONVERT(nvarchar(36), B.[GUID]), N'''''''''''''''') DESC,
                ISNULL(B.[BarcodeP], N'''''''''''''''') DESC
        ) AS DisplayOrder,

        DATEDIFF_BIG(
            second,
            LAG(B.DATUM) OVER (
                PARTITION BY B.STROJ
                ORDER BY B.RN_ASC
            ),
            B.DATUM
        ) AS DATUM_Rozdil_Sec,

        CAST(
            DATEDIFF_BIG(
                second,
                LAG(B.DATUM) OVER (
                    PARTITION BY B.STROJ
                    ORDER BY B.RN_ASC
                ),
                B.DATUM
            ) / 60.0
        AS decimal(18,3)) AS DATUM_Rozdil_Min,

        CAST(
            DATEDIFF_BIG(
                second,
                CASE 
                    WHEN @DateModifiedOD IS NOT NULL 
                    THEN @DateModifiedOD
                    ELSE MIN(B.DATUM) OVER (PARTITION BY B.STROJ)
                END,
                B.DATUM
            ) / 60.0
        AS decimal(18,3)) AS Delta_Minut,

        CASE
            WHEN PA.AnchorCounter11 IS NOT NULL THEN
                CAST(
                    PA.AnchorCounter11
                    - (B.RunMnozstviGlobal)
                AS decimal(18,3))

            WHEN PA.AnchorCounter11 IS NULL THEN
                CAST(0 - B.RunMnozstviGlobal AS decimal(18,3))

            ELSE NULL
        END AS Delta_Mnozstvi
    FROM BASE B
    OUTER APPLY
    (
        SELECT TOP 1
            A.AnchorCounter11,
            A.AnchorRunMnoz
        FROM ANCHORS A
        WHERE A.STROJ = B.STROJ
          AND A.AnchorRN <= B.RN_ASC
        ORDER BY A.AnchorRN DESC
    ) PA
)

SELECT *
FROM V U
WHERE 1 = 1
  AND (
        @Filtr_Zdroj IS NULL
        OR EXISTS (SELECT 1 FROM #FiltrZdroj fz WHERE fz.ZDROJ = U.ZDROJ)
      )
'''';

    IF @Filtr_users IS NOT NULL
    BEGIN
        IF @Filtr_users LIKE N''''%;%'''' OR
           @Filtr_users LIKE N''''%--%'''' OR
           @Filtr_users LIKE N''''%/*%'''' OR
           @Filtr_users LIKE N''''%*/%'''' OR
           @Filtr_users LIKE N''''%xp_%'''' OR
           @Filtr_users LIKE N''''%exec%'''' OR
           @Filtr_users LIKE N''''%execute%'''' OR
           @Filtr_users LIKE N''''%union%'''' OR
           @Filtr_users LIKE N''''%insert%'''' OR
           @Filtr_users LIKE N''''%update%'''' OR
           @Filtr_users LIKE N''''%delete%'''' OR
           @Filtr_users LIKE N''''%merge%'''' OR
           @Filtr_users LIKE N''''%drop%'''' OR
           @Filtr_users LIKE N''''%alter%'''' OR
           @Filtr_users LIKE N''''%create%''''
        BEGIN
           THROW 50001, ''''Nepovoleny obsah ve @Filtr_users.'''', 1;
        END;

        SET @sql += N'''' AND ('''' + @Filtr_users + N'''')'''';
    END

    SET @sql += N''''
ORDER BY
    DisplayOrder ASC
OPTION (RECOMPILE);
'''';

    EXEC sp_executesql
        @sql,
        N''''
          @DateModifiedOD datetime,
          @DateModifiedDO datetime,
          @DateModified_TV datetime,
          @CisloSluzby nvarchar(max),
          @Description nvarchar(100),
          @S0 int, @S1 int, @S2 int, @S3 int, @S4 int, @S5 int, @S6 int, @S7 int, @S8 int, @S9 int, @S10 int, @S11 int,
          @Filtr_Zdroj nvarchar(max),
          @Filtr_StrojSklad nvarchar(max),
          @Filtr_StrojLokace nvarchar(max)
        '''',
        @DateModifiedOD    = @DateModifiedOD,
        @DateModifiedDO    = @DateModifiedDO,
        @DateModified_TV   = @DateModified_TV,
        @CisloSluzby       = @CisloSluzby,
        @Description       = @Description,
        @S0                = @S0,
        @S1                = @S1,
        @S2                = @S2,
        @S3                = @S3,
        @S4                = @S4,
        @S5                = @S5,
        @S6                = @S6,
        @S7                = @S7,
        @S8                = @S8,
        @S9                = @S9,
        @S10               = @S10,
        @S11               = @S11,
        @Filtr_Zdroj       = @Filtr_Zdroj,
        @Filtr_StrojSklad  = @Filtr_StrojSklad,
        @Filtr_StrojLokace = @Filtr_StrojLokace;
END'';

-- Installation step 380
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Jedná se o proceduru sloužící pro vytvořenípředlohy na Výdej z Typu Dokladu TO
-- =============================================
CREATE PROCEDURE [dbo].[fask_proc_DI2SE] 
	@p1 nvarchar(20),
	@p2 nvarchar(20)
	AS
BEGIN
	SET NOCOUNT ON;

	-- zjistíme jakou akci provádíme
	DECLARE @coSeDeje varchar(12) = (SELECT TOP 1 [DOC_ID] FROM [CZMST_DI] WHERE [CountEntries] = @p2)
	DECLARE @MaxSE int = (SELECT TOP 1 MAX(CountEntries) FROM [CZMST_SE])

	DECLARE @CountEntries int;

	set @CountEntries = ISNULL(@MaxSE,0) + 1;


		if @coSeDeje = ''''TO'''' begin
		-- TO, přegenerovani DI do SE

INSERT INTO CZMST_SE
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ITEMTYPE]
           ,[ITEMDESC]
           ,[VNDDOCNM]
           ,[VNDITNUM]
           ,[ORD]
           ,[CZ_CarKod]
           ,[SKL_ID]
           ,[LOCNCODE]
           ,[MJ]
           ,[QTYSHPPD]
           ,[QTYPACK]
           ,[CZ_DatVyr_Track]
           ,[CZ_DatVyr_Delka]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_SW_Track]
           ,[CZ_SW_Delka]
           ,[CZ_Doslo]
           ,[Note]
           ,[TYPEPAL]
           ,[QTYPAL]
           ,[PRIORITY]
           ,[PRINTED]
           ,[USERID]
           ,[CZ_REZ1_Track]
           ,[CZ_REZ2_Track]
           ,[ITEMCODE]
           ,[WEIGHT])
SELECT
			@CountEntries as CountEntries, 
			DI.DOC_ID + CONVERT(nvarchar(100),DI.CountEntries) as SOPNUMBE,
			DI.ITEMNMBR,
			'''''''' as ITEMTYPE,
			Z.ITEMDESC,
			'''''''' as VNDDOCNM,
			DI.VNDITNUM,
			DI.DEX_ROW_ID as ORD,
			DI.CZ_CarKod,
			DI.SKL_ID,
			DI.LOCNCODE,
			DI.MJ,
			DI.QTYSHPPD,
			DI.QTYPACK,
			0 as CZ_DatVyr_Track,
			0 as CZ_DatVyr_Delka,
			Z.CZ_SerNum_Track,
			Z.CZ_SerNum_Delka,
			0 as CZ_SW_Track,
			0 as CZ_SW_Delka,
			0 as CZ_Doslo,
			'''''''' as Note,
			'''''''' as TYPEPAL,
			0 as QTYPAL,
			0 as PRIORITY,
			0 as PRINTED,
			DI.USER_ID,
			0 as CZ_REZ1_Track,
			0 as CZ_REZ2_Track,
			DI.ITEMCODE,
			DI.WEIGHT
FROM CZMST_DI as DI LEFT JOIN FASK_ZASOBY AS Z ON Z.SKL_ID = DI.SKL_ID AND Z.ITEMNMBR = DI.ITEMNMBR
WHERE DI.CountEntries = @p2


INSERT INTO [CZMST_SE_SN]
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ORD]
           ,[SERLNMBR]
           ,[QTY])
SELECT
			@CountEntries as CountEntries, 
			DI.DOC_ID + CONVERT(nvarchar(100),DI.CountEntries) as SOPNUMBE,
			DI.ITEMNMBR,
			DI.DEX_ROW_ID as ORD,
			DI.SERLTNUM,
			DI.QTYSHPPD
FROM CZMST_DI AS DI
WHERE DI.SERLTNUM IS NOT NULL AND DI.SERLTNUM != '''''''' AND DI.CountEntries = @p2

	end
	-- neznámý cosedeje ;)
	else begin
		return -1
	end

END'';

-- Installation step 381
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Tadeas Divacky
-- Create date:		25.8.2020 
-- Description:	Jedná se o proceduru sloužící pro hromadnou editaci Funkci a Procedur
-- =============================================
CREATE PROCEDURE [dbo].[fask_proc_EditFuncProc] 
	@TEXT_OLD nvarchar(100),
	@TEXT_NEW nvarchar(100)
	AS
BEGIN
	SET NOCOUNT ON;


DECLARE @sp_names TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(MAX)
);

--Pomocná tabulka
DECLARE @HelpText TABLE
(
    Val NVARCHAR(MAX)
);


--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
        @sp_name NVARCHAR(128),
        @text NVARCHAR(MAX);

INSERT  @sp_names
SELECT
    sch.name+''''.''''+ob.name AS Name
FROM 
     sys.objects AS ob
     LEFT JOIN sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL
AND ob.type_desc in (
''''SQL_INLINE_TABLE_VALUED_FUNCTION'''',
''''SQL_SCALAR_FUNCTION'''',
''''SQL_TABLE_VALUED_FUNCTION'''',
''''SQL_STORED_PROCEDURE''''
)

SET @sp_count = (SELECT COUNT(1) FROM @sp_names)

--Cyklus přes všechny
WHILE (@sp_count > @count)
BEGIN
    SET @count = @count + 1; -- inkrement pro projiti všech procedur
    SET @text = N''''''''; -- Prazdny text

	--Vytažení Name procedruz podle jedinečneho ID pořadí
    SET @sp_name = (SELECT  name
                    FROM    @sp_names
                    WHERE   ID = @count);

	-- Vytažení Obsahu textu tela procedury
    INSERT INTO @HelpText
    EXEC sp_HelpText @sp_name;

	--Vytažení obsahu  textu procedury
    SELECT  @text = COALESCE(@text + '''' '''' + Val, Val)
    FROM    @HelpText;

	--Smazani tmp promenne 
    DELETE FROM @HelpText;


    IF @text LIKE ''''%'''' + @TEXT_OLD + ''''%''''
    BEGIN
		IF @text LIKE ''''%CREATE PROCEDURE%''''
			BEGIN
				SET @text = REPLACE(@text, ''''CREATE PROCEDURE'''', ''''ALTER PROCEDURE'''');
			END
		ELSE IF @text LIKE ''''%CREATE FUNCTION%''''
			BEGIN
				SET @text = REPLACE(@text, ''''CREATE FUNCTION'''', ''''ALTER FUNCTION'''');
			END

			SET @text = REPLACE(@text, @TEXT_OLD, @TEXT_NEW);

			EXECUTE sp_executesql @text;
    END
END

END'';

-- Installation step 382
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_LOKACE]
    @DatabaseName nvarchar(100)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* Povinný @DatabaseName */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* DB musí existovat */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    -- Smazání číselníku lokací
    DELETE FROM dbo.CZMST094;

    DECLARE @SQL nvarchar(max);

    SET @SQL = N''''
        INSERT INTO dbo.CZMST094
        (
            SKL_ID,
            LOCNCODE,
            TYPE,
            [Description],
            Barcode
        )
        SELECT
            SKL_ID,
            LOCNCODE,
            TYPE,
            [Description],
            Barcode
        FROM '''' + QUOTENAME(@DatabaseName) + N''''.dbo.CZMST_SkladLokace_Mapa;
    '''';

    EXEC sp_executesql @SQL;
END'';

-- Installation step 383
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 26.6.2025
 -- Description:	
 -- =============================================
 CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_MENY] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';


 
 	DELETE  FROM CZMST097





 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST097]
            ([mena_ID]
            ,[mena_text])
 SELECT 
 ID as mena_ID,
 LEFT(Kod,30) as mena_text
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.sCMeny WHERE (Pouzit = 1)
 '''';

 EXEC sp_executesql @SQL;

 END
 
 /**************************************************************************************/
 /****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_ODBERATELE]    ******/
 SET ANSI_NULLS ON'';

-- Installation step 384
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_NacteniOPlanu]
(
      @PohodaDBName        sysname

    , @Nezaplanovane       bit
    , @PohodaE1            bit   -- 1 = sloupec VPrQTY existuje, 0 = neexistuje

    , @OD_DatumOD          datetime
    , @OD_DatumDO          datetime
    , @OD_DatumOD_Check    bit
    , @OD_DatumDO_Check    bit

    , @DO_DatumOD          datetime
    , @DO_DatumDO          datetime
    , @DO_DatumOD_Check    bit
    , @DO_DatumDO_Check    bit

    , @ZAP_DatumOD         datetime
    , @ZAP_DatumDO         datetime
    , @ZAP_DatumOD_Check   bit
    , @ZAP_DatumDO_Check   bit

    , @OBJ                 nvarchar(50)      = NULL
    , @Kod                 nvarchar(50)      = NULL
    , @Firma               nvarchar(100)     = NULL
    , @FormaUhrady         nvarchar(200)     = NULL  -- CSV: ''''1,2,3''''

    , @UserParam_1         nvarchar(255)     = NULL
    , @UserParam_2         nvarchar(255)     = NULL
    , @UserParam_3         nvarchar(255)     = NULL
    , @UserParam_4         nvarchar(255)     = NULL
    , @UserParam_5         nvarchar(255)     = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL       nvarchar(MAX);
    DECLARE @DbNameQ   sysname;
    DECLARE @QTYPart   nvarchar(MAX);

    ----------------------------------------------------------------------
    -- NORMALIZACE PRÁZDNÝCH STRINGŮ NA NULL
    ----------------------------------------------------------------------
    IF (@OBJ         IS NOT NULL AND LTRIM(RTRIM(@OBJ))         = '''''''') SET @OBJ = NULL;
    IF (@Kod         IS NOT NULL AND LTRIM(RTRIM(@Kod))         = '''''''') SET @Kod = NULL;
    IF (@Firma       IS NOT NULL AND LTRIM(RTRIM(@Firma))       = '''''''') SET @Firma = NULL;
    IF (@FormaUhrady IS NOT NULL AND LTRIM(RTRIM(@FormaUhrady)) = '''''''') SET @FormaUhrady = NULL;

    IF (@UserParam_1 IS NOT NULL AND LTRIM(RTRIM(@UserParam_1)) = '''''''') SET @UserParam_1 = NULL;
    IF (@UserParam_2 IS NOT NULL AND LTRIM(RTRIM(@UserParam_2)) = '''''''') SET @UserParam_2 = NULL;
    IF (@UserParam_3 IS NOT NULL AND LTRIM(RTRIM(@UserParam_3)) = '''''''') SET @UserParam_3 = NULL;
    IF (@UserParam_4 IS NOT NULL AND LTRIM(RTRIM(@UserParam_4)) = '''''''') SET @UserParam_4 = NULL;
    IF (@UserParam_5 IS NOT NULL AND LTRIM(RTRIM(@UserParam_5)) = '''''''') SET @UserParam_5 = NULL;

    ----------------------------------------------------------------------
    -- NÁZEV POHODA DATABÁZE
    ----------------------------------------------------------------------
    SET @DbNameQ = QUOTENAME(@PohodaDBName);

    ----------------------------------------------------------------------
    -- QTY / QTY_Zaplanovano / QTY_Zbyva PODLE VERZE POHODY
    ----------------------------------------------------------------------
    IF (@PohodaE1 = 1)
    BEGIN
        SET @QTYPart = N''''
            , op.Mnozstvi         AS QTY
            , op.VPrQTY           AS QTY_Zaplanovano
            , (op.Mnozstvi - ISNULL(op.VPrQTY,0)) AS QTY_Zbyva'''';
    END
    ELSE
    BEGIN
        -- VPrQTY neexistuje → všechno je "nezapl.", QTY_Zaplanovano = NULL, QTY_Zbyva = celé množství
        SET @QTYPart = N''''
            , op.Mnozstvi         AS QTY
            , CAST(NULL AS decimal(18,4)) AS QTY_Zaplanovano
            , op.Mnozstvi         AS QTY_Zbyva'''';
    END

    BEGIN TRY

        ------------------------------------------------------------------
        -- ZÁKLAD SELECTU
        ------------------------------------------------------------------
        SET @SQL = N''''
        SELECT 
              op.RefSKz           AS ITEMNMBR
            , op.SText            AS ITEMDESC
            , op.Kod              AS ITEMCODE

            , o.Firma             AS OBJ_COMPANY

            , o.Cislo             AS OBJ_NMBR
            , o.SText             AS OBJ_DESC
            , o.RelTpObj          AS OBJ_TYPE
        '''' + @QTYPart + N''''

            , o.ID                AS OBJ_ORD
            , op.ID               AS OBJ_ITEM_ORD

            , o.DatOd             AS OBJ_DATE_FROM
            , o.DatDo             AS OBJ_DATE_TO

            , o.Datum             AS OBJ_DATE_ZAPL

            , o.RelForUh          AS OBJ_ForUh
            , fu.IDS              AS OBJ_ForUh_IDS

            , x.UserParam_1       AS UserParam_1
            , x.UserParam_2       AS UserParam_2
            , x.UserParam_3       AS UserParam_3
            , x.UserParam_4       AS UserParam_4
            , x.UserParam_5       AS UserParam_5

            -- plánování v našich tabulkách
            , pv.PLAN_QTY         AS PLAN_QTY
            , CASE 
                  WHEN pv.PLAN_QTY IS NULL                 THEN 0  -- není v našich tabulkách
                  WHEN pv.PLAN_QTY = op.Mnozstvi           THEN 1  -- stejné množství
                  ELSE 2                                        -- rozdílné množství
              END              AS PLAN_FLAG

        FROM '''' + @DbNameQ + N''''.dbo.OBJ      AS o
        LEFT JOIN '''' + @DbNameQ + N''''.dbo.OBJpol   AS op ON op.RefAg = o.ID
        LEFT JOIN '''' + @DbNameQ + N''''.dbo.sFormUh  AS fu ON fu.ID = o.RelForUh

        LEFT JOIN (
            SELECT ORD, UserParam_1, UserParam_2, UserParam_3, UserParam_4, UserParam_5
            FROM dbo.FASK_Get_Planovani_NacteniUserParams()
        ) x ON x.ORD = op.ID

        -- agregace z našich tabulek plánování
        LEFT JOIN (
            SELECT 
                  ITEMNMBR
                , OBJ_NMBR
                , SUM(QTY) AS PLAN_QTY
            FROM dbo.FASK_Vyroba_PVP
            GROUP BY ITEMNMBR, OBJ_NMBR
        ) pv
            ON pv.ITEMNMBR = op.RefSKz
           AND pv.OBJ_NMBR = o.Cislo

        WHERE 
            o.RelTpObj = 1
            AND o.Vyrizeno = 0
            AND op.RefSKz IS NOT NULL
        '''';

        ------------------------------------------------------------------
        -- DYNAMICKÉ DOPLNĚNÍ FILTRŮ (beze změny)
        ------------------------------------------------------------------

        -- Nezaplanovane
        IF (@Nezaplanovane = 1 AND @PohodaE1 = 1)
        BEGIN
            -- jen v E1, kde existuje VPrQTY
            SET @SQL += N''''
            AND (op.VPrQTY IS NULL OR op.VPrQTY <> op.Mnozstvi)'''';
        END

		  -- Nezaplanovane podle našich plánů (vyhodit zelené = plně shodné)
        IF (@Nezaplanovane = 1)
        BEGIN
            SET @SQL += N''''
            AND (pv.PLAN_QTY IS NULL OR pv.PLAN_QTY <> op.Mnozstvi)'''';
        END

        -- OBJ (číslo dokladu)
        IF (@OBJ IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.Cislo LIKE @OBJ + ''''''''%'''''''''''';
        END

        -- Kód položky
        IF (@Kod IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND op.Kod LIKE @Kod + ''''''''%'''''''''''';
        END

        -- Firma
        IF (@Firma IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.Firma LIKE @Firma + ''''''''%'''''''''''';
        END

        -- Forma úhrady (CSV seznam ID)
        IF (@FormaUhrady IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.RelForUh IN (
                SELECT TRY_CAST(value AS int)
                FROM STRING_SPLIT(@FormaUhrady, '''''''','''''''')
            )'''';
        END

        -- Datum OD (o.DatOd)
        IF (@OD_DatumOD_Check = 1 AND @OD_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd BETWEEN @OD_DatumOD AND @OD_DatumDO'''';
        END
        ELSE IF (@OD_DatumDO_Check = 1 AND @OD_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd <= @OD_DatumDO'''';
        END
        ELSE IF (@OD_DatumDO_Check = 0 AND @OD_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd >= @OD_DatumOD'''';
        END

        -- Datum DO (o.DatDo)
        IF (@DO_DatumOD_Check = 1 AND @DO_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo BETWEEN @DO_DatumOD AND @DO_DatumDO'''';
        END
        ELSE IF (@DO_DatumDO_Check = 1 AND @DO_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo <= @DO_DatumDO'''';
        END
        ELSE IF (@DO_DatumDO_Check = 0 AND @DO_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo >= @DO_DatumOD'''';
        END

        -- Datum ZAP (o.Datum)
        IF (@ZAP_DatumOD_Check = 1 AND @ZAP_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.Datum BETWEEN @ZAP_DatumOD AND @ZAP_DatumDO'''';
        END
        ELSE IF (@ZAP_DatumDO_Check = 1 AND @ZAP_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.Datum <= @ZAP_DatumDO'''';
        END
        ELSE IF (@ZAP_DatumDO_Check = 0 AND @ZAP_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.Datum >= @ZAP_DatumOD'''';
        END

        -- UserParam_1–5
        IF (@UserParam_1 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_1 = @UserParam_1'''';
        END
        IF (@UserParam_2 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_2 = @UserParam_2'''';
        END
        IF (@UserParam_3 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_3 = @UserParam_3'''';
        END
        IF (@UserParam_4 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_4 = @UserParam_4'''';
        END
        IF (@UserParam_5 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_5 = @UserParam_5'''';
        END

        -- ORDER BY
        SET @SQL += N''''
        ORDER BY 
            o.Cislo;'''';

        ------------------------------------------------------------------
        -- EXECUTE
        ------------------------------------------------------------------
        EXEC sp_executesql
            @SQL,
            N''''
              @Nezaplanovane       bit

            , @OD_DatumOD          datetime
            , @OD_DatumDO          datetime
            , @OD_DatumOD_Check    bit
            , @OD_DatumDO_Check    bit

            , @DO_DatumOD          datetime
            , @DO_DatumDO          datetime
            , @DO_DatumOD_Check    bit
            , @DO_DatumDO_Check    bit

            , @ZAP_DatumOD         datetime
            , @ZAP_DatumDO         datetime
            , @ZAP_DatumOD_Check   bit
            , @ZAP_DatumDO_Check   bit

            , @OBJ                 nvarchar(50)
            , @Kod                 nvarchar(50)
            , @Firma               nvarchar(100)
            , @FormaUhrady         nvarchar(200)

            , @UserParam_1         nvarchar(255)
            , @UserParam_2         nvarchar(255)
            , @UserParam_3         nvarchar(255)
            , @UserParam_4         nvarchar(255)
            , @UserParam_5         nvarchar(255)
            '''',
            @Nezaplanovane     = @Nezaplanovane,

            @OD_DatumOD        = @OD_DatumOD,
            @OD_DatumDO        = @OD_DatumDO,
            @OD_DatumOD_Check  = @OD_DatumOD_Check,
            @OD_DatumDO_Check  = @OD_DatumDO_Check,

            @DO_DatumOD        = @DO_DatumOD,
            @DO_DatumDO        = @DO_DatumDO,
            @DO_DatumOD_Check  = @DO_DatumOD_Check,
            @DO_DatumDO_Check  = @DO_DatumDO_Check,

            @ZAP_DatumOD       = @ZAP_DatumOD,
            @ZAP_DatumDO       = @ZAP_DatumDO,
            @ZAP_DatumOD_Check = @ZAP_DatumOD_Check,
            @ZAP_DatumDO_Check = @ZAP_DatumDO_Check,

            @OBJ               = @OBJ,
            @Kod               = @Kod,
            @Firma             = @Firma,
            @FormaUhrady       = @FormaUhrady,

            @UserParam_1       = @UserParam_1,
            @UserParam_2       = @UserParam_2,
            @UserParam_3       = @UserParam_3,
            @UserParam_4       = @UserParam_4,
            @UserParam_5       = @UserParam_5;
    END TRY
    BEGIN CATCH
        SELECT
              CAST(NULL AS nvarchar(50))   AS ITEMNMBR
            , CAST(NULL AS nvarchar(255))  AS ITEMDESC
            , CAST(NULL AS nvarchar(50))   AS ITEMCODE

            , CAST(NULL AS nvarchar(100))  AS OBJ_COMPANY

            , CAST(NULL AS nvarchar(50))   AS OBJ_NMBR
            , CAST(NULL AS nvarchar(255))  AS OBJ_DESC
            , CAST(NULL AS nvarchar(50))   AS OBJ_TYPE

            , CAST(NULL AS decimal(18,4))  AS QTY
            , CAST(NULL AS nvarchar(50))   AS QTY_Zaplanovano
            , CAST(NULL AS nvarchar(50))   AS QTY_Zbyva

            , CAST(NULL AS int)            AS OBJ_ORD
            , CAST(NULL AS int)            AS OBJ_ITEM_ORD

            , CAST(NULL AS datetime)       AS OBJ_DATE_FROM
            , CAST(NULL AS datetime)       AS OBJ_DATE_TO
            , CAST(NULL AS datetime)       AS OBJ_DATE_ZAPL

            , CAST(NULL AS int)            AS OBJ_ForUh
            , CAST(NULL AS nvarchar(50))   AS OBJ_ForUh_IDS

            , CAST(NULL AS nvarchar(255))  AS UserParam_1
            , CAST(NULL AS nvarchar(255))  AS UserParam_2
            , CAST(NULL AS nvarchar(255))  AS UserParam_3
            , CAST(NULL AS nvarchar(255))  AS UserParam_4
            , CAST(NULL AS nvarchar(255))  AS UserParam_5

            , CAST(NULL AS decimal(18,4))  AS PLAN_QTY
            , CAST(NULL AS int)           AS PLAN_FLAG
        WHERE 1 = 0;
    END CATCH
END'';

-- Installation step 385
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ODBERATELE]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST090

		 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';

	 SET @SQL = ''''
	INSERT INTO [dbo].[CZMST090]
           ([odb_id]
           ,[odb_desc]
           ,[odb_typ]
           ,[odb_carcode]
           ,[odb_ico]
           ,[mena_ID]
           ,[odb_misto]
           ,[odb_ulice]
           ,[odb_cisloOr]
           ,[odb_psc]
           ,[odb_dic]
           ,[odb_Odberatel]
           ,[odb_Dodavatel])
		SELECT 
		  left(ID, 12) as [odb_id]
		, left(Firma, 31) as [odb_desc]
		, 0 as [odb_typ]
		, left(Cislo, 21) as [odb_carcode]
		, left(ICO, 20) as [odb_ico]
		, left(RefCM, 10) as [mena_ID]
		, left(Obec, 100) as [odb_misto]
		, left(Ulice, 100) as [odb_ulice]
		, null as [odb_cisloOr]
		, left(PSC, 15) as [odb_psc]
		, left(DIC, 15) as [odb_dic]
		, p1 as [odb_Odberatel]
		, p2 as [odb_Dodavatel]
		FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.AD
'''';

EXEC sp_executesql @SQL;


END

/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_SKLAD]    ******/
SET ANSI_NULLS ON'';

-- Installation step 386
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_PlanovaniVyroby]
   @DatabaseName nvarchar(100)
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
      /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);


 -- -- JaS: smazani ciselniku lokaci
 --	DELETE  FROM CZMST094




 ---- JaS: pracovni select
 --SET @SQL = ''''
 --INSERT INTO [dbo].[CZMST094]
 --           ([SKL_ID]
 --          ,[LOCNCODE]
 --          ,[TYPE]
 --          ,[Description]
 --          ,[Barcode])
 --SELECT 
 --SKL_ID as SKL_ID,
 --LOCNCODE as LOCNCODE,
 --TYPE as TYPE,
 --Description as Description,
 --Barcode as Barcode
 --FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 --'''';

 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 387
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_Pracovnici]
    @DatabaseName nvarchar(100)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* Povinný @DatabaseName */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* DB musí existovat */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    /* Volitelně: ověření existence zdrojové tabulky v cílové DB */
    DECLARE @ObjId int;
    DECLARE @CheckSql nvarchar(max) =
        N''''SELECT @ObjIdOUT = OBJECT_ID('''''''''''' + REPLACE(@DatabaseName,'''''''''''''''','''''''''''''''''''''''') + N''''.dbo.sCIN'''''''');'''';

    EXEC sp_executesql
        @CheckSql,
        N''''@ObjIdOUT int OUTPUT'''',
        @ObjIdOUT = @ObjId OUTPUT;

    IF @ObjId IS NULL
    BEGIN
        RAISERROR(N''''V databázi "%s" neexistuje tabulka dbo.sCIN.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    -- JaS: smazani ciselniku pracovniku
    DELETE FROM dbo.CZMST096;

    DECLARE @SQL nvarchar(max);

    -- JaS: pracovni select
    SET @SQL = N''''
        INSERT INTO dbo.CZMST096
        (
            prac_id,
            prac_desc,
            prac_typ,
            prac_carcode
        )
        SELECT
            IDS AS prac_id,
            SText  AS prac_desc,
            ''''''''1''''''''  AS prac_typ,
            IDS AS prac_carcode
        FROM '''' + QUOTENAME(@DatabaseName) + N''''.dbo.sCIN;
    '''';

    EXEC sp_executesql @SQL;
END'';

-- Installation step 388
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_SKLADY]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST093

		 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';

	   SET @SQL = ''''
INSERT INTO [dbo].[CZMST093]
           ([skl_id]
           ,[skl_desc]
           ,[skl_typ]
           ,[skl_carcode])
		select 
		LEFT(ID, 20) as skl_id,
		LEFT(COALESCE(IDS,SText, ''''''''), 40) as skl_desc,
		'''''''' as skl_typ,
		LEFT(COALESCE(IDS,''''''''), 21) as skl_carcode
		from '''' + QUOTENAME(@DatabaseName) + ''''.dbo.sSklad
    '''';

    EXEC sp_executesql @SQL;
END

/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_STREDISKA]     ******/
SET ANSI_NULLS ON'';

-- Installation step 389
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_STREDISKA]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST091

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';

--INSERT INTO [dbo].[CZMST091]
--           ([str_id]
--           ,[str_desc]
--           ,[str_typ]
--           ,[str_carcode])
--		   select 
--LEFT(ID,30) as str_id,
--LEFT(COALESCE(sText,''''''''),40) as str_desc,
--LEFT(COALESCE(IDS, ''''''''), 3) as str_typ,
--LEFT(ID,30) as str_carcode
--from StwPh_63489040_2025.dbo.sSTR

    --SET @SQL = ''''
    --    INSERT INTO [dbo].[CZMST091]
    --           ([str_id], [str_desc], [str_typ], [str_carcode])
    --    SELECT 
    --        LEFT(IDS,30) AS str_id,
    --        LEFT(COALESCE(sText, ''''''''''''''''), 40) AS str_desc,
    --        LEFT(COALESCE(IDS, ''''''''''''''''), 3) AS str_typ,
    --        LEFT(ID,30) AS str_carcode
    --    FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.sSTR;
    --'''';

	    SET @SQL = N''''
        INSERT INTO dbo.CZMST091
            (str_id, str_desc, str_typ, str_carcode)
        SELECT 
            LEFT(COALESCE(IDS, N''''''''''''''''), 30) AS str_id,
            LEFT(COALESCE(sText, N''''''''''''''''), 40) AS str_desc,
            LEFT(COALESCE(IDS, N''''''''''''''''), 3) AS str_typ,
            LEFT(COALESCE(ID, N''''''''''''''''), 30) AS str_carcode
        FROM '''' + QUOTENAME(@DatabaseName) + N''''.dbo.sSTR;
    '''';

    EXEC sp_executesql @SQL;



END'';

-- Installation step 390
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzský Matouš
-- Create date: 3.11.2025
-- Description:	Procedura pro dotažení vazeb z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_VazbaMat]
    @DatabaseName nvarchar(100),
    @TypyVyrobku nvarchar(max),
    @ListVyrobku nvarchar(max),
    @TypyMaterialu nvarchar(max)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);

    DELETE FROM dbo.FASK_Vyroba_TP;

    DECLARE @cnt int;
    SELECT @cnt = COUNT(*) FROM dbo.FASK_Vyroba_TP;

    IF @cnt > 0
    BEGIN
        PRINT N''''Tabulka [FASK_Vyroba_TP] již obsahuje '''' + CAST(@cnt AS NVARCHAR(50)) + N'''' záznamů. Záznamy z IS POHODA nebudou přidány.'''';
        RETURN -1;
    END

    /* ===== Původní @Polozky TABLE -> musí být #temp, aby do něj šlo INSERTovat z dynamického SQL ===== */
    CREATE TABLE #Polozky
    (
        ID int IDENTITY(1,1) PRIMARY KEY,
        Klic int,
        ITEMDESC nvarchar(100),
        MJ nvarchar(50)
    );

    -- Deklarace proměnných
    DECLARE @sp_count int,
            @count int = 0,
            @ITEMNMBR nvarchar(31) = N''''0'''',
            @ITEMDESC nvarchar(100) = N''''0'''',
            @MJ nvarchar(50) = N''''0'''',
            @ID_L_Max nvarchar(50) = N''''99'''';

    /* ===== Načtení položek ze zvolené DB (nahrazuje StwPh_63489040_2025.dbo.SKz) ===== */
    DECLARE @SQL nvarchar(max);

    IF ISNULL(@ListVyrobku, N'''''''') = N''''''''
    BEGIN
        SET @SQL = N''''
            INSERT INTO #Polozky (Klic, ITEMDESC, MJ)
            SELECT
                S.ID      AS Klic,
                S.Nazev   AS ITEMDESC,
                S.MJ      AS MJ
            FROM '''' + @Db + N''''.dbo.SKz AS S
            WHERE S.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@TypyVyrobku, DEFAULT));
        '''';

        EXEC sp_executesql
            @SQL,
            N''''@TypyVyrobku nvarchar(max)'''',
            @TypyVyrobku = @TypyVyrobku;
    END
    ELSE
    BEGIN
        SET @SQL = N''''
            INSERT INTO #Polozky (Klic, ITEMDESC, MJ)
            SELECT
                S.ID      AS Klic,
                S.Nazev   AS ITEMDESC,
                S.MJ      AS MJ
            FROM '''' + @Db + N''''.dbo.SKz AS S
            WHERE S.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@TypyVyrobku, DEFAULT))
              AND S.ID IN (SELECT Name FROM dbo.splitstring(@ListVyrobku, DEFAULT));
        '''';

        EXEC sp_executesql
            @SQL,
            N''''@TypyVyrobku nvarchar(max), @ListVyrobku nvarchar(max)'''',
            @TypyVyrobku = @TypyVyrobku,
            @ListVyrobku = @ListVyrobku;
    END

    SET @sp_count = (SELECT COUNT(1) FROM #Polozky);

    -- Cyklus přes všechny
    WHILE (@sp_count > @count)
    BEGIN
        SET @count = @count + 1;

        SET @ITEMNMBR = (SELECT Klic FROM #Polozky WHERE ID = @count);
        SET @ITEMDESC = (SELECT ITEMDESC FROM #Polozky WHERE ID = @count);
        SET @MJ       = (SELECT MJ FROM #Polozky WHERE ID = @count);

        SET @ID_L_Max = (SELECT ISNULL(MAX(ID_L), ''''99'''') FROM dbo.FASK_Vyroba_TP);
        SET @ID_L_Max = CONVERT(nvarchar(50), CONVERT(int, @ID_L_Max) + 1);

        INSERT INTO dbo.FASK_Vyroba_TP
        (
            ID_H, ID_L,
            ITEMNMBR_Def, DESC_Def, MJ_Def,
            ITEMNMBR_fol, DESC_Fol, MJ_Fol,
            koef, ID_USER, dateedit, [alter], PUO
        )
        SELECT
            NULL,
            @ID_L_Max,
            @ITEMNMBR,
            @ITEMDESC,
            @MJ,
            NULL,
            NULL,
            NULL,
            NULL,
            99,
            GETDATE(),
            0,
            1;

        /* Pozn.: sem posílám @DatabaseName (bez hranatých závorek).
           Pokud tvoje *_Pln procedura očekává už QUOTENAME, změň zpět na @Db. */
        EXECUTE dbo.FASK_proc_EXPORT_POHODA_FASK_VazbaMatPln
            @DatabaseName,
            @ITEMNMBR,
            @ITEMDESC,
            @MJ,
            @ID_L_Max,
            @TypyMaterialu;
    END

    DROP TABLE #Polozky;
END'';

-- Installation step 391
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzský Matouš
-- Create date: 3.11.2025
-- Description:	Procedura pro dotažení vazeb z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_VazbaMatPln] 
    @DatabaseName nvarchar(100),
    @ITEMNMBR nvarchar(31) = NULL,
    @ITEMDESC nvarchar(100) = NULL,
    @MJ nvarchar(50) = NULL,
    @ID_H int,
    @TypyMaterialu nvarchar(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);

    /* ===== Původní @PolozkyMat TABLE -> musí být #temp, aby do něj šlo INSERTovat z dynamického SQL ===== */
    CREATE TABLE #PolozkyMat
    (
        ID int IDENTITY(1,1) PRIMARY KEY,
        Klic int,
        ITEMDESC nvarchar(100),
        MJ nvarchar(50),
        QTY numeric(19,5)
    );

    DECLARE @AllCount int,
            @Inkrement int = 0,
            @ITEMNMBR_pol nvarchar(31) = N''''0'''',
            @ITEMDESC_pol nvarchar(100) = N''''0'''',
            @MJ_pol nvarchar(50) = N''''0'''',
            @QTY numeric(19,5),
            @ID_L_Max int;

    /* ===== Načtení položek materiálu ze zvolené DB (nahrazuje StwPh_63489040_2025.dbo.SKzPol + dbo.SKz) ===== */
    DECLARE @SQL nvarchar(max);

    SET @SQL = N''''
        INSERT INTO #PolozkyMat (Klic, ITEMDESC, MJ, QTY)
        SELECT
            S.ID       AS Klic,
            S.Nazev    AS ITEMDESC,
            S.MJ       AS MJ,
            P.Mnozstvi AS QTY
        FROM '''' + @Db + N''''.dbo.SKzPol AS P
        LEFT JOIN '''' + @Db + N''''.dbo.SKz AS S ON S.ID = P.RefSKz
        WHERE P.RefAg = @ITEMNMBR
          AND S.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@TypyMaterialu, DEFAULT));
    '''';

    EXEC sp_executesql
        @SQL,
        N''''@ITEMNMBR nvarchar(31), @TypyMaterialu nvarchar(max)'''',
        @ITEMNMBR = @ITEMNMBR,
        @TypyMaterialu = @TypyMaterialu;

    SET @AllCount = (SELECT COUNT(1) FROM #PolozkyMat);

    WHILE (@AllCount > @Inkrement)
    BEGIN
        SET @Inkrement = @Inkrement + 1;

        SET @ITEMNMBR_pol  = (SELECT Klic    FROM #PolozkyMat WHERE ID = @Inkrement);
        SET @ITEMDESC_pol  = (SELECT ITEMDESC FROM #PolozkyMat WHERE ID = @Inkrement);
        SET @MJ_pol        = (SELECT MJ     FROM #PolozkyMat WHERE ID = @Inkrement);
        SET @QTY           = (SELECT QTY    FROM #PolozkyMat WHERE ID = @Inkrement);

        SET @ID_L_Max = (SELECT MAX(ID_L) FROM dbo.FASK_Vyroba_TP);
        SET @ID_L_Max = ISNULL(@ID_L_Max, 0) + 1;

        INSERT INTO dbo.FASK_Vyroba_TP
        (
            ID_H,
            ID_L,
            ITEMNMBR_Def,
            DESC_Def,
            MJ_Def,
            ITEMNMBR_fol,
            DESC_Fol,
            MJ_Fol,
            koef,
            ID_USER,
            dateedit,
            [alter],
            PUO
        )
        SELECT
            @ID_H,
            @ID_L_Max,
            @ITEMNMBR,
            @ITEMDESC,
            @MJ,
            @ITEMNMBR_pol,
            @ITEMDESC_pol,
            @MJ_pol,
            @QTY,
            99,
            GETDATE(),
            0,
            0;
    END

    DROP TABLE #PolozkyMat;
END'';

-- Installation step 392
EXEC sys.sp_executesql N''-----------------------------------------------
-- Autor: Ing.Rathouzský Matouš
-- Datum: 4.6.2026


----------------------------------------------

CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY]
    @DatabaseName nvarchar(100),
    @ExportTypFilter nvarchar(100),
    @ExportSkladFilter nvarchar(100),
    @ExportovatPouzeAktivniPolozky bit,
    @EXZas_DotahovatAlternativniDodavatele bit,
    @EvidenceSarzi bit,
    @EvidenceVyrobnichCisel bit,
    @PohodaE1 bit
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);

    DECLARE @TrackExp int = 0;
    DECLARE @IObchodExists bit = 0;
    DECLARE @IObchodRez1Expr nvarchar(400) = N''''CAST(0 AS nvarchar(50))'''';
    DECLARE @SQL nvarchar(max);

    /* =========================
       TrackExp (VPrCZExpTrackIS)
       ========================= */
    IF @PohodaE1 = 0
    BEGIN
        SET @TrackExp = 0;
    END
    ELSE
    BEGIN
        SET @SQL = N''''
IF COL_LENGTH('''''''''''' + REPLACE(@DatabaseName,'''''''''''''''','''''''''''''''''''''''') + N''''.dbo.SKz'''''''', ''''''''VPrCZExpTrackIS'''''''') IS NOT NULL
BEGIN
    SELECT TOP (1) @TrackExpOUT = ISNULL(VPrCZExpTrackIS, 0)
    FROM '''' + @Db + N''''.dbo.SKz;
END
ELSE
BEGIN
    SET @TrackExpOUT = 0;
END
'''';
        EXEC sp_executesql
            @SQL,
            N''''@TrackExpOUT int OUTPUT'''',
            @TrackExpOUT = @TrackExp OUTPUT;
    END

    DELETE FROM dbo.FASK_ZASOBY;

    /* =========================
       IObchod -> REZ1
       Bezpečná detekce sloupce v cílové Pohoda DB.
       Pokud sloupec SKz.IObchod neexistuje, do REZ1 se zapíše ''''0''''.
       ========================= */
    SET @SQL = N''''
IF COL_LENGTH('''''''''''' + REPLACE(@DatabaseName,'''''''''''''''','''''''''''''''''''''''') + N''''.dbo.SKz'''''''', ''''''''IObchod'''''''') IS NOT NULL
BEGIN
    SET @IObchodExistsOUT = 1;
END
ELSE
BEGIN
    SET @IObchodExistsOUT = 0;
END
'''';

    EXEC sp_executesql
        @SQL,
        N''''@IObchodExistsOUT bit OUTPUT'''',
        @IObchodExistsOUT = @IObchodExists OUTPUT;

    IF @IObchodExists = 1
        SET @IObchodRez1Expr = N''''CAST(ISNULL(SKz.IObchod,0) AS nvarchar(50))'''';
    ELSE
        SET @IObchodRez1Expr = N''''CAST(0 AS nvarchar(50))'''';

    /* =========================
       Větvení podle filtrů
       ========================= */
    IF (ISNULL(@ExportSkladFilter,N'''''''') <> N'''''''') OR (ISNULL(@ExportTypFilter,N'''''''') <> N'''''''')
    BEGIN -- A0

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN -- B0

            /* SKz_FillBy_EPAP_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C0

                /* SKzAlternatives_FillBy_EPAP_DAD */
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;

            END -- C0

        END -- B0
        ELSE
        BEGIN -- B1

            /* SKz_FillBy_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C1
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END -- C1
        END -- B1

    END -- A0
    ELSE
    BEGIN -- A1
        /* Pro jednoduchost: bez filtrů – stejné jako tvoje A1, jen dynamicky (SKz + volitelně SKzNC) */

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
WHERE (SKz.Odbyt <> 0);
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
WHERE (SKz.Odbyt <> 0);
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
        ELSE
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz;
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    '''' + @IObchodRez1Expr + N'''',0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg;
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
    END

    SELECT COUNT(*) FROM dbo.FASK_ZASOBY;
END'';

-- Installation step 393
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY_zal20260417]
    @DatabaseName nvarchar(100),
    @ExportTypFilter nvarchar(100),
    @ExportSkladFilter nvarchar(100),
    @ExportovatPouzeAktivniPolozky bit,
    @EXZas_DotahovatAlternativniDodavatele bit,
    @EvidenceSarzi bit,
    @EvidenceVyrobnichCisel bit,
    @PohodaE1 bit
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);

    DECLARE @TrackExp int = 0;
    DECLARE @SQL nvarchar(max);

    /* =========================
       TrackExp (VPrCZExpTrackIS)
       ========================= */
    IF @PohodaE1 = 0
    BEGIN
        SET @TrackExp = 0;
    END
    ELSE
    BEGIN
        SET @SQL = N''''
IF COL_LENGTH('''''''''''' + REPLACE(@DatabaseName,'''''''''''''''','''''''''''''''''''''''') + N''''.dbo.SKz'''''''', ''''''''VPrCZExpTrackIS'''''''') IS NOT NULL
BEGIN
    SELECT TOP (1) @TrackExpOUT = ISNULL(VPrCZExpTrackIS, 0)
    FROM '''' + @Db + N''''.dbo.SKz;
END
ELSE
BEGIN
    SET @TrackExpOUT = 0;
END
'''';
        EXEC sp_executesql
            @SQL,
            N''''@TrackExpOUT int OUTPUT'''',
            @TrackExpOUT = @TrackExp OUTPUT;
    END

    DELETE FROM dbo.FASK_ZASOBY;

    /* =========================
       Větvení podle filtrů
       ========================= */
    IF (ISNULL(@ExportSkladFilter,N'''''''') <> N'''''''') OR (ISNULL(@ExportTypFilter,N'''''''') <> N'''''''')
    BEGIN -- A0

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN -- B0

            /* SKz_FillBy_EPAP_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C0

                /* SKzAlternatives_FillBy_EPAP_DAD */
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;

            END -- C0

        END -- B0
        ELSE
        BEGIN -- B1

            /* SKz_FillBy_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C1
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END -- C1
        END -- B1

    END -- A0
    ELSE
    BEGIN -- A1
        /* Pro jednoduchost: bez filtrů – stejné jako tvoje A1, jen dynamicky (SKz + volitelně SKzNC) */

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
WHERE (SKz.Odbyt <> 0);
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
WHERE (SKz.Odbyt <> 0);
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
        ELSE
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz;
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg;
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
    END

    SELECT COUNT(*) FROM dbo.FASK_ZASOBY;
END'';

-- Installation step 394
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY_zal20260604]
    @DatabaseName nvarchar(100),
    @ExportTypFilter nvarchar(100),
    @ExportSkladFilter nvarchar(100),
    @ExportovatPouzeAktivniPolozky bit,
    @EXZas_DotahovatAlternativniDodavatele bit,
    @EvidenceSarzi bit,
    @EvidenceVyrobnichCisel bit,
    @PohodaE1 bit
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    /* ===== Povinný @DatabaseName ===== */
    IF @DatabaseName IS NULL OR LEN(LTRIM(RTRIM(@DatabaseName))) = 0
    BEGIN
        RAISERROR(N''''@DatabaseName je povinný parametr a nesmí být prázdný.'''', 16, 1);
        RETURN;
    END

    /* ===== DB musí existovat ===== */
    IF DB_ID(@DatabaseName) IS NULL
    BEGIN
        RAISERROR(N''''@DatabaseName "%s" neexistuje.'''', 16, 1, @DatabaseName);
        RETURN;
    END

    DECLARE @Db sysname = QUOTENAME(@DatabaseName);

    DECLARE @TrackExp int = 0;
    DECLARE @SQL nvarchar(max);

    /* =========================
       TrackExp (VPrCZExpTrackIS)
       ========================= */
    IF @PohodaE1 = 0
    BEGIN
        SET @TrackExp = 0;
    END
    ELSE
    BEGIN
        SET @SQL = N''''
IF COL_LENGTH('''''''''''' + REPLACE(@DatabaseName,'''''''''''''''','''''''''''''''''''''''') + N''''.dbo.SKz'''''''', ''''''''VPrCZExpTrackIS'''''''') IS NOT NULL
BEGIN
    SELECT TOP (1) @TrackExpOUT = ISNULL(VPrCZExpTrackIS, 0)
    FROM '''' + @Db + N''''.dbo.SKz;
END
ELSE
BEGIN
    SET @TrackExpOUT = 0;
END
'''';
        EXEC sp_executesql
            @SQL,
            N''''@TrackExpOUT int OUTPUT'''',
            @TrackExpOUT = @TrackExp OUTPUT;
    END

    DELETE FROM dbo.FASK_ZASOBY;

    /* =========================
       Větvení podle filtrů
       ========================= */
    IF (ISNULL(@ExportSkladFilter,N'''''''') <> N'''''''') OR (ISNULL(@ExportTypFilter,N'''''''') <> N'''''''')
    BEGIN -- A0

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN -- B0

            /* SKz_FillBy_EPAP_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C0

                /* SKzAlternatives_FillBy_EPAP_DAD */
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;

            END -- C0

        END -- B0
        ELSE
        BEGIN -- B1

            /* SKz_FillBy_DAD */
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';

            EXEC sp_executesql
                @SQL,
                N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                  @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @ExportSkladFilter=@ExportSkladFilter,
                @ExportTypFilter=@ExportTypFilter,
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN -- C1
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
INNER JOIN '''' + @Db + N''''.dbo.sSklad AS s ON s.ID = SKz.RefSklad
WHERE (SKz.Odbyt <> 0)
  AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter, DEFAULT)))
  AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, DEFAULT)));
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@ExportSkladFilter nvarchar(100), @ExportTypFilter nvarchar(100),
                      @EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @ExportSkladFilter=@ExportSkladFilter,
                    @ExportTypFilter=@ExportTypFilter,
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END -- C1
        END -- B1

    END -- A0
    ELSE
    BEGIN -- A1
        /* Pro jednoduchost: bez filtrů – stejné jako tvoje A1, jen dynamicky (SKz + volitelně SKzNC) */

        IF @ExportovatPouzeAktivniPolozky = 1
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
WHERE (SKz.Odbyt <> 0);
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg
WHERE (SKz.Odbyt <> 0);
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
        ELSE
        BEGIN
            SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKz.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKz.MJ, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz;
'''';
            EXEC sp_executesql
                @SQL,
                N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                @EvidenceSarzi=@EvidenceSarzi,
                @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                @PohodaE1=@PohodaE1,
                @TrackExp=@TrackExp;

            IF @EXZas_DotahovatAlternativniDodavatele = 1
            BEGIN
                SET @SQL = N''''
INSERT INTO dbo.FASK_ZASOBY
(
    ITEMNMBR, ITEMDESC, ITEMCODE, VNDITNUM, CZ_CarKod, LOCNCODE, SKL_ID, QTY, QTYPACK, MJ, DMJ,
    TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, PRICE4, PRICE5,
    CZ_SerNum_Track, CZ_SerNum_Delka,
    CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track,
    REZ1, REZ2, REZ3, REZ4,
    ODB_ID, mena_ID, SERLTNUM, WEIGHT, TIMEFROM, TIMETO, LSTMod, loginid,
    CZ_Expirace_Track, EXPIRACE
)
SELECT
    LEFT(SKz.ID,40),
    LEFT(SKz.Nazev,100),
    LEFT(ISNULL(SKz.IDS, ''''''''''''''''),70),
    LEFT(SKzNC.EAN,60),
    '''''''''''''''',
    '''''''''''''''',
    SKz.RefSklad,
    ISNULL(SKz.StavZ,0),
    0,
    LEFT(ISNULL(SKzNC.MJEAN, ''''''''''''''''),10),
    '''''''''''''''',
    0,
    0,0,0,0,0,0,
    dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID, @EvidenceSarzi, @EvidenceVyrobnichCisel, @PohodaE1),
    0,
    0,0,0,0,
    0,0,0,0,
    LEFT(SKz.RefAD,12),
    '''''''''''''''',
    '''''''''''''''',
    NULL,
    NULL,
    NULL,
    GETDATE(),
    '''''''''''''''',
    @TrackExp,
    NULL
FROM '''' + @Db + N''''.dbo.SKz AS SKz
INNER JOIN '''' + @Db + N''''.dbo.SKzNC AS SKzNC ON SKz.ID = SKzNC.RefAg;
'''';
                EXEC sp_executesql
                    @SQL,
                    N''''@EvidenceSarzi bit, @EvidenceVyrobnichCisel bit, @PohodaE1 bit, @TrackExp int'''',
                    @EvidenceSarzi=@EvidenceSarzi,
                    @EvidenceVyrobnichCisel=@EvidenceVyrobnichCisel,
                    @PohodaE1=@PohodaE1,
                    @TrackExp=@TrackExp;
            END
        END
    END

    SELECT COUNT(*) FROM dbo.FASK_ZASOBY;
END'';

-- Installation step 395
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Expedice]
	@cisloDavky [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;


  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
    SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);


 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''E'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 396
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Inventura]
	@cisloDavky [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;


  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
    SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);


 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''I'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 397
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_LOKACE]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


  -- JaS: smazani ciselniku lokaci
 	DELETE  FROM CZMST094




 -- JaS: pracovni select
 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST094]
            ([SKL_ID]
           ,[LOCNCODE]
           ,[TYPE]
           ,[Description]
           ,[Barcode])
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';

 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 398
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_LOKACE_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


  -- JaS: smazani ciselniku lokaci
 	DELETE  FROM CZMST094


   DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST094]
            ([SKL_ID]
           ,[LOCNCODE]
           ,[TYPE]
           ,[Description]
           ,[Barcode])
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 399
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_LOKACE_DB_TEST]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


  -- JaS: smazani ciselniku lokaci
 	DELETE  FROM CZMST094


   DECLARE @select nvarchar(max) =
N''''
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 400
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 9.2.2026
 -- Description:	
 -- =============================================
 create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_MENY_DB] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';


 
 	DELETE  FROM CZMST097



 ------------------------------
  DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST097]
            ([mena_ID]
            ,[mena_text])
 SELECT 
 ID as mena_ID,
 LEFT(Kod,30) as mena_text
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.sCMeny WHERE (Pouzit = 1)
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 401
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 18.2.2026
 -- Description:	
 -- =============================================
 CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_MENY_DB_TEST] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''StwPh_63489040_2025'''';


 
 	DELETE  FROM CZMST097



 ------------------------------
  DECLARE @select nvarchar(max) =
N''''
 SELECT 
 ID as mena_ID,
 LEFT(Kod,30) as mena_text,
 Pouzit as mena_hlavni,
 NULL as mena_kurz,
 NULL as mena_kurzDatum
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.sCMeny WHERE (Pouzit = 1)
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 402
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_NacteniOPlanu]
(
      @Nezaplanovane       bit
    , @PohodaE1            bit   -- 1 = sloupec VPrQTY existuje, 0 = neexistuje

    , @OD_DatumOD          datetime
    , @OD_DatumDO          datetime
    , @OD_DatumOD_Check    bit
    , @OD_DatumDO_Check    bit

    , @DO_DatumOD          datetime
    , @DO_DatumDO          datetime
    , @DO_DatumOD_Check    bit
    , @DO_DatumDO_Check    bit

    , @ZAP_DatumOD         datetime
    , @ZAP_DatumDO         datetime
    , @ZAP_DatumOD_Check   bit
    , @ZAP_DatumDO_Check   bit

    , @OBJ                 nvarchar(50)      = NULL
    , @Kod                 nvarchar(50)      = NULL
    , @Firma               nvarchar(100)     = NULL
    , @FormaUhrady         nvarchar(200)     = NULL  -- CSV: ''''1,2,3''''

    , @UserParam_1         nvarchar(255)     = NULL
    , @UserParam_2         nvarchar(255)     = NULL
    , @UserParam_3         nvarchar(255)     = NULL
    , @UserParam_4         nvarchar(255)     = NULL
    , @UserParam_5         nvarchar(255)     = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PohodaDBName sysname;
    DECLARE @DbNameQ      sysname;
    DECLARE @SQL          nvarchar(MAX);
    DECLARE @QTYPart      nvarchar(MAX);

    ----------------------------------------------------------------------
    -- NORMALIZACE PRÁZDNÝCH STRINGŮ NA NULL (bez filtru)
    ----------------------------------------------------------------------
    IF (@OBJ         IS NOT NULL AND LTRIM(RTRIM(@OBJ))         = '''''''') SET @OBJ = NULL;
    IF (@Kod         IS NOT NULL AND LTRIM(RTRIM(@Kod))         = '''''''') SET @Kod = NULL;
    IF (@Firma       IS NOT NULL AND LTRIM(RTRIM(@Firma))       = '''''''') SET @Firma = NULL;
    IF (@FormaUhrady IS NOT NULL AND LTRIM(RTRIM(@FormaUhrady)) = '''''''') SET @FormaUhrady = NULL;

    IF (@UserParam_1 IS NOT NULL AND LTRIM(RTRIM(@UserParam_1)) = '''''''') SET @UserParam_1 = NULL;
    IF (@UserParam_2 IS NOT NULL AND LTRIM(RTRIM(@UserParam_2)) = '''''''') SET @UserParam_2 = NULL;
    IF (@UserParam_3 IS NOT NULL AND LTRIM(RTRIM(@UserParam_3)) = '''''''') SET @UserParam_3 = NULL;
    IF (@UserParam_4 IS NOT NULL AND LTRIM(RTRIM(@UserParam_4)) = '''''''') SET @UserParam_4 = NULL;
    IF (@UserParam_5 IS NOT NULL AND LTRIM(RTRIM(@UserParam_5)) = '''''''') SET @UserParam_5 = NULL;

    ----------------------------------------------------------------------
    -- NÁZEV POHODA DATABÁZE
    ----------------------------------------------------------------------
    SET @PohodaDBName = N''''StwPh_27675301_2025'''';
    SET @DbNameQ      = QUOTENAME(@PohodaDBName);

    ----------------------------------------------------------------------
    -- QTY / QTY_Zaplanovano / QTY_Zbyva PODLE VERZE POHODY
    ----------------------------------------------------------------------
    IF (@PohodaE1 = 1)
    BEGIN
        SET @QTYPart = N''''
            , op.Mnozstvi         AS QTY
            , op.VPrQTY           AS QTY_Zaplanovano
            , (op.Mnozstvi - ISNULL(op.VPrQTY,0)) AS QTY_Zbyva'''';
    END
    ELSE
    BEGIN
        -- VPrQTY neexistuje → všechno je "nezapl.", QTY_Zaplanovano = NULL, QTY_Zbyva = celé množství
        SET @QTYPart = N''''
            , op.Mnozstvi         AS QTY
            , CAST(NULL AS decimal(18,4)) AS QTY_Zaplanovano
            , op.Mnozstvi         AS QTY_Zbyva'''';
    END

    BEGIN TRY

        ------------------------------------------------------------------
        -- ZÁKLAD SELECTU
        ------------------------------------------------------------------
        SET @SQL = N''''
        SELECT 
              op.RefSKz           AS ITEMNMBR
            , op.SText            AS ITEMDESC
            , op.Kod              AS ITEMCODE

            , o.Firma             AS OBJ_COMPANY

            , o.Cislo             AS OBJ_NMBR
            , o.SText             AS OBJ_DESC
            , o.RelTpObj          AS OBJ_TYPE
        '''' + @QTYPart + N''''

            , o.ID                AS OBJ_ORD
            , op.ID               AS OBJ_ITEM_ORD

            , o.DatOd             AS OBJ_DATE_FROM
            , o.DatDo             AS OBJ_DATE_TO

            , o.Datum             AS OBJ_DATE_ZAPL

            , o.RelForUh          AS OBJ_ForUh
            , fu.IDS              AS OBJ_ForUh_IDS

            , x.UserParam_1       AS UserParam_1
            , x.UserParam_2       AS UserParam_2
            , x.UserParam_3       AS UserParam_3
            , x.UserParam_4       AS UserParam_4
            , x.UserParam_5       AS UserParam_5

            -- plánování v našich tabulkách
            , pv.PLAN_QTY         AS PLAN_QTY
            , CASE 
                  WHEN pv.PLAN_QTY IS NULL                 THEN 0  -- není v našich tabulkách
                  WHEN pv.PLAN_QTY = op.Mnozstvi           THEN 1  -- stejné množství
                  ELSE 2                                        -- rozdílné množství
              END              AS PLAN_FLAG

        FROM '''' + @DbNameQ + N''''.dbo.OBJ      AS o
        LEFT JOIN '''' + @DbNameQ + N''''.dbo.OBJpol   AS op ON op.RefAg = o.ID
        LEFT JOIN '''' + @DbNameQ + N''''.dbo.sFormUh  AS fu ON fu.ID = o.RelForUh

        LEFT JOIN (
            SELECT ORD, UserParam_1, UserParam_2, UserParam_3, UserParam_4, UserParam_5
            FROM dbo.FASK_Get_Planovani_NacteniUserParams()
        ) x ON x.ORD = op.ID

        -- agregace z našich tabulek plánování
        LEFT JOIN (
            SELECT 
                  ITEMNMBR
                , OBJ_NMBR
                , SUM(QTY) AS PLAN_QTY
            FROM dbo.FASK_Vyroba_PVP
            GROUP BY ITEMNMBR, OBJ_NMBR
        ) pv
            ON pv.ITEMNMBR = op.RefSKz
           AND pv.OBJ_NMBR = o.Cislo

        WHERE 
            o.RelTpObj = 1
            AND o.Vyrizeno = 0
            AND op.RefSKz IS NOT NULL
        '''';

        ------------------------------------------------------------------
        -- DYNAMICKÉ DOPLNĚNÍ FILTRŮ
        ------------------------------------------------------------------

        -- Nezaplanovane
        IF (@Nezaplanovane = 1 AND @PohodaE1 = 1)
        BEGIN
            -- jen v E1, kde existuje VPrQTY
            SET @SQL += N''''
            AND (op.VPrQTY IS NULL OR op.VPrQTY <> op.Mnozstvi)'''';
        END
        -- @PohodaE1 = 0 → nelze rozlišit, nepřidáváme podmínku (všechno vyjede)

			  -- Nezaplanovane podle našich plánů (vyhodit zelené = plně shodné)
        IF (@Nezaplanovane = 1)
        BEGIN
            SET @SQL += N''''
            AND (pv.PLAN_QTY IS NULL OR pv.PLAN_QTY <> op.Mnozstvi)'''';
        END

        -- OBJ (číslo dokladu)
        IF (@OBJ IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.Cislo LIKE @OBJ + ''''''''%'''''''''''';
        END

        -- Kód položky
        IF (@Kod IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND op.Kod LIKE @Kod + ''''''''%'''''''''''';
        END

        -- Firma
        IF (@Firma IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.Firma LIKE @Firma + ''''''''%'''''''''''';
        END

        -- Forma úhrady (CSV seznam ID)
        IF (@FormaUhrady IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND o.RelForUh IN (
                SELECT TRY_CAST(value AS int)
                FROM STRING_SPLIT(@FormaUhrady, '''''''','''''''')
            )'''';
        END

        -- Datum OD (o.DatOd)
        IF (@OD_DatumOD_Check = 1 AND @OD_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd BETWEEN @OD_DatumOD AND @OD_DatumDO'''';
        END
        ELSE IF (@OD_DatumDO_Check = 1 AND @OD_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd <= @OD_DatumDO'''';
        END
        ELSE IF (@OD_DatumDO_Check = 0 AND @OD_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatOd >= @OD_DatumOD'''';
        END
        -- jinak se DatOd nefiltruje

        -- Datum DO (o.DatDo)
        IF (@DO_DatumOD_Check = 1 AND @DO_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo BETWEEN @DO_DatumOD AND @DO_DatumDO'''';
        END
        ELSE IF (@DO_DatumDO_Check = 1 AND @DO_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo <= @DO_DatumDO'''';
        END
        ELSE IF (@DO_DatumDO_Check = 0 AND @DO_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.DatDo >= @DO_DatumOD'''';
        END

        -- Datum ZAP (o.Datum)
        IF (@ZAP_DatumOD_Check = 1 AND @ZAP_DatumDO_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.Datum BETWEEN @ZAP_DatumOD AND @ZAP_DatumDO'''';
        END
        ELSE IF (@ZAP_DatumDO_Check = 1 AND @ZAP_DatumOD_Check = 0)
        BEGIN
            SET @SQL += N''''
            AND o.Datum <= @ZAP_DatumDO'''';
        END
        ELSE IF (@ZAP_DatumDO_Check = 0 AND @ZAP_DatumOD_Check = 1)
        BEGIN
            SET @SQL += N''''
            AND o.Datum >= @ZAP_DatumOD'''';
        END

        -- UserParam_1–5
        IF (@UserParam_1 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_1 = @UserParam_1'''';
        END
        IF (@UserParam_2 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_2 = @UserParam_2'''';
        END
        IF (@UserParam_3 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_3 = @UserParam_3'''';
        END
        IF (@UserParam_4 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_4 = @UserParam_4'''';
        END
        IF (@UserParam_5 IS NOT NULL)
        BEGIN
            SET @SQL += N''''
            AND x.UserParam_5 = @UserParam_5'''';
        END

        -- ORDER BY
        SET @SQL += N''''
        ORDER BY 
            o.Cislo;'''';

        ------------------------------------------------------------------
        -- EXECUTE
        ------------------------------------------------------------------
        EXEC sp_executesql
            @SQL,
            N''''
              @Nezaplanovane       bit

            , @OD_DatumOD          datetime
            , @OD_DatumDO          datetime
            , @OD_DatumOD_Check    bit
            , @OD_DatumDO_Check    bit

            , @DO_DatumOD          datetime
            , @DO_DatumDO          datetime
            , @DO_DatumOD_Check    bit
            , @DO_DatumDO_Check    bit

            , @ZAP_DatumOD         datetime
            , @ZAP_DatumDO         datetime
            , @ZAP_DatumOD_Check   bit
            , @ZAP_DatumDO_Check   bit

            , @OBJ                 nvarchar(50)
            , @Kod                 nvarchar(50)
            , @Firma               nvarchar(100)
            , @FormaUhrady         nvarchar(200)

            , @UserParam_1         nvarchar(255)
            , @UserParam_2         nvarchar(255)
            , @UserParam_3         nvarchar(255)
            , @UserParam_4         nvarchar(255)
            , @UserParam_5         nvarchar(255)
            '''',
            @Nezaplanovane     = @Nezaplanovane,

            @OD_DatumOD        = @OD_DatumOD,
            @OD_DatumDO        = @OD_DatumDO,
            @OD_DatumOD_Check  = @OD_DatumOD_Check,
            @OD_DatumDO_Check  = @OD_DatumDO_Check,

            @DO_DatumOD        = @DO_DatumOD,
            @DO_DatumDO        = @DO_DatumDO,
            @DO_DatumOD_Check  = @DO_DatumOD_Check,
            @DO_DatumDO_Check  = @DO_DatumDO_Check,

            @ZAP_DatumOD       = @ZAP_DatumOD,
            @ZAP_DatumDO       = @ZAP_DatumDO,
            @ZAP_DatumOD_Check = @ZAP_DatumOD_Check,
            @ZAP_DatumDO_Check = @ZAP_DatumDO_Check,

            @OBJ               = @OBJ,
            @Kod               = @Kod,
            @Firma             = @Firma,
            @FormaUhrady       = @FormaUhrady,

            @UserParam_1       = @UserParam_1,
            @UserParam_2       = @UserParam_2,
            @UserParam_3       = @UserParam_3,
            @UserParam_4       = @UserParam_4,
            @UserParam_5       = @UserParam_5;
    END TRY
    BEGIN CATCH
        -- prázdný dataset – stejné sloupce jako DataSet PV_Zaplanovani, ale žádné řádky
        SELECT
              CAST(NULL AS nvarchar(50))   AS ITEMNMBR
            , CAST(NULL AS nvarchar(255))  AS ITEMDESC
            , CAST(NULL AS nvarchar(50))   AS ITEMCODE

            , CAST(NULL AS nvarchar(100))  AS OBJ_COMPANY

            , CAST(NULL AS nvarchar(50))   AS OBJ_NMBR
            , CAST(NULL AS nvarchar(255))  AS OBJ_DESC
            , CAST(NULL AS nvarchar(50))   AS OBJ_TYPE

            , CAST(NULL AS decimal(18,4))  AS QTY
            , CAST(NULL AS nvarchar(50))   AS QTY_Zaplanovano
            , CAST(NULL AS nvarchar(50))   AS QTY_Zbyva

            , CAST(NULL AS int)            AS OBJ_ORD
            , CAST(NULL AS int)            AS OBJ_ITEM_ORD

            , CAST(NULL AS datetime)       AS OBJ_DATE_FROM
            , CAST(NULL AS datetime)       AS OBJ_DATE_TO
            , CAST(NULL AS datetime)       AS OBJ_DATE_ZAPL

            , CAST(NULL AS int)            AS OBJ_ForUh
            , CAST(NULL AS nvarchar(50))   AS OBJ_ForUh_IDS

            , CAST(NULL AS nvarchar(255))  AS UserParam_1
            , CAST(NULL AS nvarchar(255))  AS UserParam_2
            , CAST(NULL AS nvarchar(255))  AS UserParam_3
            , CAST(NULL AS nvarchar(255))  AS UserParam_4
            , CAST(NULL AS nvarchar(255))  AS UserParam_5

            , CAST(NULL AS decimal(18,4))  AS PLAN_QTY
            , CAST(NULL AS int)            AS PLAN_FLAG
        WHERE 1 = 0;
    END CATCH
END'';

-- Installation step 403
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Odberatele]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 -- JaS: smazani ciselniku firem
 	DELETE  FROM CZMST090




 -- JaS: pracovni select

 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST090]
      ([odb_id]
      ,[odb_desc]
      ,[odb_typ]
      ,[odb_carcode]
      ,[odb_ico]
      ,[mena_ID]
      ,[odb_misto]
      ,[odb_ulice]
      ,[odb_cisloOr]
      ,[odb_psc]
      ,[odb_dic]
      ,[odb_Odberatel]
      ,[odb_Dodavatel])
SELECT
	 UkolID  as odb_id,
	 UkolID as odb_desc,
	 UkolID as odb_typ,
	 UkolID as odb_carcode,
	 UkolID as odb_ico,
	 UkolID as mena_ID,
	 UkolID as odb_misto,
	 UkolID as odb_ulice,
	 UkolID as odb_cisloOr,
	 UkolID as odb_psc,
	 UkolID as odb_dic,
	 State as odb_Odberatel,
	 UserID as odb_Dodavatel
 FROM [FASK].[dbo].[CZ_UKOL_UZIV]
 '''';

 
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 404
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Odberatele_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 -- JaS: smazani ciselniku firem
 	DELETE  FROM CZMST090



  DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST090]
      ([odb_id]
      ,[odb_desc]
      ,[odb_typ]
      ,[odb_carcode]
      ,[odb_ico]
      ,[mena_ID]
      ,[odb_misto]
      ,[odb_ulice]
      ,[odb_cisloOr]
      ,[odb_psc]
      ,[odb_dic]
      ,[odb_Odberatel]
      ,[odb_Dodavatel])
SELECT
	 UkolID  as odb_id,
	 UkolID as odb_desc,
	 UkolID as odb_typ,
	 UkolID as odb_carcode,
	 UkolID as odb_ico,
	 UkolID as mena_ID,
	 UkolID as odb_misto,
	 UkolID as odb_ulice,
	 UkolID as odb_cisloOr,
	 UkolID as odb_psc,
	 UkolID as odb_dic,
	 State as odb_Odberatel,
	 UserID as odb_Dodavatel
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 405
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Odberatele_DB_TEST]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 -- JaS: smazani ciselniku firem
 	DELETE  FROM CZMST090



  DECLARE @select nvarchar(max) =
N''''
SELECT
	 UkolID  as odb_id,
	 UkolID as odb_desc,
	 UkolID as odb_typ,
	 UkolID as odb_carcode,
	 UkolID as odb_ico,
	 UkolID as mena_ID,
	 UkolID as odb_misto,
	 UkolID as odb_ulice,
	 UkolID as odb_cisloOr,
	 UkolID as odb_psc,
	 UkolID as odb_dic,
	 --State as odb_Odberatel,
	 --UserID as odb_Dodavatel
	 NULL as odb_Odberatel,
	 NULL as odb_Dodavatel
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 406
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Ostatni]
	@cisloDavky [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;


  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
    SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);


 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''O'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 407
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_PlanovaniVyroby]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 -- -- JaS: smazani ciselniku lokaci
 --	DELETE  FROM CZMST094




 ---- JaS: pracovni select
 --SET @SQL = ''''
 --INSERT INTO [dbo].[CZMST094]
 --           ([SKL_ID]
 --          ,[LOCNCODE]
 --          ,[TYPE]
 --          ,[Description]
 --          ,[Barcode])
 --SELECT 
 --SKL_ID as SKL_ID,
 --LOCNCODE as LOCNCODE,
 --TYPE as TYPE,
 --Description as Description,
 --Barcode as Barcode
 --FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 --'''';

 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 408
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Pracovnici]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 -- JaS: smazani ciselniku pracovniku
 	DELETE  FROM CZMST096

 -- JaS: pracovni select

 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST096]
            ([prac_id]
           ,[prac_desc]
           ,[prac_typ]
		   ,[prac_carcode])
 SELECT 
 UkolID as prac_id,
 State as skl_desc,
 State as prac_typ,
 UserID as prac_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';
 
 
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 409
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Pracovnici_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 -- JaS: smazani ciselniku pracovniku
 	DELETE  FROM CZMST096


 ------------------------
   DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST096]
            ([prac_id]
           ,[prac_desc]
           ,[prac_typ]
		   ,[prac_carcode])
 SELECT 
 UkolID as prac_id,
 State as skl_desc,
 State as prac_typ,
 UserID as prac_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 410
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Pracovnici_DB_TEST]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 -- JaS: smazani ciselniku pracovniku
 	DELETE  FROM CZMST096


 ------------------------
   DECLARE @select nvarchar(max) =
N''''
 SELECT 
 UkolID as prac_id,
 State as prac_desc,
 State as prac_typ,
 UserID as prac_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 411
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Predloha]
    @SKL_ID nvarchar(20) = NULL,
    @SOPNUMBE nvarchar(30) = NULL,
    @ITEMTYPE nvarchar(11) = NULL
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- normalizace: prázdný řetězec => NULL
    SET @SKL_ID   = NULLIF(LTRIM(RTRIM(@SKL_ID)), '''''''');
    SET @SOPNUMBE = NULLIF(LTRIM(RTRIM(@SOPNUMBE)), '''''''');
	  SET @ITEMTYPE = NULLIF(LTRIM(RTRIM(@ITEMTYPE)), '''''''');

    -- bezpečné escapování pro vložení do stringu: '''' -> ''''''''
    DECLARE @SKL_ID_ESC   NVARCHAR(100) = REPLACE(@SKL_ID,   N'''''''''''''''', N'''''''''''''''''''''''');
    DECLARE @SOPNUMBE_ESC NVARCHAR(200) = REPLACE(@SOPNUMBE, N'''''''''''''''', N'''''''''''''''''''''''');   
    DECLARE @ITEMTYPEE_ESC NVARCHAR(100) = REPLACE(@ITEMTYPE, N'''''''''''''''', N'''''''''''''''''''''''');    

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;
	DECLARE @cisloDavky INT;

  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
   -- SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);

	SELECT @cisloDavky = ISNULL(MAX(CountEntries), 0) + 1
FROM dbo.CZMST_SE WITH (UPDLOCK, HOLDLOCK);



 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''J'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 SELECT @cisloDavky AS cisloDavky;
 END
 
 --SET ANSI_NULLS ON'';

-- Installation step 412
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Predloha_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL = N''''
SET NOCOUNT ON;

DECLARE @cisloDavky INT;

-- zabrání kolizi při paralelním volání (MSSQL)
SELECT @cisloDavky = ISNULL(MAX(CountEntries), 0) + 1
FROM dbo.CZMST_SE WITH (UPDLOCK, HOLDLOCK);

INSERT INTO dbo.CZMST_SE (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
        @cisloDavky AS [CountEntries],
        ''''''''-'''''''' AS [SOPNUMBE],
        [ITEMNMBR],
        ''''''''J'''''''' AS [ITEMTYPE],
        [ITEMDESC],
        ''''''''-'''''''' AS [VNDDOCNM],
        [VNDITNUM],
        1 AS [ORD],
        [CZ_CarKod],
        [SKL_ID],
        [LOCNCODE],
        [MJ],
        [QTY] AS [QTYSHPPD],
        [QTYPACK],
        ''''''''0'''''''' AS [CZ_DatVyr_Track],
        ''''''''0'''''''' AS [CZ_DatVyr_Delka],
        [CZ_SerNum_Track],
        ''''''''0'''''''' AS [CZ_SerNum_Delka],
        ''''''''0'''''''' AS [CZ_SW_Track],
        ''''''''0'''''''' AS [CZ_SW_Delka],
        ''''''''0'''''''' AS [CZ_Doslo],
        ''''''''-'''''''' AS [Note],
        ''''''''0'''''''' AS [TYPEPAL],
        0 AS [QTYPAL],
        ''''''''3'''''''' AS [PRIORITY],
        ''''''''0'''''''' AS [PRINTED],
        NULL AS [USERID],
        ''''''''0'''''''' AS [CZ_REZ1_Track],
        ''''''''0'''''''' AS [CZ_REZ2_Track],
        ''''''''0'''''''' AS [ITEMCODE],
        [WEIGHT],
        GETDATE() AS [Realization_Start],
        GETDATE() AS [Realization_Stop],
        [CZ_Expirace_Track]
FROM dbo.FASK_ZASOBY;

-- tohle je klíč: helper si vezme ExecuteScalar() a dostane cisloDavky
SELECT @cisloDavky AS cisloDavky;
'''';

    SELECT @SQL AS SqlText;
END'';

-- Installation step 413
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Predloha_DB_TEST]
    @SKL_ID nvarchar(20) = NULL,
    @SOPNUMBE nvarchar(30) = NULL,
    @ITEMTYPE nvarchar(11) = NULL
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- normalizace: prázdný řetězec => NULL
    SET @SKL_ID   = NULLIF(LTRIM(RTRIM(@SKL_ID)), '''''''');
    SET @SOPNUMBE = NULLIF(LTRIM(RTRIM(@SOPNUMBE)), '''''''');
	
    SET @ITEMTYPE = NULLIF(LTRIM(RTRIM(@ITEMTYPE)), '''''''');

    DECLARE @SQL NVARCHAR(MAX);

    -- bezpečné escapování pro vložení do stringu: '''' -> ''''''''
    DECLARE @SKL_ID_ESC   NVARCHAR(100) = REPLACE(@SKL_ID,   N'''''''''''''''', N'''''''''''''''''''''''');
    DECLARE @SOPNUMBE_ESC NVARCHAR(200) = REPLACE(@SOPNUMBE, N'''''''''''''''', N'''''''''''''''''''''''');  
    DECLARE @ITEMTYPEE_ESC NVARCHAR(100) = REPLACE(@ITEMTYPE, N'''''''''''''''', N'''''''''''''''''''''''');     

    SET @SQL = N''''
SET NOCOUNT ON;

SELECT
    CAST(NULL AS int) AS [CountEntries],
    '''' + CASE 
            WHEN @SOPNUMBE IS NULL THEN N''''N''''''''-''''''''''''
            ELSE N''''N'''''''''''' + @SOPNUMBE_ESC + N''''''''''''''''
        END + N'''' AS [SOPNUMBE],

    [ITEMNMBR],
    N''''''''J'''''''' AS [ITEMTYPE],
    [ITEMDESC],
    N''''''''-'''''''' AS [VNDDOCNM],
    [VNDITNUM],
    CAST(1 AS int) AS [ORD],
    [CZ_CarKod],
    [SKL_ID],
    [LOCNCODE],
    [MJ],
    [QTY] AS [QTYSHPPD],
    [QTYPACK],
    CAST(0 AS tinyint)  AS [CZ_DatVyr_Track],
    CAST(0 AS smallint) AS [CZ_DatVyr_Delka],
    [CZ_SerNum_Track],
    CAST(0 AS smallint) AS [CZ_SerNum_Delka],
    CAST(0 AS tinyint)  AS [CZ_SW_Track],
    CAST(0 AS smallint) AS [CZ_SW_Delka],
    CAST(0 AS tinyint)  AS [CZ_Doslo],
    N''''''''-'''''''' AS [Note],
    CAST(NULL AS nvarchar(10))  AS [TYPEPAL],
    CAST(NULL AS numeric(19,5)) AS [QTYPAL],
    CAST(3 AS tinyint) AS [PRIORITY],
    CAST(0 AS tinyint) AS [PRINTED],
    CAST(NULL AS int)  AS [USERID],
    CAST(0 AS tinyint) AS [CZ_REZ1_Track],
    CAST(0 AS tinyint) AS [CZ_REZ2_Track],
    [ITEMCODE],
    [WEIGHT],
    GETDATE() AS [Realization_Start],
    GETDATE() AS [Realization_Stop],
    [CZ_Expirace_Track]
FROM dbo.FASK_ZASOBY
'''' + CASE 
        WHEN @SKL_ID IS NULL THEN N'''''''' 
        ELSE N''''WHERE [SKL_ID] = N'''''''''''' + @SKL_ID_ESC + N''''''''''''''''
    END + N'''';
'''';

    SELECT @SQL AS SqlText;
END'';

-- Installation step 414
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Prijem]
	@cisloDavky [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;


  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
    SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);


 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''P'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 415
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Sklady]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

 -- JaS: smazani ciselniku skladu
 	DELETE  FROM CZMST093

 -- JaS: pracovni select

 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST093]
            ([skl_id]
           ,[skl_desc]
           ,[skl_carcode])
 SELECT 
 UkolID as skl_id,
 State as skl_desc,
 UserID as skl_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';
 
  EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 416
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Sklady_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

 -- JaS: smazani ciselniku skladu
 	DELETE  FROM CZMST093

  DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST093]
            ([skl_id]
           ,[skl_desc]
           ,[skl_carcode])
 SELECT 
 UkolID as skl_id,
 State as skl_desc,
 UserID as skl_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 417
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Sklady_DB_TEST]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

 -- JaS: smazani ciselniku skladu
 	DELETE  FROM CZMST093

--  DECLARE @select nvarchar(max) =
--N''''
-- INSERT INTO [dbo].[CZMST093]
--            ([skl_id]
--           ,[skl_desc]
--           ,[skl_carcode])
-- SELECT 
-- UkolID as skl_id,
-- State as skl_desc,
-- UserID as skl_carcode
-- FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
-- '''';

   DECLARE @select nvarchar(max) =
N''''
 SELECT 
 UkolID as skl_id,
 State as skl_desc,
 NULL as skl_typ,
 UserID as skl_carcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 418
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Strediska]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
-- JaS: smazani ciselniku stredisek
 	DELETE  FROM CZMST091

 -- JaS: pracovni select

 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST091]
            ([str_id]
           ,[str_typ]
           ,[str_carcode]
           ,[skl_id]
		   ,[odb_id])
 SELECT 
 UkolID as str_id,
 State as str_typ,
 UserID as str_carcode,
 UserID as skl_id,
 UserID as odb_id
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';

 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 419
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Strediska_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
-- JaS: smazani ciselniku stredisek
 	DELETE  FROM CZMST091

 -- JaS: pracovni select

 --SET @SQL = ''''
 --INSERT INTO [dbo].[CZMST091]
 --           ([str_id]
 --          ,[str_typ]
 --          ,[str_carcode]
 --          ,[skl_id]
	--	   ,[odb_id])
 --SELECT 
 --UkolID as str_id,
 --State as str_typ,
 --UserID as str_carcode,
 --UserID as skl_id,
 --UserID as odb_id
 --FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 --'''';

 --EXEC sp_executesql @SQL;

 DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST091]
            ([str_id]
           ,[str_typ]
           ,[str_carcode]
           ,[skl_id]
		   ,[odb_id])
 SELECT 
 UkolID as str_id,
 State as str_typ,
 UserID as str_carcode,
 UserID as skl_id,
 UserID as odb_id
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 420
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Strediska_DB_TEST]
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
-- JaS: smazani ciselniku stredisek
 	DELETE  FROM CZMST091


 DECLARE @select nvarchar(max) =
N''''
 SELECT 
 UkolID as str_id,
 NULL as str_desc,
 State as str_typ,
 UserID as str_carcode,
 UserID as skl_id,
 UserID as odb_id
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZ_UKOL_UZIV]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 421
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 15.10.2025
 -- Description:	
 -- =============================================
 create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_TypDokladu] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 	DELETE  FROM CZMST094




--zde zmnenit pro CZMST094
 SET @SQL = ''''
 INSERT INTO [dbo].[CZMST094]
            ([SKL_ID]
           ,[LOCNCODE]
           ,[TYPE]
           ,[Description]
           ,[Barcode])
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';

 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 422
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 15.10.2025
 -- Description:	
 -- =============================================
 create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_TypDokladu_DB] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 	DELETE  FROM CZMST094


 --------------------------------------
   DECLARE @select nvarchar(max) =
N''''
 INSERT INTO [dbo].[CZMST094]
            ([SKL_ID]
           ,[LOCNCODE]
           ,[TYPE]
           ,[Description]
           ,[Barcode])
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 423
EXEC sys.sp_executesql N''-- =============================================
 -- Author:		Ing. Rathouzsky Matous
 -- Modify date: 18.10.2025
 -- Description:	
 -- =============================================
 create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_TypDokladu_DB_TEST] 
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';


 
 	DELETE  FROM CZMST094


 --------------------------------------
   DECLARE @select nvarchar(max) =
N''''
 SELECT 
 SKL_ID as SKL_ID,
 LOCNCODE as LOCNCODE,
 TYPE as TYPE,
 Description as Description,
 Barcode as Barcode
 FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[CZMST_SkladLokace_Mapa]
 '''';


 SELECT @select AS SelectTemplate;

 END'';

-- Installation step 424
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_Vyroba]
	@cisloDavky [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;

	 -- Sestavení dynamického SQL
    DECLARE @SQL NVARCHAR(MAX);
	 -- Zde můžeš jednoduše změnit název databáze
    DECLARE @DatabaseName NVARCHAR(128) = ''''FASK'''';

	--DECLARE @cisloDavky INT;


  -- JaS: smazani
 --	 DELETE  FROM CZMST_SE
 -- WHERE CountEntries = ''''999''''

 -- vypočti číslo dávky
    SET @cisloDavky = (SELECT ISNULL(MAX(CountEntries), 0) + 1 FROM CZMST_SE);


 -- JaS: pracovni select
SET @SQL = ''''
INSERT INTO [dbo].[CZMST_SE] (
      [CountEntries]
     ,[SOPNUMBE]
     ,[ITEMNMBR]
     ,[ITEMTYPE]
     ,[ITEMDESC]
     ,[VNDDOCNM]
     ,[VNDITNUM]
     ,[ORD]
     ,[CZ_CarKod]
     ,[SKL_ID]
     ,[LOCNCODE]
     ,[MJ]
     ,[QTYSHPPD]
     ,[QTYPACK]
     ,[CZ_DatVyr_Track]
     ,[CZ_DatVyr_Delka]
     ,[CZ_SerNum_Track]
     ,[CZ_SerNum_Delka]
     ,[CZ_SW_Track]
     ,[CZ_SW_Delka]
     ,[CZ_Doslo]
     ,[Note]
     ,[TYPEPAL]
     ,[QTYPAL]
     ,[PRIORITY]
     ,[PRINTED]
     ,[USERID]
     ,[CZ_REZ1_Track]
     ,[CZ_REZ2_Track]
     ,[ITEMCODE]
     ,[WEIGHT]
     ,[Realization_Start]
     ,[Realization_Stop]
     ,[CZ_Expirace_Track]
)
SELECT
		'''' + CAST(@cisloDavky AS NVARCHAR(10)) + '''' AS [CountEntries],
		''''''''-'''''''' AS [SOPNUMBE],
		[ITEMNMBR],
		''''''''V'''''''' AS [ITEMTYPE],
		[ITEMDESC],
		''''''''-'''''''' AS [VNDDOCNM],
		[VNDITNUM],
     1 AS [ORD],
		[CZ_CarKod],
		[SKL_ID],  
		[LOCNCODE],
		[MJ],
		[QTY] AS [QTYSHPPD],
		[QTYPACK],
		''''''''0'''''''' AS [CZ_DatVyr_Track],
		''''''''0'''''''' AS [CZ_DatVyr_Delka],
		[CZ_SerNum_Track],
		''''''''0'''''''' AS [CZ_SerNum_Delka],
		''''''''0'''''''' AS [CZ_SW_Track],
		''''''''0'''''''' AS [CZ_SW_Delka],
		''''''''0'''''''' AS [CZ_Doslo],
		''''''''-'''''''' AS [Note],
		''''''''0'''''''' AS [TYPEPAL],
		0 AS [QTYPAL],
		''''''''3'''''''' AS [PRIORITY],
		''''''''0'''''''' AS [PRINTED],
		NULL AS [USERID],
		''''''''0'''''''' AS [CZ_REZ1_Track],
		''''''''0'''''''' AS [CZ_REZ2_Track],
		''''''''0'''''''' AS [ITEMCODE],
		[WEIGHT],
		GETDATE() AS [Realization_Start],
		GETDATE() AS [Realization_Stop],
		[CZ_Expirace_Track]
FROM '''' + QUOTENAME(@DatabaseName) + ''''.dbo.[FASK_ZASOBY];
'''';
-- Struktura FASK_ZASOBY
   --,[ITEMNMBR]
   --   ,[ITEMDESC]
   --   ,[ITEMCODE]
   --   ,[VNDITNUM]
   --   ,[CZ_CarKod]
   --   ,[LOCNCODE]
   --   ,[SKL_ID]
   --   ,[QTY]
   --   ,[QTYPACK]
   --   ,[MJ]
   --   ,[DMJ]
   --   ,[TAXRATE]
   --   ,[PRICE0]
   --   ,[PRICE1]
   --   ,[PRICE2]
   --   ,[PRICE3]
   --   ,[PRICE4]
   --   ,[PRICE5]
   --   ,[CZ_SerNum_Track]
   --   ,[CZ_SerNum_Delka]
   --   ,[CZ_Rez1_Track]
   --   ,[CZ_Rez2_Track]
   --   ,[CZ_Rez3_Track]
   --   ,[CZ_Rez4_Track]
   --   ,[REZ1]
   --   ,[REZ2]
   --   ,[REZ3]
   --   ,[REZ4]
   --   ,[ODB_ID]
   --   ,[mena_ID]
   --   ,[SERLTNUM]
   --   ,[WEIGHT]
   --   ,[TIMEFROM]
   --   ,[TIMETO]
   --   ,[LSTMod]
   --   ,[loginid]
   --   ,[CZ_Expirace_Track]
   --   ,[EXPIRACE]
 EXEC sp_executesql @SQL;

 END
 
 SET ANSI_NULLS ON'';

-- Installation step 425
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY]
	@ExportTypFilter [nvarchar](100),
	@ExportSkladFilter [nvarchar](100),
	@ExportovatPouzeAktivniPolozky [bit],
	@EXZas_DotahovatAlternativniDodavatele [bit],
	@EvidenceSarzi [bit],
	@EvidenceVyrobnichCisel [bit]
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM FASK_ZASOBY



-- Je potřeba implementovat dle konktetnycho IS
-- Tohle je konkretne pro IIS EKONOM AGRO

INSERT INTO [FASK_ZASOBY]
           ([ITEMNMBR]
           ,[ITEMDESC]
           ,[ITEMCODE]
           ,[VNDITNUM]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[QTY]
           ,[QTYPACK]
           ,[MJ]
           ,[DMJ]
           ,[TAXRATE]
           ,[PRICE0]
           ,[PRICE1]
           ,[PRICE2]
           ,[PRICE3]
           ,[PRICE4]
           ,[PRICE5]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_Rez1_Track]
           ,[CZ_Rez2_Track]
           ,[CZ_Rez3_Track]
           ,[CZ_Rez4_Track]
           ,[REZ1]
           ,[REZ2]
           ,[REZ3]
           ,[REZ4]
           ,[ODB_ID]
           ,[mena_ID]
           ,[SERLTNUM]
           ,[WEIGHT]
           ,[TIMEFROM]
           ,[TIMETO]
           ,[LSTMod]
           ,[loginid]
           ,[CZ_Expirace_Track]
           ,[EXPIRACE])
		SELECT
			   [ITEMNMBR]
			   ,[ITEMDESC]
			   ,[ITEMCODE]
			   ,[VNDITNUM]
			   ,[CZ_CarKod]
			   ,[LOCNCODE]
			   ,[SKL_ID]
			   ,[QTY]
			   ,[QTYPACK]
			   ,[MJ]
			   ,[DMJ]
			   ,[TAXRATE]
			   ,[PRICE0]
			   ,[PRICE1]
			   ,[PRICE2]
			   ,[PRICE3]
			   ,[PRICE4]
			   ,[PRICE5]
			   ,[CZ_SerNum_Track]
			   ,[CZ_SerNum_Delka]
			   ,[CZ_Rez1_Track]
			   ,[CZ_Rez2_Track]
			   ,[CZ_Rez3_Track]
			   ,[CZ_Rez4_Track]
			   ,[REZ1]
			   ,[REZ2]
			   ,[REZ3]
			   ,[REZ4]
			   ,[ODB_ID]
			   ,[mena_ID]
			   ,[SERLTNUM]
			   ,[WEIGHT]
			   ,[TIMEFROM]
			   ,[TIMETO]
			   ,[LSTMod]
			   ,[loginid]
			   ,[CZ_Expirace_Track]
			   ,[EXPIRACE] FROM
(	
	SELECT 
			   Left(ZAS.KodPol ,40) as ITEMNMBR,  -- ID POLOZKY
			   Left(ZAS.NazevPol, 100) as ITEMDESC,
			   Left(ZAS.KodPol ,70) as ITEMCODE, -- KOD KARTY
			   Left(ZAS.CarKodpol, 60) as VNDITNUM, -- Treba uprasnit s AGRO .... potrebuju EAN zakladny MJ kde QTYPACK je 0 a pak EAN Alternativnej MJ kde je QTYPACK prepočovy koeficient
			   '''''''' as CZ_CarKod,
			   '''''''' as LOCNCODE, 
			   ''''1'''' as SKL_ID, -- Treba uprasnit s AGRO použivany sklad a jeho ID
			   0 as QTY,  -- Treba upresnit....
			   ZAS.PrepKoefPaleta as QTYPACK, -- bude se dotahovat k alert MJ kteru je treba dopresnit
			   Left(ZAS.MjAlter ,5) as MJ, -- treba dopresnit
			   '''''''' as DMJ,
			   0 as TAXRATE,
			   0 as PRICE0,
			   0 as PRICE1,
			   0 as PRICE2,
			   0 as PRICE3,
			   0 as PRICE4,
			   0 as PRICE5,
			   0 as CZ_SerNum_Track, -- DOPLNIT AGRO
			   0 as CZ_SerNum_Delka,
			   0 as CZ_Rez1_Track,
			   0 as CZ_Rez2_Track, 
			   0 as CZ_Rez3_Track, 
			   0 as CZ_Rez4_Track, 
			   0 as REZ1,
			   0 as REZ2,
			   0 as REZ3,
			   0 as REZ4,
			   null as ODB_ID,
			   '''''''' as mena_ID,
			   '''''''' as SERLTNUM,
			   null as WEIGHT,
			   null as TIMEFROM,
			   null as TIMETO,
			   GETDATE() as LSTMod,
			   '''''''' as loginid,
			   0 as CZ_Expirace_Track,
			   null as EXPIRACE
		FROM [agrocs-ekonom].dbo.V_IIS_CI_CiselnikVyrobkuProFask1 as ZAS 
		where ZAS.MjAlter != '''''''' or ZAS.MjAlter is not null
		) as X
		order by x.ITEMNMBR

SELECT Count(*) from FASK_ZASOBY

END

/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASKEvents_Archivace]    Script Date: 28.03.2022 14:00:46 ******/
SET ANSI_NULLS ON'';

-- Installation step 426
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY_DB]
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM FASK_ZASOBY;

    DECLARE @select nvarchar(max) =
N''''
INSERT INTO [FASK_ZASOBY]
           ([ITEMNMBR]
           ,[ITEMDESC]
           ,[ITEMCODE]
           ,[VNDITNUM]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[QTY]
           ,[QTYPACK]
           ,[MJ]
           ,[DMJ]
           ,[TAXRATE]
           ,[PRICE0]
           ,[PRICE1]
           ,[PRICE2]
           ,[PRICE3]
           ,[PRICE4]
           ,[PRICE5]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_Rez1_Track]
           ,[CZ_Rez2_Track]
           ,[CZ_Rez3_Track]
           ,[CZ_Rez4_Track]
           ,[REZ1]
           ,[REZ2]
           ,[REZ3]
           ,[REZ4]
           ,[ODB_ID]
           ,[mena_ID]
           ,[SERLTNUM]
           ,[WEIGHT]
           ,[TIMEFROM]
           ,[TIMETO]
           ,[LSTMod]
           ,[loginid]
           ,[CZ_Expirace_Track]
           ,[EXPIRACE])
SELECT
       [ITEMNMBR]
      ,[ITEMDESC]
      ,[ITEMCODE]
      ,[VNDITNUM]
      ,[CZ_CarKod]
      ,[LOCNCODE]
      ,[SKL_ID]
      ,[QTY]
      ,[QTYPACK]
      ,[MJ]
      ,[DMJ]
      ,[TAXRATE]
      ,[PRICE0]
      ,[PRICE1]
      ,[PRICE2]
      ,[PRICE3]
      ,[PRICE4]
      ,[PRICE5]
      ,[CZ_SerNum_Track]
      ,[CZ_SerNum_Delka]
      ,[CZ_Rez1_Track]
      ,[CZ_Rez2_Track]
      ,[CZ_Rez3_Track]
      ,[CZ_Rez4_Track]
      ,[REZ1]
      ,[REZ2]
      ,[REZ3]
      ,[REZ4]
      ,[ODB_ID]
      ,[mena_ID]
      ,[SERLTNUM]
      ,[WEIGHT]
      ,[TIMEFROM]
      ,[TIMETO]
      ,[LSTMod]
      ,[loginid]
      ,[CZ_Expirace_Track]
      ,[EXPIRACE]
FROM
(
    SELECT
           LEFT(ZAS.KodPol, 40)   AS ITEMNMBR,
           LEFT(ZAS.NazevPol,100) AS ITEMDESC,
           LEFT(ZAS.KodPol, 70)   AS ITEMCODE,
           LEFT(ZAS.CarKodpol,60) AS VNDITNUM,
           ''''''''''''''''                   AS CZ_CarKod,
           ''''''''''''''''                   AS LOCNCODE,
           1                      AS SKL_ID,      -- <- oprava: bez apostrofů
           0                      AS QTY,
           ZAS.PrepKoefPaleta     AS QTYPACK,
           LEFT(ZAS.MjAlter, 5)   AS MJ,
           ''''''''''''''''                   AS DMJ,
           0                      AS TAXRATE,
           0                      AS PRICE0,
           0                      AS PRICE1,
           0                      AS PRICE2,
           0                      AS PRICE3,
           0                      AS PRICE4,
           0                      AS PRICE5,
           0                      AS CZ_SerNum_Track,
           0                      AS CZ_SerNum_Delka,
           0                      AS CZ_Rez1_Track,
           0                      AS CZ_Rez2_Track,
           0                      AS CZ_Rez3_Track,
           0                      AS CZ_Rez4_Track,
           0                      AS REZ1,
           0                      AS REZ2,
           0                      AS REZ3,
           0                      AS REZ4,
           NULL                   AS ODB_ID,
           ''''''''''''''''                   AS mena_ID,
           ''''''''''''''''                   AS SERLTNUM,
           NULL                   AS WEIGHT,
           NULL                   AS TIMEFROM,
           NULL                   AS TIMETO,
           GETDATE()              AS LSTMod,
           ''''''''''''''''                   AS loginid,
           0                      AS CZ_Expirace_Track,
           NULL                   AS EXPIRACE
    FROM [agrocs-ekonom].dbo.V_IIS_CI_CiselnikVyrobkuProFask1 AS ZAS
    WHERE ZAS.MjAlter IS NOT NULL AND ZAS.MjAlter <> ''''''''''''''''     -- <- oprava: AND
) AS X
ORDER BY X.ITEMNMBR;
'''';

    SELECT @select AS SelectTemplate;
END'';

-- Installation step 427
EXEC sys.sp_executesql N''CREATE   PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY_DB_TEST]

	@SKL_ID [nvarchar](20) = NULL

WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

	  -- normalizace: NULL / "" / "   " => ""
    DECLARE @skl nvarchar(20) = LTRIM(RTRIM(ISNULL(@SKL_ID, N'''''''')));

    -- literal ''''...''''
    DECLARE @sklLit nvarchar(100) = N'''''''''''''''' + REPLACE(@skl, N'''''''''''''''', N'''''''''''''''''''''''') + N'''''''''''''''';

    DECLARE @select nvarchar(max) = N''''
SELECT
    LEFT(ZAS.KodPol, 40)    AS ITEMNMBR,
    LEFT(ZAS.NazevPol, 100) AS ITEMDESC,
    LEFT(ZAS.KodPol, 70)    AS ITEMCODE,
    LEFT(ZAS.CarKodpol, 60) AS VNDITNUM,
    CAST('''''''''''''''' AS nvarchar(70)) AS CZ_CarKod,
    CAST('''''''''''''''' AS nvarchar(11)) AS LOCNCODE,
    CAST('''' + @sklLit + N'''' AS nvarchar(20)) AS SKL_ID,

    CAST(0 AS numeric(19,5)) AS QTY,
    CAST(ZAS.PrepKoefPaleta AS numeric(19,5)) AS QTYPACK,

    LEFT(ZAS.MjAlter, 10)   AS MJ,
    CAST('''''''''''''''' AS nvarchar(200)) AS DMJ,

    CAST(0 AS numeric(4,2))   AS TAXRATE,
    CAST(0 AS numeric(18,2))  AS PRICE0,
    CAST(0 AS numeric(18,2))  AS PRICE1,
    CAST(0 AS numeric(18,2))  AS PRICE2,
    CAST(0 AS numeric(18,2))  AS PRICE3,
    CAST(0 AS numeric(18,2))  AS PRICE4,
    CAST(0 AS numeric(18,2))  AS PRICE5,

    CAST(0 AS tinyint)   AS CZ_SerNum_Track,
    CAST(0 AS smallint)  AS CZ_SerNum_Delka,
    CAST(0 AS tinyint)   AS CZ_Rez1_Track,
    CAST(0 AS tinyint)   AS CZ_Rez2_Track,
    CAST(0 AS tinyint)   AS CZ_Rez3_Track,
    CAST(0 AS tinyint)   AS CZ_Rez4_Track,

    CAST(NULL AS nvarchar(50)) AS REZ1,
    CAST(NULL AS nvarchar(50)) AS REZ2,
    CAST(NULL AS nvarchar(50)) AS REZ3,
    CAST(NULL AS nvarchar(50)) AS REZ4,

    CAST(NULL AS nvarchar(12)) AS ODB_ID,
    CAST('''''''''''''''' AS nvarchar(10)) AS mena_ID,
    CAST('''''''''''''''' AS nvarchar(50)) AS SERLTNUM,
    CAST(NULL AS numeric(19,5)) AS WEIGHT,

    CAST(NULL AS datetime) AS TIMEFROM,
    CAST(NULL AS datetime) AS TIMETO,
    GETDATE()              AS LSTMod,
    CAST('''''''''''''''' AS nvarchar(20)) AS loginid,

    CAST(0 AS tinyint) AS CZ_Expirace_Track,
    CAST(NULL AS datetime) AS EXPIRACE
FROM [agrocs-ekonom].dbo.V_IIS_CI_CiselnikVyrobkuProFask1 AS ZAS
WHERE 1= 1 
AND ZAS.MjAlter IS NOT NULL
AND ZAS.MjAlter <> ''''''''''''''''
AND ('''' + @sklLit + N'''' = '''''''''''''''' OR ZAS.VyrSklad = '''' + @sklLit + N'''')
ORDER BY LEFT(ZAS.KodPol, 40);
'''';

DELETE FROM FASK_ZASOBY


    SELECT @select AS SelectTemplate;
END'';

-- Installation step 428
EXEC sys.sp_executesql N''-- =============================================
-- FASK_proc_FEFOFIFO: puvodne procProdejOverLokaci
-- Vraci seznam (select) doporucenych lokaci, odkud je mozne brat material (nasledne je tento seznam zobrazen).
-- seznam SERIAL poskládaný od nejstaršího k nejnovějšímu naskladnění
-- 22.3.2021 JiS : rozsireni o polozku @locncode, kdy se chce vsechen material na lokaci => itemnmbr a serlnmbr nemusi byt nastaveno

--Zalozil: Ing. Matouš Rathouzský 6.8.2025 
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_FEFOFIFO]
    @Itemnmbr NVarChar(31), -- cislo zvolene polozky (CZMST095.ITEMNMBR)
    @Skl_id NVarChar(20), -- ID vybraneho skladu (CZMST_DI.SKL_ID)
	@Serltnum NVarChar(21), -- sarze (CZMST095.SERLTNUM)
    @doc_id NVarChar(12), -- id dokladu (CZMST092.doc_id)
	@locncode nvarchar(20) -- zbozi pouze na pozadovane lokaci

AS
BEGIN
	SET NOCOUNT ON;

	if ltrim(rtrim(@doc_id))   = '''''''' set @doc_id = NULL
	if ltrim(rtrim(@Itemnmbr)) = '''''''' set @Itemnmbr = NULL
	if ltrim(rtrim(@Serltnum)) = '''''''' set @Serltnum = NULL
	if ltrim(rtrim(@skl_id))   = '''''''' set @Skl_id = NULL
	if ltrim(rtrim(@locncode)) = '''''''' set @locncode = NULL

	--SELECT
	--	(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
	--	Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
	--	[CENIK].[NAZEV MAT] as [ITEMDESC],
	--	Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
	--	[SERIAL].[DAT ZARUKA] as [EXPIRACE],
	--	[SERIAL].[DAT PRIJEM] as [PRIJEM],
	--	Coalesce([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM], GetDate()) as [RAZENI],
	--	Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
	--	Left([LOKACE].[LOKACE ID], 20) AS [LOCNCODE],
	--	Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
	--	IsNull([SERIAL].[BLOK], 0) as [BLOKACE] --[BLOKOVANO]
	--FROM
	--	[LOKACE] 
	--	LEFT JOIN [SERIAL] ON 1=1
	--		AND [LOKACE].[MAT ID] = [SERIAL].[MAT ID]
	--		AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
	--	LEFT JOIN [CENIK] ON 1=1
	--		AND [CENIK].[MAT ID] = [LOKACE].[MAT ID]
	--WHERE 1=1
	--	AND isnull([LOKACE].[MAT ID], '''''''')		= Coalesce(@Itemnmbr, isnull([LOKACE].[MAT ID], ''''''''))
	--	AND isnull([LOKACE].[SERIAL ID], '''''''')	= Coalesce(@Serltnum, isnull([LOKACE].[SERIAL ID], ''''''''))
	--	AND isnull([LOKACE].[SKLAD ID], '''''''')		= Coalesce(@Skl_id,   isnull([LOKACE].[SKLAD ID], ''''''''))
	--	AND isnull([LOKACE].[LOKACE ID], '''''''')	= Coalesce(@locncode, isnull([LOKACE].[LOKACE ID], ''''''''))
	--	AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
	--	/*AND
	--	(
	--		(
	--			@doc_id IN (''''preDoVyr'''', ''''Vydej'''', ''''vratka'''', ''''zmenaLokace'''', ''''zmnLokSTisk'''')
	--			AND [LOKACE].[MNOZSTVI] > 0
	--		)
	--		OR @doc_id = ''''vratka''''
	--		OR @doc_id = ''''zmenaLokace''''
	--		OR @doc_id = ''''zmnLokSTisk''''
	--	)*/
	--	AND [SERIAL].[BLOK] = 0
	--ORDER BY
	--	CASE [BLOK] WHEN 0 THEN ''''A'''' ELSE ''''Z'''' END ASC
	--	,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
	--	[LOKACE].[MAT ID] ASC
	--	,[LOKACE].[SERIAL ID] ASC

SELECT	
	[DEX_ROW_ID] AS [Index],
	[ITEMNMBR] AS [ITEMNMBR],
	[ITEMDESC] AS [ITEMDESC],
	[SERLTNUM] AS [SERLTNUM],
	NULL AS [EXPIRACE],
	NULL AS [PRIJEM],
	GETDATE() AS [RAZENI],
	--Cast(0 As numeric(19,5)) AS [QTYSHPPD],
	[QTY] AS [QTYSHPPD],
	[LOCNCODE] AS [LOCNCODE],
	[SKL_ID] AS [SKL_ID],
	0 AS [BLOKACE]
	FROM [FASK].[dbo].[FASK_ZASOBY]


 --MaR defaultni hodnoty
 --SELECT
	--1 AS [Index],
	--'''''''' AS [ITEMNMBR],
	--'''''''' AS [ITEMDESC],
	--'''''''' AS [SERLTNUM],
	--NULL AS [EXPIRACE],
	--NULL AS [PRIJEM],
	--GETDATE() AS [RAZENI],
	--Cast(0 As numeric(19,5)) AS [QTYSHPPD],
	--'''''''' AS [LOCNCODE],
	--'''''''' AS [SKL_ID],
	--0 AS [BLOKACE]




END'';

-- Installation step 429
EXEC sys.sp_executesql N''-- =============================================
-- FASK_proc_FEFOFIFO: puvodne procProdejOverLokaci
-- Vraci seznam (select) doporucenych lokaci, odkud je mozne brat material (nasledne je tento seznam zobrazen).
-- seznam SERIAL poskládaný od nejstaršího k nejnovějšímu naskladnění
-- 22.3.2021 JiS : rozsireni o polozku @locncode, kdy se chce vsechen material na lokaci => itemnmbr a serlnmbr nemusi byt nastaveno

--Zalozil: Ing. Matouš Rathouzský 7.8.2025 
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_FEFOFIFO_2]
    @Itemnmbr NVarChar(31), -- cislo zvolene polozky (CZMST095.ITEMNMBR)
    @Skl_id NVarChar(20), -- ID vybraneho skladu (CZMST_DI.SKL_ID)
	@Serltnum NVarChar(21), -- sarze (CZMST095.SERLTNUM)
    @doc_id NVarChar(12), -- id dokladu (CZMST092.doc_id)
	@locncode nvarchar(20) -- zbozi pouze na pozadovane lokaci

AS
BEGIN
	SET NOCOUNT ON;

	if ltrim(rtrim(@doc_id))   = '''''''' set @doc_id = NULL
	if ltrim(rtrim(@Itemnmbr)) = '''''''' set @Itemnmbr = NULL
	if ltrim(rtrim(@Serltnum)) = '''''''' set @Serltnum = NULL
	if ltrim(rtrim(@skl_id))   = '''''''' set @Skl_id = NULL
	if ltrim(rtrim(@locncode)) = '''''''' set @locncode = NULL

	--SELECT
	--	(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
	--	Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
	--	[CENIK].[NAZEV MAT] as [ITEMDESC],
	--	Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
	--	[SERIAL].[DAT ZARUKA] as [EXPIRACE],
	--	[SERIAL].[DAT PRIJEM] as [PRIJEM],
	--	Coalesce([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM], GetDate()) as [RAZENI],
	--	Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
	--	Left([LOKACE].[LOKACE ID], 20) AS [LOCNCODE],
	--	Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
	--	IsNull([SERIAL].[BLOK], 0) as [BLOKACE] --[BLOKOVANO]
	--FROM
	--	[LOKACE] 
	--	LEFT JOIN [SERIAL] ON 1=1
	--		AND [LOKACE].[MAT ID] = [SERIAL].[MAT ID]
	--		AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
	--	LEFT JOIN [CENIK] ON 1=1
	--		AND [CENIK].[MAT ID] = [LOKACE].[MAT ID]
	--WHERE 1=1
	--	AND isnull([LOKACE].[MAT ID], '''''''')		= Coalesce(@Itemnmbr, isnull([LOKACE].[MAT ID], ''''''''))
	--	AND isnull([LOKACE].[SERIAL ID], '''''''')	= Coalesce(@Serltnum, isnull([LOKACE].[SERIAL ID], ''''''''))
	--	AND isnull([LOKACE].[SKLAD ID], '''''''')		= Coalesce(@Skl_id,   isnull([LOKACE].[SKLAD ID], ''''''''))
	--	AND isnull([LOKACE].[LOKACE ID], '''''''')	= Coalesce(@locncode, isnull([LOKACE].[LOKACE ID], ''''''''))
	--	AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
	--	/*AND
	--	(
	--		(
	--			@doc_id IN (''''preDoVyr'''', ''''Vydej'''', ''''vratka'''', ''''zmenaLokace'''', ''''zmnLokSTisk'''')
	--			AND [LOKACE].[MNOZSTVI] > 0
	--		)
	--		OR @doc_id = ''''vratka''''
	--		OR @doc_id = ''''zmenaLokace''''
	--		OR @doc_id = ''''zmnLokSTisk''''
	--	)*/
	--	AND [SERIAL].[BLOK] = 0
	--ORDER BY
	--	CASE [BLOK] WHEN 0 THEN ''''A'''' ELSE ''''Z'''' END ASC
	--	,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
	--	[LOKACE].[MAT ID] ASC
	--	,[LOKACE].[SERIAL ID] ASC



	--MaR predpripraveny select pro JaS
  SELECT
  [ITEMNMBR] AS ITEMNMBR,
  [ITEMDESC] AS ITEMDESC,
  [QTYSHPPD_DEF] AS QTYSHPPD_DEF,
  [QTYSHPPD] AS QTYSHPPD,
  [SKL_ID] AS SKL_ID,
  [LOCNCODE] AS LOCNCODE,
  [DATECHANGE] AS DATECHANGE,
  [EXPIRATION] AS EXPIRATION,
  1 AS [Index],
  [SERLTNUM] AS SERLTNUM,
  [QTY_OWNER] AS QTY_OWNER,
  [PRAC_ID_OWNER] AS PRAC_ID_OWNER,
  NULL AS PRIJEM,
  NULL AS RAZENI,
  CAST(0 AS bit) AS BLOKACE
   FROM [FASK].[dbo].[CZMST_SkladLokace_Stav]

 --MaR defaultni hodnoty
--SELECT
--  '''''''' AS ITEMNMBR,
--  '''''''' AS ITEMDESC,
--  0.0 AS QTYSHPPD_DEF,
--  0.0 AS QTYSHPPD,
--  '''''''' AS SKL_ID,
--  '''''''' AS LOCNCODE,
--  GETDATE() AS DATECHANGE,
--  NULL AS EXPIRATION,
--  0 AS [Index],
--  '''''''' AS SERLTNUM,
--  0.0 AS QTY_OWNER,
--  '''''''' AS PRAC_ID_OWNER,
--  NULL AS PRIJEM,
--  NULL AS RAZENI,
--  CAST(0 AS bit) AS BLOKACE;

--MaR vychozi tabulka
--SELECT TOP (1000) [ITEMNMBR]
--      ,[ITEMDESC]
--      ,[QTYSHPPD_DEF]
--      ,[QTYSHPPD]
--      ,[SERLTNUM]
--      ,[SKL_ID]
--      ,[LOCNCODE]
--      ,[DATECHANGE]
--      ,[EXPIRATION]
--      ,[QTYSHPPD_DEF_DATE]
--      ,[QTY_OWNER]
--      ,[PRAC_ID_OWNER]
--  FROM [FASK].[dbo].[CZMST_SkladLokace_Stav]


END'';

-- Installation step 430
EXEC sys.sp_executesql N''-- =============================================
-- FASK_proc_FEFOFIFO: puvodne procProdejOverLokaci
-- Vraci seznam (select) doporucenych lokaci, odkud je mozne brat material (nasledne je tento seznam zobrazen).
-- seznam SERIAL poskládaný od nejstaršího k nejnovějšímu naskladnění
-- 22.3.2021 JiS : rozsireni o polozku @locncode, kdy se chce vsechen material na lokaci => itemnmbr a serlnmbr nemusi byt nastaveno

--Zalozil: Ing. Matouš Rathouzský 8.8.2025 
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_FEFOFIFO_3]
    @Itemnmbr NVarChar(31), -- cislo zvolene polozky (CZMST095.ITEMNMBR)
    @Skl_id NVarChar(20), -- ID vybraneho skladu (CZMST_DI.SKL_ID)
	@Serltnum NVarChar(21), -- sarze (CZMST095.SERLTNUM)
    @doc_id NVarChar(12), -- id dokladu (CZMST092.doc_id)
	@locncode nvarchar(20) -- zbozi pouze na pozadovane lokaci

AS
BEGIN
	SET NOCOUNT ON;

	if ltrim(rtrim(@doc_id))   = '''''''' set @doc_id = NULL
	if ltrim(rtrim(@Itemnmbr)) = '''''''' set @Itemnmbr = NULL
	if ltrim(rtrim(@Serltnum)) = '''''''' set @Serltnum = NULL
	if ltrim(rtrim(@skl_id))   = '''''''' set @Skl_id = NULL
	if ltrim(rtrim(@locncode)) = '''''''' set @locncode = NULL

	--SELECT
	--	(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
	--	Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
	--	[CENIK].[NAZEV MAT] as [ITEMDESC],
	--	Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
	--	[SERIAL].[DAT ZARUKA] as [EXPIRACE],
	--	[SERIAL].[DAT PRIJEM] as [PRIJEM],
	--	Coalesce([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM], GetDate()) as [RAZENI],
	--	Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
	--	Left([LOKACE].[LOKACE ID], 20) AS [LOCNCODE],
	--	Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
	--	IsNull([SERIAL].[BLOK], 0) as [BLOKACE] --[BLOKOVANO]
	--FROM
	--	[LOKACE] 
	--	LEFT JOIN [SERIAL] ON 1=1
	--		AND [LOKACE].[MAT ID] = [SERIAL].[MAT ID]
	--		AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
	--	LEFT JOIN [CENIK] ON 1=1
	--		AND [CENIK].[MAT ID] = [LOKACE].[MAT ID]
	--WHERE 1=1
	--	AND isnull([LOKACE].[MAT ID], '''''''')		= Coalesce(@Itemnmbr, isnull([LOKACE].[MAT ID], ''''''''))
	--	AND isnull([LOKACE].[SERIAL ID], '''''''')	= Coalesce(@Serltnum, isnull([LOKACE].[SERIAL ID], ''''''''))
	--	AND isnull([LOKACE].[SKLAD ID], '''''''')		= Coalesce(@Skl_id,   isnull([LOKACE].[SKLAD ID], ''''''''))
	--	AND isnull([LOKACE].[LOKACE ID], '''''''')	= Coalesce(@locncode, isnull([LOKACE].[LOKACE ID], ''''''''))
	--	AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
	--	/*AND
	--	(
	--		(
	--			@doc_id IN (''''preDoVyr'''', ''''Vydej'''', ''''vratka'''', ''''zmenaLokace'''', ''''zmnLokSTisk'''')
	--			AND [LOKACE].[MNOZSTVI] > 0
	--		)
	--		OR @doc_id = ''''vratka''''
	--		OR @doc_id = ''''zmenaLokace''''
	--		OR @doc_id = ''''zmnLokSTisk''''
	--	)*/
	--	AND [SERIAL].[BLOK] = 0
	--ORDER BY
	--	CASE [BLOK] WHEN 0 THEN ''''A'''' ELSE ''''Z'''' END ASC
	--	,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
	--	[LOKACE].[MAT ID] ASC
	--	,[LOKACE].[SERIAL ID] ASC



	--MaR predpripraveny vysledny string pro JaS
--     DECLARE @select NVARCHAR(MAX) =
--N''''SELECT
--    CAST(NULL AS NVARCHAR(255))   AS ITEMNMBR,
--    CAST(NULL AS NVARCHAR(255))   AS ITEMDESC,
--    CAST(NULL AS DECIMAL(38,10))  AS QTYSHPPD_DEF,
--    CAST(NULL AS DECIMAL(38,10))  AS QTYSHPPD,
--    CAST(NULL AS NVARCHAR(255))   AS SKL_ID,
--    CAST(NULL AS NVARCHAR(255))   AS LOCNCODE,
--    CAST(NULL AS DATETIME2(7))    AS DATECHANGE,
--    CAST(NULL AS DATETIME2(7))    AS EXPIRATION,
--    CAST(NULL AS INT)             AS [Index],
--    CAST(NULL AS NVARCHAR(255))   AS SERLTNUM,
--    CAST(NULL AS DECIMAL(38,10))  AS QTY_OWNER,
--    CAST(NULL AS NVARCHAR(255))   AS PRAC_ID_OWNER,
--    CAST(NULL AS DATETIME2(7))    AS PRIJEM,
--    CAST(NULL AS DATETIME2(7))    AS RAZENI,
--    CAST(NULL AS BIT)             AS BLOKACE'''';


     DECLARE @select NVARCHAR(MAX) =
N''''  SELECT
  [ITEMNMBR] AS ITEMNMBR,
  [ITEMDESC] AS ITEMDESC,
  [QTYSHPPD_DEF] AS QTYSHPPD_DEF,
  [QTYSHPPD] AS QTYSHPPD,
  [SKL_ID] AS SKL_ID,
  [LOCNCODE] AS LOCNCODE,
  [DATECHANGE] AS DATECHANGE,
  [EXPIRATION] AS EXPIRATION,
  1 AS [Index],
  [SERLTNUM] AS SERLTNUM,
  [QTY_OWNER] AS QTY_OWNER,
  [PRAC_ID_OWNER] AS PRAC_ID_OWNER,
  NULL AS PRIJEM,
  NULL AS RAZENI,
  CAST(0 AS bit) AS BLOKACE
   FROM [CZMST_SkladLokace_Stav]
   order by EXPIRATION'''';


    SELECT @select AS SelectTemplate;

--MaR zkouska pro pripad prazdneho retezce od ABRA.SAB
	--return ''''''''

END'';

-- Installation step 431
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_FEFOFIFO_4]
	@Itemnmbr [nvarchar](31),
	@Skl_id [nvarchar](20),
	@Serltnum [nvarchar](21),
	@doc_id [nvarchar](12),
	@locncode [nvarchar](20)
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

	if ltrim(rtrim(@doc_id))   = '''''''' set @doc_id =   ''''''''
	if ltrim(rtrim(@Itemnmbr)) = '''''''' set @Itemnmbr = ''''''''
	if ltrim(rtrim(@Serltnum)) = '''''''' set @Serltnum = '''''''' --@Serltnum = NULL
	if ltrim(rtrim(@skl_id))   = '''''''' set @Skl_id =   ''''''''
	if ltrim(rtrim(@locncode)) = '''''''' set @locncode = ''''''''

	--SELECT
	--	(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
	--	Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
	--	[CENIK].[NAZEV MAT] as [ITEMDESC],
	--	Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
	--	[SERIAL].[DAT ZARUKA] as [EXPIRACE],
	--	[SERIAL].[DAT PRIJEM] as [PRIJEM],
	--	Coalesce([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM], GetDate()) as [RAZENI],
	--	Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
	--	Left([LOKACE].[LOKACE ID], 20) AS [LOCNCODE],
	--	Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
	--	IsNull([SERIAL].[BLOK], 0) as [BLOKACE] --[BLOKOVANO]
	--FROM
	--	[LOKACE] 
	--	LEFT JOIN [SERIAL] ON 1=1
	--		AND [LOKACE].[MAT ID] = [SERIAL].[MAT ID]
	--		AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
	--	LEFT JOIN [CENIK] ON 1=1
	--		AND [CENIK].[MAT ID] = [LOKACE].[MAT ID]
	--WHERE 1=1
	--	AND isnull([LOKACE].[MAT ID], '''''''')		= Coalesce(@Itemnmbr, isnull([LOKACE].[MAT ID], ''''''''))
	--	AND isnull([LOKACE].[SERIAL ID], '''''''')	= Coalesce(@Serltnum, isnull([LOKACE].[SERIAL ID], ''''''''))
	--	AND isnull([LOKACE].[SKLAD ID], '''''''')		= Coalesce(@Skl_id,   isnull([LOKACE].[SKLAD ID], ''''''''))
	--	AND isnull([LOKACE].[LOKACE ID], '''''''')	= Coalesce(@locncode, isnull([LOKACE].[LOKACE ID], ''''''''))
	--	AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
	--	/*AND
	--	(
	--		(
	--			@doc_id IN (''''preDoVyr'''', ''''Vydej'''', ''''vratka'''', ''''zmenaLokace'''', ''''zmnLokSTisk'''')
	--			AND [LOKACE].[MNOZSTVI] > 0
	--		)
	--		OR @doc_id = ''''vratka''''
	--		OR @doc_id = ''''zmenaLokace''''
	--		OR @doc_id = ''''zmnLokSTisk''''
	--	)*/
	--	AND [SERIAL].[BLOK] = 0
	--ORDER BY
	--	CASE [BLOK] WHEN 0 THEN ''''A'''' ELSE ''''Z'''' END ASC
	--	,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
	--	[LOKACE].[MAT ID] ASC
	--	,[LOKACE].[SERIAL ID] ASC



	--MaR predpripraveny vysledny string pro JaS
--     DECLARE @select NVARCHAR(MAX) =
--N''''SELECT
--    CAST(NULL AS NVARCHAR(255))   AS ITEMNMBR,
--    CAST(NULL AS NVARCHAR(255))   AS ITEMDESC,
--    CAST(NULL AS DECIMAL(38,10))  AS QTYSHPPD_DEF,
--    CAST(NULL AS DECIMAL(38,10))  AS QTYSHPPD,
--    CAST(NULL AS NVARCHAR(255))   AS SKL_ID,
--    CAST(NULL AS NVARCHAR(255))   AS LOCNCODE,
--    CAST(NULL AS DATETIME2(7))    AS DATECHANGE,
--    CAST(NULL AS DATETIME2(7))    AS EXPIRATION,
--    CAST(NULL AS INT)             AS [Index],
--    CAST(NULL AS NVARCHAR(255))   AS SERLTNUM,
--    CAST(NULL AS DECIMAL(38,10))  AS QTY_OWNER,
--    CAST(NULL AS NVARCHAR(255))   AS PRAC_ID_OWNER,
--    CAST(NULL AS DATETIME2(7))    AS PRIJEM,
--    CAST(NULL AS DATETIME2(7))    AS RAZENI,
--    CAST(NULL AS BIT)             AS BLOKACE'''';




-- už máte nahoře:
-- IF LTRIM(RTRIM(@Serltnum)) = '''''''' SET @Serltnum = NULL;

--DECLARE @where  nvarchar(max) = N'''''''';
--IF @Serltnum IS NOT NULL
--BEGIN
--    SET @where = N'''' WHERE SERLTNUM = N'''''''''''' 
--               + REPLACE(@Serltnum, '''''''''''''''', '''''''''''''''''''''''') 
--               + N'''''''''''''''';
--END

--DECLARE @select nvarchar(max) =
--N''''  SELECT
--  [ITEMNMBR],[ITEMDESC],[QTYSHPPD_DEF],[QTYSHPPD],[SKL_ID],[LOCNCODE],
--  [DATECHANGE],[EXPIRATION],1 AS [Index],[SERLTNUM],[QTY_OWNER],
--  [PRAC_ID_OWNER],NULL AS PRIJEM,NULL AS RAZENI,CAST(0 AS bit) AS BLOKACE,
--  1 AS State,N''''''''Šarže již vyexpirovala!'''''''' AS [Message]
--  FROM [FASKABRA_SAB_POLOHOVANI].[dbo].[CZMST_SkladLokace_Stav]''''
--+ @where +
--N'''' ORDER BY [EXPIRATION]'''';

--SELECT @select AS SelectTemplate;

--DECLARE @select nvarchar(max) =
--N''''  SELECT
--  [ITEMNMBR],[ITEMDESC],[QTYSHPPD_DEF],[QTYSHPPD],[SKL_ID],[LOCNCODE],
--  [DATECHANGE],[EXPIRATION],1 AS [Index],[SERLTNUM],[QTY_OWNER],
--  [PRAC_ID_OWNER],NULL AS PRIJEM,NULL AS RAZENI,CAST(0 AS bit) AS BLOKACE,
--  0 AS State,N'''''''''''''''' AS [Message]
--  FROM [FASKABRA_SAB_POLOHOVANI].[dbo].[CZMST_SkladLokace_Stav]
--  WHERE SERLTNUM = N'''''''''''' + REPLACE(@Serltnum,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
--  ORDER BY [EXPIRATION]'''';

--SELECT @select AS SelectTemplate;

DECLARE @select nvarchar(max) =
N''''  SELECT
  [ITEMNMBR],[ITEMDESC],[QTYSHPPD_DEF],[QTYSHPPD],[SKL_ID],[LOCNCODE],
  [DATECHANGE],[EXPIRATION],1 AS [Index],[SERLTNUM],[QTY_OWNER],
  [PRAC_ID_OWNER],NULL AS PRIJEM,NULL AS RAZENI,CAST(0 AS bit) AS BLOKACE,
  0 AS State,N'''''''''''''''' AS [Message]
  FROM [CZMST_SkladLokace_Stav]
    WHERE 
     SERLTNUM = N'''''''''''' + REPLACE(@Serltnum,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
   ORDER BY [EXPIRATION]'''';
	


	-- AND ITEMNMBR = N'''''''''''' + REPLACE(@Itemnmbr,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
	-- AND SKL_ID = N'''''''''''' + REPLACE(@Skl_id,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
	-- AND LOCNCODE = N'''''''''''' + REPLACE(@locncode,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''

SELECT @select AS SelectTemplate;

--MaR zkouska pro pripad prazdneho retezce od ABRA.SAB
	--return ''''''''

END'';

-- Installation step 432
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 12.10.2020
-- Description:	Procedrua pro FAKE naplneni Inventury do I4
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_Fill_I4_From_I1I2I3]
	-- Add the parameters for the stored procedure here
	@CountEntries int = 1,
	@LOCNCODE_FAKE nvarchar(20) = ''''99'''',
	@SERLTNUM_FAKE nvarchar(20) = ''''66''''	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_I4];

	if @cnt > 0
		BEGIN
			PRINT ''''Tabulka [CZMST_I4] již obsahuje '''' + CAST(@cnt AS NVARCHAR(50)) + '''' záznamů. Záznamy z inventury nebudou přidány.''''
			return -1;
		END

	/*************************/
	--Varianta kdy je položka vedena na šarže, ale šarže neni vyplnena
	--Vyplnuje se FAKE šarže 66
	/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				@SERLTNUM_FAKE as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),'''':'''','''''''') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'''''''' as [REZ_1],
				'''''''' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 2
			AND I2.SERLNMBR is null
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na množství, ale šarže neni vyplnen			
			/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				'''''''' as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),'''':'''','''''''') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'''''''' as [REZ_1],
				'''''''' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 0
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na šarže, a ma šaržu
			/************************/


						INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				ISNULL(case I3.QTYPACK when 0 then SUM(I2.QTY) else I3.QTYPACK * SUM(I2.QTY) END,0) as [QUANTITY],
				SUM(ISNULL(I2.QTY,0)) as QUANTITYMJ,
				I3.QTYPACK,
				ISNULL(I2.SERLNMBR,''''''''),
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),'''':'''','''''''') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'''''''' as [REZ_1],
				'''''''' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
			FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			left join (SELECT SUM(I2.QTY) as QTY_SUM, I1.ITEMNMBR, I1.CountEntries FROM CZMST_I1 as I1
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			group by I1.ITEMNMBR, I1.CountEntries
			) as I_SUM ON I_SUM.ITEMNMBR = I1.ITEMNMBR AND I_SUM.CountEntries = I1.CountEntries
			where I1.CZ_SerNum_Track = 2 AND I2.SERLNMBR is not null AND I1.CountEntries = @CountEntries
			Group by  I3.VNDITNUM, I1.ITEMDESC, I1.ITEMCODE, I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.MJ, I3.QTYPACK, I1.CZ_SerNum_Track, I2.SERLNMBR,  I1.QUANTITY, I_SUM.QTY_SUM
			order by I3.VNDITNUM

			/***************************/
			/*** Experiment s prayzdnim stanem a fake sarži***/

			--			INSERT INTO CZMST_I4 (
			--	[CountEntries]
			--	,[CE_Orig]
			--	,[ITEMNMBR]
			--	,[CZ_CarKod]
			--	,[LOCNCODE]
			--	,[SKL_ID]
			--	,[VNDITNUM]
			--	,[MJ]
			--	,[QUANTITY]
			--	,[QUANTITYMJ]
			--	,[QTYPACK]
			--	,[SERLNMBR]
			--	,[DATEDONE]
			--	,[TIMEDONE]
			--	,[USERID]
			--	,[GUID]
			--	,[O_Checked]
			--	,[INPUT_MODE]
			--	,[ID_TERMINAL]
			--	,[ITEMCODE]
			--	,[REZ_1]
			--	,[REZ_2]
			--	,[WEIGHT]
			--	,[Expirace])
			--SELECT 
			--	2 as CountEntries,
			--	null as CE_Orig,
			--	I1.ITEMNMBR,
			--	I1.CZ_CarKod,
			--	''''99'''' as [LOCNCODE],
			--	I1.SKL_ID,
			--	I3.VNDITNUM,
			--	I3.MJ,
			--	0 as [QUANTITY],
			--	0 as [QUANTITYMJ],
			--	0 as [QTYPACK],
			--	''''66'''' as [SERLNMBR],
			--	convert(varchar, getdate(), 112) as [DATEDONE],
			--	replace(Convert (varchar(8),GetDate(), 108),'''':'''','''''''') as [TIMEDONE],
			--	0 as [USERID],
			--	NEWID() as [GUID],
			--	0 as [O_Checked],
			--	0 as [INPUT_MODE],
			--	99 as [ID_TERMINAL],
			--	I1.ITEMCODE,
			--	'''''''' as [REZ_1],
			--	'''''''' as [REZ_2],
			--	null as [WEIGHT],
			--	null as [Expirace]
			--	FROM CZMST_I1 as I1
			--left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			--left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			--WHERE I1.CZ_SerNum_Track = 2
			--AND I2.SERLNMBR is not null
			--AND I1.CountEntries = 1
			--Group by I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.VNDITNUM, I3.MJ, I1.ITEMDESC, I1.ITEMCODE


SELECT Count(*) FROM CZMST_I4 where CountEntries = @CountEntries

END


/******************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_NaplnLokMechZ_INV] ******/
SET ANSI_NULLS ON'';

-- Installation step 433
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Bc Tadeas Divacky
-- Create date: 
-- Description:	Procedura Pro naplneni LokMech z Inventury
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_NaplnLokMechZ_INV] 
	@CountEntries int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

	if @cnt > 0
		BEGIN
			PRINT ''''Tabulka [CZMST_SkladLokace_Stav] již obsahuje '''' + CAST(@cnt AS NVARCHAR(50)) + '''' záznamů. Záznamy z inventury nebudou přidány.''''
			return -1;
		END

		INSERT INTO [CZMST_SkladLokace_Stav]
			([ITEMNMBR],
			[ITEMDESC],
			[QTYSHPPD_DEF],
			[QTYSHPPD],
			[SERLTNUM],
			[SKL_ID],
			[LOCNCODE],
			[DATECHANGE],
			[EXPIRATION],
			[QTYSHPPD_DEF_DATE],
			[QTY_OWNER],
			[PRAC_ID_OWNER])
		SELECT
			i4.ITEMNMBR as ITEMNMBR,
			ISNULL(i1.ITEMDESC, '''''''') as ITEMDESC,
			SUM(i4.QUANTITY) as QTYSHPPD_DEF,
			SUM(i4.QUANTITY) as QTYSHPPD,
			ISNULL(i4.SERLNMBR, '''''''') as SERLTNUM,
			ISNULL(i4.skl_id, '''''''') as SKL_ID,
			ISNULL(i4.LOCNCODE, '''''''') as LOCNCODE,
			GETDATE() as DATECHANGE,
			i4.Expirace as EXPIRATION,
			GETDATE() as QTYSHPPD_DEF_DATE,
			0 as [QTY_OWNER],
			null as [PRAC_ID_OWNER]
			from [CZMST_I4] i4
			left join [CZMST_I1] i1 on i1.ITEMNMBR = i4.ITEMNMBR
			WHERE  I4.CountEntries = @CountEntries
			GROUP BY i4.ITEMNMBR, i1.ITEMDESC, i4.SERLNMBR, i4.skl_id, i4.LOCNCODE, i4.Expirace


		select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

		PRINT ''''Lokační mechanismus úspešně naplněn inventurními daty dne '''' + CAST(GETDATE() AS NVARCHAR(50))  + ''''. Do lokačního mechanismu bylo přidáno '''' + CAST(@cnt AS NVARCHAR(50)) + '''' nových záznamů.'''';

END

/*********************************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_Insert_VyrobaTP] ******/
SET ANSI_NULLS ON'';

-- Installation step 434
EXEC sys.sp_executesql N''--=============================================================================
--Author: Ing. Matous Rathouzsky
--Create date: 26.6.2025



--=============================================================================

CREATE PROCEDURE [dbo].[fask_proc_PriznakSledovaniZasoby]
	@Databaze nvarchar(128),               -- např. ''''StwPh_04535667_2020''''
	@RelSKzVC int = NULL,
	@ID int,
	@EvidenceSarzi bit,
	@EvidenceVyrobnichCisel bit,
	@PohodaE1 bit,
	@Result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @VPrFXTS int = 0;
	DECLARE @VPrFDTS int = 0;
	DECLARE @RefVPrFXTS int = 0;
	DECLARE @RefVPrFDTS int = 0;
	DECLARE @VPrCZSerNumTrIS int = 0;

	SET @Result = ISNULL(@RelSKzVC, 0);

	IF @Result = 0
	BEGIN
		IF @PohodaE1 = 1
		BEGIN
			DECLARE @SQL nvarchar(max);
			DECLARE @Params nvarchar(max) = N''''@ID int, @VPrFXTS int OUTPUT, @VPrFDTS int OUTPUT, @RefVPrFXTS int OUTPUT, @RefVPrFDTS int OUTPUT'''';

			SET @SQL = N''''
				SELECT TOP 1
					  @VPrFXTS = VPrFXTS,
					  @VPrFDTS = VPrFDTS,
					  @RefVPrFXTS = RefVPrFXTS,
					  @RefVPrFDTS = RefVPrFDTS
				FROM '''' + QUOTENAME(@Databaze) + ''''.dbo.SKz WHERE ID = @ID'''';

			EXEC sp_executesql @SQL, @Params, 
				@ID = @ID, 
				@VPrFXTS = @VPrFXTS OUTPUT,
				@VPrFDTS = @VPrFDTS OUTPUT,
				@RefVPrFXTS = @RefVPrFXTS OUTPUT,
				@RefVPrFDTS = @RefVPrFDTS OUTPUT;

			IF ISNULL(@VPrFXTS, 0) = 1
			BEGIN
				SET @Result = ISNULL(@RefVPrFXTS, 1) - 1;
				IF @Result = 1 AND @EvidenceVyrobnichCisel = 0 SET @Result = 0;
				IF @Result = 2 AND @EvidenceSarzi = 0 SET @Result = 0;
			END
			ELSE IF ISNULL(@VPrFDTS, 0) = 1
			BEGIN
				SET @Result = ISNULL(@RefVPrFDTS, 1) - 1;
				IF @Result = 1 AND @EvidenceVyrobnichCisel = 0 SET @Result = 0;
				IF @Result = 2 AND @EvidenceSarzi = 0 SET @Result = 0;
			END
		END
	END
	ELSE IF @Result = 1 AND @PohodaE1 = 1
	BEGIN
		DECLARE @SQL2 nvarchar(max);
		DECLARE @Params2 nvarchar(max) = N''''@ID int, @VPrCZSerNumTrIS int OUTPUT'''';

		SET @SQL2 = N''''
			SELECT TOP 1 @VPrCZSerNumTrIS = VPrCZSerNumTrIS
			FROM '''' + QUOTENAME(@Databaze) + ''''.dbo.SKz WHERE ID = @ID'''';

		EXEC sp_executesql @SQL2, @Params2, 
			@ID = @ID, 
			@VPrCZSerNumTrIS = @VPrCZSerNumTrIS OUTPUT;

		IF ISNULL(@VPrCZSerNumTrIS, 0) = 1
			SET @Result = 10;
	END
END'';

-- Installation step 435
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_proc_TiskovaUlohaDetail_Get]
    @ID_TU INT
AS
BEGIN
    SET NOCOUNT ON;

select
TU.ID as ID_TU
,TU.POCET as POCET
,TU.HEIGHT_PAPER_SIZE
,TU.WIDTH_PAPER_SIZE
,TU.PAPER_KIND
,T.[ID] as ID_T
      ,T.[NAME]
      ,T.[LOCATION]
      ,T.[IP]
      ,T.[PORT]
      ,T.[COM]
      ,T.[SOUBOR]
      ,T.[TIMEOUT]
      ,T.[BARCODE]
	  ,FFKS.typ_h
	  ,FFKS.typ
,FFKS.formular
    FROM FASK_TISKOVE_ULOHY AS TU
    LEFT JOIN CZMST_TISKARNA AS T ON T.ID = TU.ID_TISKARNY
    LEFT JOIN FASK_FORMULARE AS FF ON FF.ID = TU.ID_FORMULARE
    LEFT JOIN FASK_FORMULARE_KS AS FFKS ON FFKS.ID_H = FF.ID AND FF.TYP = FFKS.TYP_H
    WHERE TU.ID = @ID_TU;
END'';

-- Installation step 436
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_procGetInfo1]
	@CountEntries [nvarchar](50)=NULL,
	@SOPNUMBE [nvarchar](30)=NULL,
	@OutputParam [nvarchar](255) OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN

--SET NOCOUNT ON;

--	if ltrim(rtrim(@CountEntries))   = '''''''' set @CountEntries = NULL
--	if ltrim(rtrim(@SOPNUMBE))       = '''''''' set @SOPNUMBE = NULL

    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    --SET NOCOUNT ON;

    -- Assign the result to the output parameter

--	SELECT @OutputParam = [Note]+'''';Kg1:''''+ CAST([WEIGHT] AS varchar)
--   FROM [CZMST_SE]
--     WHERE CountEntries = @CountEntries;

	SELECT @OutputParam = [Note]+'''' Ahoj''''+[ITEMDESC]
    FROM [CZMST_SE]
    WHERE CountEntries=@CountEntries OR SOPNUMBE = @SOPNUMBE;
	
--	SELECT [Note]+'''' Kg:''''+CAST([WEIGHT] AS varchar)

END'';

-- Installation step 437
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_procGetPolozkaFloat_DB]
@itemnmbr [nvarchar](50)=NULL,
@location [nvarchar](50)=NULL
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

	if ltrim(rtrim(@location))   = '''''''' set @location =   ''''''''
	if ltrim(rtrim(@Itemnmbr)) = '''''''' set @Itemnmbr = ''''''''



DECLARE @select nvarchar(max) =
N''''   SELECT [QTY] 
	from [FASK_ZASOBY]
    WHERE 
	1=1
     AND ITEMNMBR = N'''''''''''' + REPLACE(@itemnmbr,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
	
   ORDER BY [DEX_ROW_ID]'''';
	
	 -- AND SKL_ID = N'''''''''''' + REPLACE(@location,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''

	-- AND ITEMNMBR = N'''''''''''' + REPLACE(@Itemnmbr,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
	-- AND SKL_ID = N'''''''''''' + REPLACE(@Skl_id,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''
	-- AND LOCNCODE = N'''''''''''' + REPLACE(@locncode,'''''''''''''''','''''''''''''''''''''''') + N''''''''''''

SELECT @select AS SelectTemplate;

--MaR zkouska pro pripad prazdneho retezce od ABRA.SAB
	--return ''''''''

END'';

-- Installation step 438
EXEC sys.sp_executesql N''create PROCEDURE [dbo].[FASK_procGetPolozkaFloat_POHODA] --FASK_procGetPolozkaFloat
    @itemnmbr NVARCHAR(50),      -- Vstupní parametr: číslo položky (ID nebo kód položky)
    @location NVARCHAR(50),      -- Vstupní parametr: lokace skladu (zatím nevyužitý)
    @dbname NVARCHAR(128)        -- Vstupní parametr: název databáze, ze které se má číst
AS
BEGIN
    -- Vytvoření proměnné pro dynamické SQL
    DECLARE @sql NVARCHAR(MAX)

    -- Sestavení dynamického SQL dotazu.
    -- V tomto případě dynamicky vkládáme název databáze do FROM části pomocí QUOTENAME (zabraňuje SQL injection)
    SET @sql = ''''
        SELECT StavZ                            -- Název sloupce, který chceme vrátit
        FROM '''' + QUOTENAME(@dbname) + ''''.dbo.SKz -- Dynamické určení databáze a tabulky (dbo.SKz)
        WHERE ID = @itemnmbr_param              -- Filtrování podle parametru ID (musí být int!)
    ''''

    -- Provedení dynamického SQL pomocí sp_executesql s parametry
    EXEC sp_executesql 
        @sql,                                   -- Samotný SQL řetězec
        N''''@itemnmbr_param NVARCHAR(50)'''',        -- Deklarace typu parametru
        @itemnmbr_param = @itemnmbr             -- Přiřazení hodnoty parametru
END

--| Proč nelze použít SELECT s proměnnou? | Protože název databáze (např. StwPh_06480853_2024) musí být pevný text, ne parametr |
--| Proč sp_executesql? | Umožňuje spouštět SQL jako text a přitom bezpečně používat parametry |
--| Alternativa? | EXEC(@sql) – ale bez parametrů, méně bezpečné a bez optimalizace |'';

-- Installation step 439
EXEC sys.sp_executesql N''CREATE PROCEDURE [dbo].[FASK_procGetPolozkaFloat_SQL]
	@itemnmbr [nvarchar](50) = NULL,
	@location [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @vysledek FLOAT = 1.5;
    SELECT [QTY] 
	from [FASK_ZASOBY]
	where @itemnmbr = [ITEMNMBR]                            

END;'';

-- Installation step 440
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 3.9.2020
-- Description:	Procedura, generujici aktualni sarzi pro vyrobu
-- =============================================
CREATE PROCEDURE [dbo].[FASK_procGetSarzeVyroba] 
	-- Add the parameters for the stored procedure here
	@smenaID nvarchar(20) = ''''999'''', 
	@userID nvarchar(20) = '''''''',
	@linkaID nvarchar(10) = 0,
	@QTY numeric(19,5) = 0,
	@ITEMNMBR nvarchar(40) = ''''''''
AS
BEGIN
	SET NOCOUNT ON;
	

	declare @datum datetime = getdate()
	declare @pracovnik nvarchar(10) -- maximum bude 10 ...
	declare @den nvarchar(2) = ''''''''
	declare @mesic nvarchar(2) = ''''''''
	declare @rok nvarchar(4) = ''''''''	
			
	set @den = RIGHT(''''0'''' + CONVERT(nvarchar(2), DAY(@datum)), 2)
	set @mesic = RIGHT(''''0'''' + CONVERT(nvarchar(2), MONTH(@datum)), 2)
	set @rok = RIGHT(''''00'''' + CONVERT(nvarchar(4), YEAR(@datum)), 4)
	set @pracovnik = RIGHT(''''0000'''' + CONVERT(nvarchar(4), @userID), 4)
	
	declare @sarze nvarchar(255)
	Set @sarze = @pracovnik + + @rok + @mesic +	@den  	


	SELECT @sarze as Sarze
	
END'';

-- Installation step 441
EXEC sys.sp_executesql N''-- =============================================
-- Author:	Jiri Skrivanek
-- Create date: 3.7.2017
-- Description:	Procedura, generujici aktualni sarzi pro vyrobu
-- =============================================
Create PROCEDURE [dbo].[fask_vyroba_GetSarze] 
	-- Add the parameters for the stored procedure here
	@smenaID nvarchar(20) = ''''999'''', 
	@userID nvarchar(20) = '''''''',
	@linkaID nvarchar(10) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @datum datetime = getdate()
	declare @pracovnik nvarchar(10) -- maximum bude 10 ...
	declare @den nvarchar(2) = ''''''''
	declare @mesic nvarchar(2) = ''''''''
	declare @rok nvarchar(4) = ''''''''	
			
	set @den = RIGHT(''''0'''' + CONVERT(nvarchar(2), DAY(@datum)), 2)
	set @mesic = RIGHT(''''0'''' + CONVERT(nvarchar(2), MONTH(@datum)), 2)
	set @rok = RIGHT(''''00'''' + CONVERT(nvarchar(4), YEAR(@datum)), 4)
	--set @pracovnik = RIGHT(''''0000000000'''' + @smenaID + @userID + @linkaID, 10)
	--set @pracovnik = @smenaID + @userID + @linkaID
	set @pracovnik = RIGHT(''''0000'''' + CONVERT(nvarchar(4), @userID), 4)
	
	declare @sarze nvarchar(255)
	--Set @sarze = @pracovnik + ''''|'''' + @den + ''''|''''+ @mesic + ''''|''''+ @rok
	--Set @sarze = @pracovnik + @den + @mesic + @rok
	Set @sarze = @pracovnik + + @rok + @mesic +	@den  	
	SELECT @sarze as Sarze
	
END'';

-- Installation step 442
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Matous Rathouzsky
-- Create date: 24.1.2022
-- Description:	Vraci pocet archivovanych zaznamu se statusem 0
-- Info:		
-- =============================================
create procedure [dbo].[FASKEvents_Archivace]
(
	-- Add the parameters for the procedure here
	--@cisloLinky int,
	@faskID uniqueidentifier
)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @pocet int
	
    declare @description_1 nvarchar(30)
    declare @description_2 nvarchar(30)
	declare @description_3 nvarchar(30)
	declare @dateevepro nvarchar(20);
    declare @ido_pro nchar(10);
	--declare @description_4 nvarchar(30)
	    
	Set @description_1 = ''''automaticke ulozeni paleta''''
	Set @description_2 = ''''posledni paleta 1''''
	Set @description_3 = ''''posledni paleta 3''''
	set @dateevepro = CONVERT(nvarchar, getdate(), 120)
    set @ido_pro = ''''R:'''' +
				SUBSTRING(@dateevepro, 6, 2) + 
				SUBSTRING(@dateevepro, 9, 2) +
				SUBSTRING(@dateevepro, 12, 2) + 
				--SUBSTRING(@dateevepro, 15, 2) + 
				SUBSTRING(@dateevepro, 18, 2)
	--Set @description_4 = ''''automaticke ulozeni paleta''''

	--najdu kolik je zaznamu a ulozim do promenne pocet 
 set @pocet = (
  SELECT COUNT(status)
  FROM [FASK_Events]
  where productionGuid is not null
  --and machineid = @cisloLinky
  and faskGUID = @faskID
  and (description like @description_1 or description like @description_2 or description like @description_3)
  and (status = 0)
  )

  --zaznamy archivuji
UPDATE [FASK_Events]
   SET [status] = 900
      ,[IDO] = @ido_pro
 where productionGuid is not null
  --and machineid = @cisloLinky
   and faskGUID = @faskID
  and (description like @description_1 or description like @description_2 or description like @description_3)
  and (status = 0)
                


	-- Return the result of the function
	RETURN @pocet

END'';

-- Installation step 443
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Matous Rathouzsky
-- Create date: 11.2.2022
-- Description:	Vraci pocet archivovanych zaznamu
-- Info:		
-- =============================================
CREATE procedure [dbo].[FASKEvents_Archivace_Vykladka]
(
	-- Add the parameters for the procedure here
	--@cisloLinky int,
	@faskID uniqueidentifier

)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @pocet int
	
    declare @description_1 nvarchar(30)
    declare @description_2 nvarchar(30)
	declare @description_3 nvarchar(30)
	--declare @description_4 nvarchar(30)

	declare @stav int 
	declare @dateevepro nvarchar(20);
	declare @ido_pro nchar(10);
	--declare @description_4 nvarchar(30)
	    
	Set @description_1 = ''''automaticke ulozeni paleta''''
	Set @description_2 = ''''posledni paleta 1''''
	Set @description_3 = ''''posledni paleta 3''''
	set @dateevepro = CONVERT(nvarchar, getdate(), 120)
    set @ido_pro = ''''R:'''' +
				SUBSTRING(@dateevepro, 6, 2) + 
				SUBSTRING(@dateevepro, 9, 2) +
				SUBSTRING(@dateevepro, 12, 2) + 
				--SUBSTRING(@dateevepro, 15, 2) + 
				SUBSTRING(@dateevepro, 18, 2)
	    
	Set @description_1 = ''''presun na streckovacku''''
	--Set @description_2 = ''''posledni paleta 1''''
	--Set @description_3 = ''''posledni paleta 3''''
	--Set @description_4 = ''''automaticke ulozeni paleta''''



	set @pocet = (
  SELECT COUNT(status)
  FROM [FASK_Events]
  where productionGuid is not null
  and faskGUID = @faskID
  and (
      status = 21 or status = 31 or status = 41 or status = 51
  or  status = 22 or status = 32 or status = 42 or status = 52
  or  status = 23 or status = 33 or status = 43 or status = 53
  or  status = 24 or status = 34 or status = 44 or status = 54
  )
  )

  	set @stav = (
  SELECT status
  FROM [FASK_Events]
  where productionGuid is not null
  and faskGUID = @faskID
  and (
      status = 21 or status = 31 or status = 41 or status = 51
  or  status = 22 or status = 32 or status = 42 or status = 52
  or  status = 23 or status = 33 or status = 43 or status = 53
  or  status = 24 or status = 34 or status = 44 or status = 54
  )
  )

  --zaznamy archivuji
UPDATE [FASK_Events]
   SET [status] = (@stav+900)
      ,[IDO] = @ido_pro
 where productionGuid is not null
   and faskGUID = @faskID
   and (
      status = 21 or status = 31 or status = 41 or status = 51
  or  status = 22 or status = 32 or status = 42 or status = 52
  or  status = 23 or status = 33 or status = 43 or status = 53
  or  status = 24 or status = 34 or status = 44 or status = 54
  )

	-- Return the result of the function
	RETURN @pocet

END
/****** Object:  View [dbo].[FASK_Inventura_I123_Compare]    ******/
SET ANSI_NULLS ON'';

-- Installation step 444
EXEC sys.sp_executesql N''CREATE PROCEDURE  [dbo].[proc_CZMST_RFID_Next]
	 @itemnmbr nvarchar(40),
	 @countnumbers int,
	 @minsequencenmbr int OUTPUT,
	 @maxsequencenmbr int OUTPUT
	AS
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
			DECLARE @id int;
			DECLARE @sequencenmbr int;
				
			select @id = ID
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr

			IF @id is NULL 
			BEGIN
				INSERT INTO CZMST_RFID_ITEMS (ITEMNMBR) VALUES (@itemnmbr)
			END

			select @id = ID, @sequencenmbr = SEQUENCENMBR
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr
		
			SET @minsequencenmbr = @sequencenmbr + 1;
			SET @maxsequencenmbr = @sequencenmbr + @countnumbers;
		
			UPDATE CZMST_RFID_ITEMS
			SET    SEQUENCENMBR = @maxsequencenmbr
			WHERE  ID = @id

			-- vraci pocet vracenych cisel, melo by byt stejne jako je @countnumbers
			RETURN (@maxsequencenmbr - @minsequencenmbr + 1);
	END'';

-- Installation step 445
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Matouš Rathouzský
-- Create date: 4.8.2023
-- Description:	presun zaznamu z Production do Production_HISTORY
-- =============================================
CREATE PROCEDURE [dbo].[Production_ArchivaceZaznamu]
    @DatumOd DATETIME,
    @DatumDo DATETIME,
    @PocetPresunutych INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Začneme transakci, abychom zajistili celistvost datových změn.
        BEGIN TRANSACTION;

        -- Přesuneme data z "Production" do "Production_HISTORY" pro záznamy v zadaném rozmezí datumů.
        INSERT INTO [dbo].[Production_HISTORY]
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ITEMTYPE]
           ,[ITEMMJ]
           ,[ITEMDESC]
           ,[ORD]
           ,[TIMEMODE]
           ,[TIMEPREPSTART]
           ,[TIMEPREPSTOP]
           ,[TIMEPREP]
           ,[TIMEUNIT]
           ,[TIMESTART]
           ,[TIMESTOP]
           ,[TIMECORSTART]
           ,[TIMECORSTOP]
           ,[TIMECOR]
           ,[TIMECRID]
           ,[TIMECRIDTYPE]
           ,[loginid]
           ,[machineid]
           ,[operationid]
           ,[dateeve]
           ,[qty]
           ,[qtyReal]
           ,[QTYPACK]
           ,[QTYPACKMJ]
           ,[description]
           ,[BarcodeP]
           ,[UserID]
           ,[TermID]
           ,[ISOK]
           ,[GUID]
           ,[SOUBEHGUID]
           ,[CORRGUID]
           ,[qtyOld]
           ,[idVS]
           ,[dateedit]
           ,[SKL_ID]
           ,[LOCNCODE]
           ,[SERLTNUM]
           ,[EXPIRATION]
           ,[NMBRPAL]
           ,[TYPEPAL]
           ,[PackType]
           ,[status]
           ,[WEIGHT]
           ,[STORNOGUID]
           ,[REZ_1]
           ,[REZ_2]
           ,[REZ_3]
           ,[REZ_4]
           ,[REZ_5]
           ,[WEIGHT_OLD])
        SELECT [CountEntries]
      ,[SOPNUMBE]
      ,[ITEMNMBR]
      ,[ITEMTYPE]
      ,[ITEMMJ]
      ,[ITEMDESC]
      ,[ORD]
      ,[TIMEMODE]
      ,[TIMEPREPSTART]
      ,[TIMEPREPSTOP]
      ,[TIMEPREP]
      ,[TIMEUNIT]
      ,[TIMESTART]
      ,[TIMESTOP]
      ,[TIMECORSTART]
      ,[TIMECORSTOP]
      ,[TIMECOR]
      ,[TIMECRID]
      ,[TIMECRIDTYPE]
      ,[loginid]
      ,[machineid]
      ,[operationid]
      ,[dateeve]
      ,[qty]
      ,[qtyReal]
      ,[QTYPACK]
      ,[QTYPACKMJ]
      ,[description]
      ,[BarcodeP]
      ,[UserID]
      ,[TermID]
      ,[ISOK]
      ,[GUID]
      ,[SOUBEHGUID]
      ,[CORRGUID]
      ,[qtyOld]
      ,[idVS]
      ,[dateedit]
      ,[SKL_ID]
      ,[LOCNCODE]
      ,[SERLTNUM]
      ,[EXPIRATION]
      ,[NMBRPAL]
      ,[TYPEPAL]
      ,[PackType]
      ,[status]
      ,[WEIGHT]
      ,[STORNOGUID]
      ,[REZ_1]
      ,[REZ_2]
      ,[REZ_3]
      ,[REZ_4]
      ,[REZ_5]
      ,[WEIGHT_OLD]
  FROM [dbo].[Production]
        WHERE dateeve BETWEEN @DatumOd AND @DatumDo;

        -- Zjistíme počet přesunutých záznamů.
        SET @PocetPresunutych = @@ROWCOUNT;

        -- Odstraníme data z "Production" pro záznamy v zadaném rozmezí datumů.
        DELETE FROM [dbo].[Production]
        WHERE dateeve BETWEEN @DatumOd AND @DatumDo;

        -- Pokud přesun proběhne úspěšně, provedeme COMMIT, aby byla transakce dokončena.
        COMMIT TRANSACTION;

        -- Vypíšeme úspěšné provedení procedury.
        PRINT ''''Data byla úspěšně přesunuta do tabulky Production_HISTORY a odstraněna z tabulky Production.'''';
    END TRY
    BEGIN CATCH
        -- Pokud dojde k chybě, provedeme ROLLBACK, aby byly vráceny všechny změny v transakci.
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Vypíšeme chybu.
        PRINT ''''Došlo k chybě při přesunu dat: '''' + ERROR_MESSAGE();
    END CATCH;
END;


SET ANSI_NULLS ON'';

-- Installation step 446
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 4.4. 2024
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	insert do tabulky Production
-- =============================================
CREATE PROCEDURE [dbo].[Production_insert]

	@CountEntries int,
    @SOPNUMBE nvarchar(30),
    @ITEMNMBR nvarchar(40),
    @ITEMTYPE nvarchar(11),
    @ITEMMJ nvarchar(5),
    @ITEMDESC nvarchar(100),
    @ORD int,
    @TIMEMODE int,
    @TIMEPREPSTART datetime,
    @TIMEPREPSTOP datetime,
    @TIMEPREP real,
    @TIMEUNIT real,
    @TIMESTART datetime,
    @TIMESTOP datetime,
    @TIMECORSTART datetime,
    @TIMECORSTOP datetime,
    @TIMECOR real,
    @TIMECRID int,
    @TIMECRIDTYPE tinyint,
    @id int,
    @loginid nvarchar(20),
    @machineid nvarchar(20),
    @operationid nvarchar(16),
    @dateeve datetime,
    @qty numeric(19, 5),
    @qtyReal numeric(19, 5),
    @QTYPACK numeric(19, 5),
    @QTYPACKMJ nvarchar(5),
    @description nvarchar(max),
    @BarcodeP nvarchar(31),
    @UserID nvarchar(20),
    @TermID tinyint,
    @ISOK datetime,
    @GUID uniqueidentifier,
    @SOUBEHGUID uniqueidentifier,
    @CORRGUID uniqueidentifier,
    @qtyOld numeric(19, 5),
    @idVS nvarchar(10),
    @dateedit datetime,
    @SKL_ID nvarchar(20),
    @LOCNCODE nvarchar(11),
    @SERLTNUM nvarchar(50),
    @EXPIRATION nvarchar(50),
    @NMBRPAL nvarchar(50),
    @TYPEPAL nvarchar(10),
    @PackType nvarchar(50),
    @status int,
    @WEIGHT numeric(19, 5),
    @STORNOGUID uniqueidentifier,
    @REZ_1 nvarchar(100),
    @REZ_2 nvarchar(100),
    @REZ_3 nvarchar(100),
    @REZ_4 nvarchar(100),
    @REZ_5 nvarchar(100),
    @WEIGHT_OLD numeric(19, 5)

AS
--Deklarace testu na chybu vlozeni
DECLARE @ins1_error int

	BEGIN

--Zacatek transakce
BEGIN TRAN

		BEGIN	
			--Vlozeni poctu nasnimanych
INSERT INTO [dbo].[Production]
           ([CountEntries]
      ,[SOPNUMBE]
      ,[ITEMNMBR]
      ,[ITEMTYPE]
      ,[ITEMMJ]
      ,[ITEMDESC]
      ,[ORD]
      ,[TIMEMODE]
      ,[TIMEPREPSTART]
      ,[TIMEPREPSTOP]
      ,[TIMEPREP]
      ,[TIMEUNIT]
      ,[TIMESTART]
      ,[TIMESTOP]
      ,[TIMECORSTART]
      ,[TIMECORSTOP]
      ,[TIMECOR]
      ,[TIMECRID]
      ,[TIMECRIDTYPE]
     -- ,[id]
      ,[loginid]
      ,[machineid]
      ,[operationid]
      ,[dateeve]
      ,[qty]
      ,[qtyReal]
      ,[QTYPACK]
      ,[QTYPACKMJ]
      ,[description]
      ,[BarcodeP]
      ,[UserID]
      ,[TermID]
      ,[ISOK]
      ,[GUID]
      ,[SOUBEHGUID]
      ,[CORRGUID]
      ,[qtyOld]
      ,[idVS]
      ,[dateedit]
      ,[SKL_ID]
      ,[LOCNCODE]
      ,[SERLTNUM]
      ,[EXPIRATION]
      ,[NMBRPAL]
      ,[TYPEPAL]
      ,[PackType]
      ,[status]
      ,[WEIGHT]
      ,[STORNOGUID]
      ,[REZ_1]
      ,[REZ_2]
      ,[REZ_3]
      ,[REZ_4]
      ,[REZ_5]
      ,[WEIGHT_OLD])
     VALUES
           (
@CountEntries,
@SOPNUMBE,
@ITEMNMBR,
@ITEMTYPE,
@ITEMMJ,
@ITEMDESC,
@ORD,
@TIMEMODE,
@TIMEPREPSTART,
@TIMEPREPSTOP,
@TIMEPREP,
@TIMEUNIT,
@TIMESTART,
@TIMESTOP,
@TIMECORSTART,
@TIMECORSTOP,
@TIMECOR,
@TIMECRID,
@TIMECRIDTYPE,
--@id,
@loginid,
@machineid,
@operationid,
@dateeve,
@qty,
@qtyReal,
@QTYPACK,
@QTYPACKMJ,
@description,
@BarcodeP,
@UserID,
@TermID,
@ISOK,
@GUID,
@SOUBEHGUID,
@CORRGUID,
@qtyOld,
@idVS,
@dateedit,
@SKL_ID,
@LOCNCODE,
@SERLTNUM,
@EXPIRATION,
@NMBRPAL,
@TYPEPAL,
@PackType,
@status,
@WEIGHT,
@STORNOGUID,
@REZ_1,
@REZ_2,
@REZ_3,
@REZ_4,
@REZ_5,
@WEIGHT_OLD


		   )
		END

				--Zjisteni zda v prvnim insertu nastala chyba
		SELECT @ins1_error = @@ERROR

		--Test na chybu vlozeni
		IF (@ins1_error = 0)
		BEGIN
			--Chyba nenastala => potvrzeni transakce
			PRINT ''''ZAZNAM BYL USPESNE VLOZEN''''
			COMMIT TRAN
		END
		ELSE
		BEGIN
			--Nastala chyba zruseni transakce
			PRINT ''''NASTALA CHYBA PRI VKLADANI HODNOT DO DB''''
			ROLLBACK TRAN
		END
END


SET ANSI_NULLS ON'';

-- Installation step 447
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie pri zmene stavu ukolu uzivatele *****/
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 
-- Description:	historie a aktualizace
-- =============================================
CREATE TRIGGER [dbo].[fask_ukol_uziv_history] 
   ON  [dbo].[CZ_UKOL_UZIV] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare 
           @ID int
           ,@UkolID int
           ,@UserID int
           ,@State nvarchar(1)
           ,@DateChanged datetime
           ,@UserIDChanged int
           ,@Note nvarchar(200)
           ,@DateNotify datetime
           ,@DateFinished datetime

INSERT INTO [CZ_UKOL_UZIV_HIST]
           ([ID]
           ,[UkolID]
           ,[UserID]
           ,[State]
           ,[DateChanged]
           ,[UserIDChanged]
           ,[Note]
           ,[DateNotify]
           ,[DateFinished])
           ( select 
				ID 
				,UkolID
				,UserID
				,[State]
				,DateChanged
				,UserIDChanged
				,Note
				,DateNotify
				,DateFinished
				from inserted
           )
END'';

-- Installation step 448
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie vystupnich dat pro modul prodej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_DI_HISTORY] 
   ON  [dbo].[CZMST_DI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_di_history select * from deleted
END'';

-- Installation step 449
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 2.3.2023
-- Description:	Trigger medzi DI a FASK_Events
-- =============================================
CREATE TRIGGER [dbo].[fask_trg_CZMST_DI2FASK_Events] 
   ON  [dbo].[CZMST_DI] 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[FASK_Events]
           ([loginid]
           ,[machineid]
           ,[dateeve]
           ,[qty]
           ,[qtyReal]
           ,[description]
           ,[barcodeReaded]
           ,[barcodeSended]
           ,[zakazka]
           ,[popis]
           ,[faskGUID]
           ,[reportType]
           ,[isProcessed]
           ,[IDO]
           ,[scan1]
           ,[scan2]
           ,[scan3]
           ,[sensor]
           ,[material]
           ,[productionGuid]
           ,[VPH]
           ,[VPPol]
           ,[EAN_IS]
           ,[IS_ID]
           ,[NMBRPAL]
           ,[status]
           ,[QTYPACK]
           ,[PackType]
           ,[WEIGHT]
           ,[BarcodeT]
           ,[REZ_1]
           ,[REZ_2]
           ,[REZ_3]
           ,[REZ_4]
           ,[REZ_5])
			select 
			DI.USER_ID as loginid 
           ,'''''''' as machineid
           ,GETDATE() as dateeve
           ,DI.QTYSHPPD as qty
           ,0 as qtyReal
           ,'''''''' as description
           ,DI.VNDITNUM as barcodeReaded
           ,DI.VNDITNUM as barcodeSended
           ,'''''''' as zakazka
           ,'''''''' as popis
           ,DI.GUID as faskGUID
           ,'''''''' as reportType
           ,null as isProcessed
           ,'''''''' as IDO
           ,'''''''' as scan1
           ,'''''''' as scan2
           ,'''''''' as scan3
           ,'''''''' as sensor
           ,'''''''' as material
           ,null as productionGuid
           ,'''''''' as VPH
           ,'''''''' as VPPol
           ,DI.VNDITNUM as EAN_IS
           ,DI.ITEMNMBR as IS_ID
           ,'''''''' as NMBRPAL
           ,null as status
           ,DI.QTYPACK as QTYPACK
           ,'''''''' as PackType
           ,null as WEIGHT
           ,0 as BarcodeT
           ,'''''''' as REZ_1
           ,'''''''' as REZ_2
           ,'''''''' as REZ_3
           ,'''''''' as REZ_4
           ,'''''''' as REZ_5
		   from inserted as DI
END'';

-- Installation step 450
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie dat hlavicek pro modul prodej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_DIH_HISTORY] 
   ON  [dbo].[CZMST_DIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_dih_history select * from deleted
END'';

-- Installation step 451
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie hlavicek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Baleni_Hlavicka] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Baleni_Hlavicka_HISTORY select * from deleted
END'';

-- Installation step 452
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie polozek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Baleni_Polozky_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Baleni_Polozky] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Baleni_Polozky_HISTORY select * from deleted
END'';

-- Installation step 453
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie hlavicek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Hlavicka_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Hlavicka] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Hlavicka_HISTORY select * from deleted
END'';

-- Installation step 454
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie polozek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Polozky_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Polozky] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Polozky_HISTORY select * from deleted
END'';

-- Installation step 455
EXEC sys.sp_executesql N''-- =============================================
-- Author:          Ing. Rathouzsky Matous
-- Create date: 13.12. 2023
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:     preliti dat z I1 do SE
-- uprava MH - cele do cursoru - inserted nemusi mit pouze jeden zaznam
-- =============================================

CREATE TRIGGER [dbo].[Trigger_I1_do_SE]

   ON  [dbo].[CZMST_I1]

   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;

 

       --deklarace promennych

       declare @CountEntries [int]

       declare @CE_Orig [int]

       declare @ITEMNMBR [nvarchar](40)

       declare @CZ_CarKod [nvarchar](70)

       declare @ITEMDESC [nvarchar](100)

       declare @LOCNCODE [nvarchar](11)

       declare @skl_id [nvarchar](20)

       declare @QUANTITY [numeric](19, 5)

       declare @DMJ [nvarchar](200)

       declare @DATEDONE [datetime]

       declare @IntegerValue [smallint]

       declare @TIMESPRT [smallint]

       declare @CZ_SerNum_Track [tinyint]

       declare @CZ_SerNum_Find [tinyint]

       declare @DEX_ROW_ID [int]

       declare @TerminalID [tinyint]

       declare @O_TID [tinyint]

       declare @REZ_1 [nvarchar](50)

       declare @REZ_2 [nvarchar](50)

       declare @ITEMCODE [nvarchar](70)

       declare @CZ_REZ1_Track [tinyint]

       declare @CZ_REZ2_Track [tinyint]

       declare @CZ_Expirace_Track [tinyint]

 

    -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT

                    @CountEntries = i.CountEntries,

                    @CE_Orig = i.CE_Orig,

                    @ITEMNMBR = i.ITEMNMBR,

                    @CZ_CarKod = i.CZ_CarKod,

                    @ITEMDESC = i.ITEMDESC,

                    @LOCNCODE = i.LOCNCODE,

                    @skl_id = i.skl_id,

                    @QUANTITY = i.QUANTITY,

                    @DMJ = i.DMJ,

                    @DATEDONE = i.DATEDONE,

                    @IntegerValue = i.IntegerValue,

                    @TIMESPRT = i.TIMESPRT,

                    @CZ_SerNum_Track = i.CZ_SerNum_Track,

                    @CZ_SerNum_Find = i.CZ_SerNum_Find,

                    @TerminalID = i.TerminalID,

                    @O_TID = i.O_TID,

                    @REZ_1 = i.REZ_1,

                    @REZ_2 = i.REZ_2,

                    @ITEMCODE = i.ITEMCODE,

                    @CZ_REZ1_Track = i.CZ_REZ1_Track,

                    @CZ_REZ2_Track = i.CZ_REZ2_Track,

                    @CZ_Expirace_Track = i.CZ_Expirace_Track

                    FROM INSERTED i

                    WHERE i.DEX_ROW_ID = @DEX_ROW_ID;

 

                    EXECUTE dbo.CZMST_SE_insert

                    @CountEntries,

                    ''''Inventura'''',--@SOPNUMBE,

                    @ITEMNMBR,

                    ''''I'''',--@ITEMTYPE,

                    @ITEMDESC,

                    '''''''',--@VNDDOCNM,

                    @CZ_CarKod,--@VNDITNUM,

                    1,--@ORD,

                    @CZ_CarKod,

                    @skl_id,

                    @LOCNCODE,

                    @DMJ,--@MJ,

                    @QUANTITY,--@QTYSHPPD,

                    0,--@QTYPACK,

                    0,--@CZ_DatVyr_Track,

                    0,--@CZ_DatVyr_Delka,

                    @CZ_SerNum_Track,

                    0,--@CZ_SerNum_Delka,

                    0,--@CZ_SW_Track,

                    0,--@CZ_SW_Delka,

                    0,--@CZ_Doslo,

                    '''''''',--@Note,

                    '''''''',--@TYPEPAL,

                    0,--@QTYPAL,

                    3,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    @DEX_ROW_ID,

                    @CZ_REZ1_Track,

                    @CZ_REZ2_Track,

                    @ITEMCODE,

                    NULL,--@WEIGHT,

                    @DATEDONE,--@Realization_Start,

                    NULL,--@Realization_Stop,

                    @CZ_Expirace_Track;

            

           END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;








-- ========================================================================================================================================================
--
--
--												MaR 22.4.2024 Vytvorene trigry pro android
--
--
--																Konec
-- ========================================================================================================================================================'';

-- Installation step 456
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie dat davky prijmu *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PE_HISTORY] 
   ON  [dbo].[CZMST_PE] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pe_history select * from deleted
END'';

-- Installation step 457
EXEC sys.sp_executesql N''-- =============================================
-- Author:          Ing. Rathouzsky Matous
-- Create date: 5.3. 2024
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:     preliti dat z PE do SE
-- uprava MH - cele do cursoru - inserted nemusi mit pouze jeden zaznam
-- =============================================

CREATE TRIGGER [dbo].[Trigger_PE_do_SE]

   ON  [dbo].[CZMST_PE]

   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;


	declare @CountEntries [int] 
	declare @PONUMBER [nvarchar](30) 
	declare @ITEMNMBR [nvarchar](40) 
	declare @ITEMDESC [nvarchar](100) 
	declare @ORD [int] 
	declare @VNDDOCNM [nvarchar](21) 
	declare @VNDITNUM [nvarchar](60) 
	declare @CZ_CarKod [nvarchar](70) 
	declare @SKL_ID [nvarchar](20) 
	declare @LOCNCODE [nvarchar](11) 
	declare @MJ [nvarchar](10) 
	declare @QTYSHPPD [numeric](19, 5) 
	declare @QTYPACK [numeric](19, 5) 
	declare @CZ_DatVyr_Track [tinyint] 
	declare @CZ_DatVyr_Delka [smallint] 
	declare @CZ_SerNum_Track [tinyint] 
	declare @CZ_SerNum_Delka [smallint] 
	declare @CZ_SW_Track [tinyint] 
	declare @CZ_SW_Delka [smallint] 
	declare @CZ_Doslo [tinyint] 
	declare @DEX_ROW_ID [int] 
	declare @WEIGHT [numeric](19, 5) 
	declare @NMBRPAL [nvarchar](50) 
	declare @TYPEPAL [nvarchar](10) 
	declare @ITEMCODE [nvarchar](70) 
	declare @SERLTNUM [nvarchar](50) 
	declare @CZ_REZ1_Track [tinyint] 
	declare @CZ_REZ2_Track [tinyint] 
	declare @Realization_Start [datetime] 
	declare @Realization_Stop [datetime] 
	declare @CZ_Expirace_Track [tinyint] 

 

    -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

SELECT
    @CountEntries = i.CountEntries,
    @PONUMBER = i.PONUMBER,
    @ITEMNMBR = i.ITEMNMBR,
    @ITEMDESC = i.ITEMDESC,
    @ORD = i.ORD,
    @VNDDOCNM = i.VNDDOCNM,
    @VNDITNUM = i.VNDITNUM,
    @CZ_CarKod = i.CZ_CarKod,
    @SKL_ID = i.SKL_ID,
    @LOCNCODE = i.LOCNCODE,
    @MJ = i.MJ,
    @QTYSHPPD = i.QTYSHPPD,
    @QTYPACK = i.QTYPACK,
    @CZ_DatVyr_Track = i.CZ_DatVyr_Track,
    @CZ_DatVyr_Delka = i.CZ_DatVyr_Delka,
    @CZ_SerNum_Track = i.CZ_SerNum_Track,
    @CZ_SerNum_Delka = i.CZ_SerNum_Delka,
    @CZ_SW_Track = i.CZ_SW_Track,
    @CZ_SW_Delka = i.CZ_SW_Delka,
    @CZ_Doslo = i.CZ_Doslo,
    @DEX_ROW_ID = i.DEX_ROW_ID,
    @WEIGHT = i.WEIGHT,
    @NMBRPAL = i.NMBRPAL,
    @TYPEPAL = i.TYPEPAL,
    @ITEMCODE = i.ITEMCODE,
    @SERLTNUM = i.SERLTNUM,
    @CZ_REZ1_Track = i.CZ_REZ1_Track,
    @CZ_REZ2_Track = i.CZ_REZ2_Track,
    @Realization_Start = i.Realization_Start,
    @Realization_Stop = i.Realization_Stop,
    @CZ_Expirace_Track = i.CZ_Expirace_Track
FROM INSERTED i
                    WHERE i.DEX_ROW_ID = @DEX_ROW_ID;

 

                    EXECUTE dbo.CZMST_SE_insert

                    @CountEntries,

                    @PONUMBER,--@SOPNUMBE,

                    @ITEMNMBR,

                    ''''P'''',--@ITEMTYPE,

                    @ITEMDESC,

                    @VNDDOCNM,

                    @VNDITNUM,

                    @ORD,

                    @CZ_CarKod,

                    @skl_id,

                    @LOCNCODE,

                    @MJ,

                    @QTYSHPPD,

                    @QTYPACK,

                    @CZ_DatVyr_Track,

                    @CZ_DatVyr_Delka,

                    @CZ_SerNum_Track,

                    @CZ_SerNum_Delka,

                    @CZ_SW_Track,

                    @CZ_SW_Delka,

                    @CZ_Doslo,

                    '''''''',--@Note,

                    @TYPEPAL,

                    0,--@QTYPAL,

                    3,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    @DEX_ROW_ID,

                    @CZ_REZ1_Track,

                    @CZ_REZ2_Track,

                    @ITEMCODE,

                    NULL,--@WEIGHT,

                    @Realization_Start,

                    @Realization_Stop,

                    @CZ_Expirace_Track;

            

           END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


/****** Object:  Trigger [dbo].[Trigger_SI_delete_I4_PI_Production]    Script Date: 22.04.2024 11:02:29 ******/
SET ANSI_NULLS ON'';

-- Installation step 458
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie seriovych cisel prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PE_SN_HISTORY] 
   ON  [dbo].[CZMST_PE_SN] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pe_sn_history select * from deleted
END'';

-- Installation step 459
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie dat hlavicek pro modul prijem *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PEH_HISTORY] 
   ON  [dbo].[CZMST_PEH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_peh_history select * from deleted
END'';

-- Installation step 460
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PI_HISTORY] 
   ON  [dbo].[CZMST_PI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pi_history select * from deleted

END'';

-- Installation step 461
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie fotek prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PI_F_HISTORY] 
   ON  [dbo].[CZMST_PI_F] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pi_f_history select * from deleted
END'';

-- Installation step 462
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PIH_HISTORY] 
   ON  [dbo].[CZMST_PIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pih_history select * from deleted

END'';

-- Installation step 463
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie dat predlohy pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SE_HISTORY] 
   ON  [dbo].[CZMST_SE] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_se_history select * from deleted
END'';

-- Installation step 464
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie dat predlohy seriovych cisel pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SE_SN_HISTORY] 
   ON  [dbo].[CZMST_SE_SN] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_se_sn_history select * from deleted
END'';

-- Installation step 465
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie hlavicek pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SEH_HISTORY] 
   ON  [dbo].[CZMST_SEH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_seh_history select * from deleted
END'';

-- Installation step 466
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie vystupnich dat pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SI_HISTORY] 
   ON  [dbo].[CZMST_SI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_si_history select * from deleted
END'';

-- Installation step 467
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 19.2. 2024
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	vymazani zaznamu pri delete v SI propis I4 a PI a Production
-- =============================================
create TRIGGER [dbo].[Trigger_SI_delete_I4_PI_Production] 
   ON  [dbo].[CZMST_SI] 
   FOR DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	--deklarace promennych
DECLARE @CountEntries [int]
DECLARE @SOPNUMBE [nvarchar](30)
DECLARE @ITEMNMBR [nvarchar](40)
DECLARE @ORD [int]
DECLARE @VNDDOCNM [nvarchar](21)
DECLARE @VNDITNUM [nvarchar](60)
DECLARE @CZ_CarKod [nvarchar](70)
DECLARE @SKL_ID [nvarchar](20)
DECLARE @LOCNCODE [nvarchar](11)
DECLARE @MJ [nvarchar](10)
DECLARE @QTYSHPPD [numeric](19, 5)
DECLARE @QTYPACK [numeric](19, 5)
DECLARE @QTYSHPPDMJ [numeric](19, 5)
DECLARE @SERLTNUM [nvarchar](50)
DECLARE @KOD_SW [nvarchar](11)
DECLARE @DAT_VYROBY [nvarchar](11)
DECLARE @REZ_1 [nvarchar](50)
DECLARE @REZ_2 [nvarchar](50)
DECLARE @ODBER_ID [nvarchar](12)
DECLARE @DATEDONE [nvarchar](8)
DECLARE @TIMEDONE [nvarchar](6)
DECLARE @USER_ID [int]
DECLARE @TYPEPAL [nvarchar](10)
DECLARE @NMBRPAL [nvarchar](50)
DECLARE @PRINTED [tinyint]
DECLARE @DEX_ROW_ID [int]
DECLARE @GUID [uniqueidentifier]
DECLARE @INPUT_MODE [tinyint]
DECLARE @ID_TERMINAL [int]
DECLARE @ITEMCODE [nvarchar](70)
DECLARE @WEIGHT [numeric](19, 5)
DECLARE @Expirace [datetime]


  -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM deleted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT
@CountEntries = i.CountEntries,
@SOPNUMBE = i.SOPNUMBE,
@ITEMNMBR = i.ITEMNMBR,
@ORD = i.ORD,
@VNDDOCNM = i.VNDDOCNM,
@VNDITNUM = i.VNDITNUM,
@CZ_CarKod = i.CZ_CarKod,
@SKL_ID = i.SKL_ID,
@LOCNCODE = i.LOCNCODE,
@MJ = i.MJ,
@QTYSHPPD = i.QTYSHPPD,
@QTYPACK = i.QTYPACK,
@QTYSHPPDMJ = i.QTYSHPPDMJ,
@SERLTNUM = i.SERLTNUM,
@KOD_SW = i.KOD_SW,
@DAT_VYROBY = i.DAT_VYROBY,
@REZ_1 = i.REZ_1,
@REZ_2 = i.REZ_2,
@ODBER_ID = i.ODBER_ID,
@DATEDONE = i.DATEDONE,
@TIMEDONE = i.TIMEDONE,
@USER_ID = i.USER_ID,
@TYPEPAL = i.TYPEPAL,
@NMBRPAL = i.NMBRPAL,
@PRINTED = i.PRINTED,
@DEX_ROW_ID = i.DEX_ROW_ID,
@GUID = i.GUID,
@INPUT_MODE = i.INPUT_MODE,
@ID_TERMINAL = i.ID_TERMINAL,
@ITEMCODE = i.ITEMCODE,
@WEIGHT = i.WEIGHT,
@Expirace = i.Expirace

	from deleted i
	  WHERE i.DEX_ROW_ID = @DEX_ROW_ID;


	DELETE FROM [dbo].[CZMST_I4]
      WHERE GUID = @GUID

	  DELETE FROM [dbo].[CZMST_PI]
      WHERE GUID = @GUID

	  	  DELETE FROM [dbo].[Production]
      WHERE GUID = @GUID

	
     END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


SET ANSI_NULLS ON'';

-- Installation step 468
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Rathouzsky Matous
-- Create date: 4.4. 2024
-- Kontroloval: Ing. Skřivánek Jan
-- Date: 09.06.2025
-- Description:	preliti dat z SI do PI a do I4 a do Production
-- =============================================
CREATE TRIGGER [dbo].[Trigger_SI_do_I4_PI_Production] 
   ON  [dbo].[CZMST_SI] 
   FOR INSERT

AS

BEGIN

       SET NOCOUNT ON;

	--deklarace promennych
DECLARE @CountEntries [int]
DECLARE @SOPNUMBE [nvarchar](30)
DECLARE @ITEMNMBR [nvarchar](40)
DECLARE @ORD [int]
DECLARE @VNDDOCNM [nvarchar](21)
DECLARE @VNDITNUM [nvarchar](60)
DECLARE @CZ_CarKod [nvarchar](70)
DECLARE @SKL_ID [nvarchar](20)
DECLARE @LOCNCODE [nvarchar](11)
DECLARE @MJ [nvarchar](10)
DECLARE @QTYSHPPD [numeric](19, 5)
DECLARE @QTYPACK [numeric](19, 5)
DECLARE @QTYSHPPDMJ [numeric](19, 5)
DECLARE @SERLTNUM [nvarchar](50)
DECLARE @KOD_SW [nvarchar](11)
DECLARE @DAT_VYROBY [nvarchar](11)
DECLARE @REZ_1 [nvarchar](50)
DECLARE @REZ_2 [nvarchar](50)
DECLARE @ODBER_ID [nvarchar](12)
DECLARE @DATEDONE [nvarchar](8)
DECLARE @TIMEDONE [nvarchar](6)
DECLARE @USER_ID [int]
DECLARE @TYPEPAL [nvarchar](10)
DECLARE @NMBRPAL [nvarchar](50)
DECLARE @PRINTED [tinyint]
DECLARE @DEX_ROW_ID [int]
DECLARE @GUID [uniqueidentifier]
DECLARE @INPUT_MODE [tinyint]
DECLARE @ID_TERMINAL [int]
DECLARE @ITEMCODE [nvarchar](70)
DECLARE @WEIGHT [numeric](19, 5)
DECLARE @Expirace [datetime]


  -- Insert statements for trigger here

       BEGIN TRAN;

     

       BEGIN        

           DECLARE curPol CURSOR LOCAL STATIC FOR

           SELECT DEX_ROW_ID

             FROM inserted;

 

           OPEN curPol;

 

           WHILE ( 1 = 1 ) BEGIN

           FETCH NEXT FROM curPol INTO @DEX_ROW_ID;

 

           IF ( @@FETCH_STATUS <> 0 )

                 BREAK;

 

                    SELECT
@CountEntries = i.CountEntries,
@SOPNUMBE = i.SOPNUMBE,
@ITEMNMBR = i.ITEMNMBR,
@ORD = i.ORD,
@VNDDOCNM = i.VNDDOCNM,
@VNDITNUM = i.VNDITNUM,
@CZ_CarKod = i.CZ_CarKod,
@SKL_ID = i.SKL_ID,
@LOCNCODE = i.LOCNCODE,
@MJ = i.MJ,
@QTYSHPPD = i.QTYSHPPD,
@QTYPACK = i.QTYPACK,
@QTYSHPPDMJ = i.QTYSHPPDMJ,
@SERLTNUM = i.SERLTNUM,
@KOD_SW = i.KOD_SW,
@DAT_VYROBY = i.DAT_VYROBY,
@REZ_1 = i.REZ_1,
@REZ_2 = i.REZ_2,
@ODBER_ID = i.ODBER_ID,
@DATEDONE = i.DATEDONE,
@TIMEDONE = i.TIMEDONE,
@USER_ID = i.USER_ID,
@TYPEPAL = i.TYPEPAL,
@NMBRPAL = i.NMBRPAL,
@PRINTED = i.PRINTED,
@DEX_ROW_ID = i.DEX_ROW_ID,
@GUID = i.GUID,
@INPUT_MODE = i.INPUT_MODE,
@ID_TERMINAL = i.ID_TERMINAL,
@ITEMCODE = i.ITEMCODE,
@WEIGHT = i.WEIGHT,
@Expirace = i.Expirace

	from INSERTED i
	  WHERE i.DEX_ROW_ID = @DEX_ROW_ID;


	  --select @CountEntries @SOPNUMBE @ORD

	 DECLARE @ItemTypeResult NVARCHAR(255);  -- Upravte délku podle skutečných potřeb


SELECT TOP 1
    @ItemTypeResult = [ITEMTYPE]
FROM
    [CZMST_SE]
WHERE
    [CountEntries] = @CountEntries
    AND [SOPNUMBE] = @SOPNUMBE
    AND [ORD] = @ORD;

-- Použití hodnoty ItemTypeResult podle potřeby
-- ...

-- Příklad výpisu hodnoty ItemTypeResult
PRINT @ItemTypeResult;

-- Podmínka IF
IF @ItemTypeResult = ''''P''''
BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno ''''P''''
    PRINT ''''ItemTypeResult je P. Provedeno další akce pro P.'''';

	EXECUTE dbo.CZMST_PI_insert 

@CountEntries,
@SOPNUMBE,--@PONUMBER,
@ORD,
@ITEMNMBR,
@VNDDOCNM,
@VNDITNUM,
@SKL_ID,
@LOCNCODE,
@MJ,
@QTYSHPPD,
@QTYSHPPDMJ,
@QTYPACK,
@SERLTNUM,
@KOD_SW,
@DAT_VYROBY,
@DATEDONE,
@TIMEDONE,
@CZ_CarKod,
@REZ_1,
@REZ_2,
@USER_ID,
@DEX_ROW_ID,
@GUID,
@INPUT_MODE,
@ID_TERMINAL,
@WEIGHT,
@NMBRPAL,
@TYPEPAL,
@ITEMCODE,
@Expirace,
null;   --@AttributeToSN;

END

ELSE IF @ItemTypeResult = ''''I''''

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno ''''I''''
	-- JaS 20250609 zkontrolovat na reálných datech
    PRINT ''''ItemTypeResult je I. Provedeno další akce pro I.'''';
	EXECUTE dbo.CZMST_I4_insert 
   @CountEntries,
	NULL,--@CE_Orig,
	@ITEMNMBR,
	@CZ_CarKod,
	@LOCNCODE,
	@SKL_ID,
	@VNDITNUM,
	@MJ,
	@QTYSHPPD,--@QUANTITY,
	@QTYSHPPD,--@QUANTITYMJ,
	@QTYPACK,
	@SERLTNUM,--@SERLNMBR,
	@DATEDONE,
	@TIMEDONE,
	NULL,--@USERID,
	@DEX_ROW_ID,
	@GUID,
	0,--@O_Checked,
	@INPUT_MODE,
	@ID_TERMINAL,
	@ITEMCODE,
	@REZ_1,
	@REZ_2,
	@WEIGHT,
	@Expirace;



END

ELSE IF @ItemTypeResult = ''''V''''

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult je rovno ''''V''''
    -- JaS 20250609 zkontrolovat na reálných datech
    PRINT ''''ItemTypeResult je V. Provedeno další akce pro V.'''';

declare @aktualniCas datetime

set @aktualniCas = GETDATE()

EXECUTE dbo.Production_insert 
	@CountEntries,
    @SOPNUMBE,
    @ITEMNMBR,
    '''''''',--66, -- @ITEMTYPE,
    @MJ,--66, -- @ITEMMJ,
    '''''''',--66, -- @ITEMDESC,
    @ORD,
    0, -- @TIMEMODE,
    null, -- @TIMEPREPSTART,
    null, -- @TIMEPREPSTOP,
    null, -- @TIMEPREP,
    null, -- @TIMEUNIT,
    null, -- @TIMESTART,
    null, -- @TIMESTOP,
    null, -- @TIMECORSTART,
    null, -- @TIMECORSTOP,
   null, -- 66, -- @TIMECOR,
   null, -- 66, -- @TIMECRID,
   null, -- 66, -- @TIMECRIDTYPE,
    66, -- @id,
    @USER_ID, -- @loginid,
    @ID_TERMINAL, -- @machineid,
    null, -- @operationid,
    @aktualniCas, -- @dateeve,
    @QTYSHPPD, -- @qty,
    @QTYSHPPD ,--66, -- @qtyReal,
    @QTYPACK,
    0, -- @QTYPACKMJ,
    '''''''', -- @description,
    @CZ_CarKod , -- @BarcodeP,
    @USER_ID,--66, -- @UserID,
   @ID_TERMINAL ,-- 66, -- @TermID,
    null, -- @ISOK,
    @GUID,
    null, -- @SOUBEHGUID,
    null, -- @CORRGUID,
    0, -- @qtyOld,
    @QTYSHPPD, -- @idVS,
    null, -- @dateedit,
    @SKL_ID,
    @LOCNCODE,
    @SERLTNUM,
   @Expirace ,-- 66, -- @EXPIRATION,
    @NMBRPAL,
    @TYPEPAL,
    '''''''', -- @PackType,
    '''''''', -- @status,
    @WEIGHT,
    null, -- @STORNOGUID,
    @REZ_1,
    @REZ_2,
    null, --  '''''''', -- @REZ_3,
    null, --  '''''''', -- @REZ_4,
    null, --  '''''''', -- @REZ_5,
    null; -- 0 -- @WEIGHT_OLD;


END

ELSE

BEGIN
    -- Akce, která se provede, pokud ItemTypeResult není ani ''''P'''', ani ''''I''''
    PRINT ''''ItemTypeResult není ani P, ani I. Provedeno jiné akce.'''';

END;




     END;

 

           CLOSE curPol;

           DEALLOCATE curPol;

 

    END;

 

       COMMIT TRAN;

 

END;


SET ANSI_NULLS ON'';

-- Installation step 469
EXEC sys.sp_executesql N''/***** Trigger pro ukladani historie vystupnich dat pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SIH_HISTORY] 
   ON  [dbo].[CZMST_SIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_sih_history select * from deleted
END'';

-- Installation step 470
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Ing. Matouš Rathouzský
-- Create date: 3.4. 2024
-- Description:	triger ktery vytvori zaznam v SE se strukturou VPP, na zaklade zaznamu VPH se zmenou prvku ACTIVE na 1
--                na zaklade zaznamu VPH se zmenou prvku ACTIVE na 0 vymaze vsechny zaznamy z SE tykajicich se Countentries z VPH
-- =============================================
CREATE TRIGGER [dbo].[trg_CZPRO_VPH_ActiveChange_new]
ON [dbo].[CZPRO_VPH]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Active) -- Zkontrolujeme, zda byl aktualizován sloupec Active
    BEGIN
        -- Deklarace proměnných pro VPH
        DECLARE @VPH_CountEntries int;
        DECLARE @VPH_SOPNUMBE nvarchar(30);
        DECLARE @VPH_SOPTYPE nvarchar(11);
        DECLARE @VPH_SOPDESC nvarchar(100);
        DECLARE @VPH_VNDDOCNMH nvarchar(21);
        DECLARE @VPH_BarcodeH nvarchar(31);
        DECLARE @VPH_LOCNCODE nvarchar(11);
        DECLARE @VPH_DateProd smallint;
        DECLARE @VPH_Rez1 nvarchar(50);
        DECLARE @VPH_Rez2 nvarchar(50);
        DECLARE @VPH_TermID tinyint;
        DECLARE @VPH_LSTMod datetime;
        DECLARE @VPH_DEX_ROW_ID int;
        DECLARE @VPH_Active tinyint;
        DECLARE @VPH_USERID int;

        -- Deklarace proměnných pro VPP
        DECLARE @VPP_CountEntries int;
        DECLARE @VPP_SOPNUMBE nvarchar(30);
        DECLARE @VPP_ITEMNMBR nvarchar(40);
        DECLARE @VPP_ITEMTYPE nvarchar(11);
        DECLARE @VPP_ITEMDESC nvarchar(100);
        DECLARE @VPP_ITEMMJ nvarchar(5);
        DECLARE @VPP_VNDDOCNMP nvarchar(21);
        DECLARE @VPP_VNDITNUM nvarchar(60);
        DECLARE @VPP_ORD int;
        DECLARE @VPP_BarcodeP nvarchar(31);
        DECLARE @VPP_LOCNCODE nvarchar(11);
        DECLARE @VPP_QTYSHPPD numeric(19, 5);
        DECLARE @VPP_QTYDOKON numeric(19, 5);
        DECLARE @VPP_QTYPACK numeric(19, 5);
        DECLARE @VPP_QTYPACKMJ nvarchar(5);
        DECLARE @VPP_TIMEMODE int;
        DECLARE @VPP_TIMEPREP real;
        DECLARE @VPP_TIMEUNIT real;
        DECLARE @VPP_DtProdT tinyint;
        DECLARE @VPP_DtProdL smallint;
        DECLARE @VPP_SerNumT tinyint;
        DECLARE @VPP_SerNumL smallint;
        DECLARE @VPP_VerT tinyint;
        DECLARE @VPP_VerL smallint;
        DECLARE @VPP_TermID tinyint;
        DECLARE @VPP_LSTMod datetime;
        DECLARE @VPP_Realization_Start datetime;
        DECLARE @VPP_Realization_Stop datetime;
        DECLARE @VPP_BarcodeT tinyint;
        DECLARE @VPP_CZ_REZ1_Track tinyint;
        DECLARE @VPP_CZ_REZ2_Track tinyint;
        DECLARE @VPP_CZ_REZ3_Track tinyint;
        DECLARE @VPP_CZ_REZ4_Track tinyint;
        DECLARE @VPP_CZ_REZ5_Track tinyint;
        DECLARE @VPP_WEIGHT_TARA numeric(19, 5);
        DECLARE @VPP_WEIGHT_NETTO numeric(19, 5);
        DECLARE @VPP_WEIGHT_TOL_PLUS numeric(19, 5);
        DECLARE @VPP_WEIGHT_TOL_MINUS numeric(19, 5);
		
        DECLARE @VPP_DEX_ROW_ID int;
        -- Získání aktualizovaných dat
        SELECT 
            @VPH_CountEntries = inserted.CountEntries,
            @VPH_SOPNUMBE = inserted.SOPNUMBE,
            @VPH_SOPTYPE = inserted.SOPTYPE,
            @VPH_SOPDESC = inserted.SOPDESC,
            @VPH_VNDDOCNMH = inserted.VNDDOCNMH,
            @VPH_BarcodeH = inserted.BarcodeH,
            @VPH_LOCNCODE = inserted.LOCNCODE,
            @VPH_DateProd = inserted.DateProd,
            @VPH_Rez1 = inserted.Rez1,
            @VPH_Rez2 = inserted.Rez2,
            @VPH_TermID = inserted.TermID,
            @VPH_LSTMod = inserted.LSTMod,
            @VPH_DEX_ROW_ID = inserted.DEX_ROW_ID,
            @VPH_Active = inserted.Active,
            @VPH_USERID = inserted.USERID
        FROM inserted;

        -- Pokud byla hodnota sloupce Active změněna z 1 na 0
        --IF EXISTS (SELECT 1 FROM deleted WHERE Active = 1)
		IF EXISTS (SELECT 1 FROM deleted WHERE Active <> 0)
        BEGIN
            -- Pokud se hodnota změnila z 1 na 0, provede smazání záznamu z tabulky CZMST_SE
            IF EXISTS (SELECT 1 FROM inserted WHERE Active = 0)
			--IF NOT EXISTS (SELECT 1 FROM inserted WHERE Active = 1)
            BEGIN
                DELETE FROM [dbo].CZMST_SE 
                WHERE CountEntries = @VPH_CountEntries;
            END;
        END;

        -- Pokud byla hodnota sloupce Active změněna z 0 na 1
        --IF EXISTS (SELECT 1 FROM deleted WHERE Active = 0)
		IF EXISTS (SELECT 1 FROM deleted WHERE Active <> 1)
        BEGIN
            -- Pokud se hodnota změnila na 1, vytvoř nový záznam v tabulce CZMST_SE
            IF EXISTS (SELECT 1 FROM inserted WHERE Active = 1)
            BEGIN
                DECLARE dataCursor CURSOR FOR
                SELECT 
                    [CountEntries],
                    [SOPNUMBE],
                    [ITEMNMBR],
                    [ITEMTYPE],
                    [ITEMDESC],
                    [ITEMMJ],
                    [VNDDOCNMP],
                    [VNDITNUM],
                    [ORD],
                    [BarcodeP],
                    [LOCNCODE],
                    [QTYSHPPD],
                    [QTYDOKON],
                    [QTYPACK],
                    [QTYPACKMJ],
                    [TIMEMODE],
                    [TIMEPREP],
                    [TIMEUNIT],
                    [DtProdT],
                    [DtProdL],
                    [SerNumT],
                    [SerNumL],
                    [VerT],
                    [VerL],
                    [TermID],
                    [LSTMod],
                    [DEX_ROW_ID],
                    [Realization_Start],
                    [Realization_Stop],
                    [BarcodeT],
                    [CZ_REZ1_Track],
                    [CZ_REZ2_Track],
                    [CZ_REZ3_Track],
                    [CZ_REZ4_Track],
                    [CZ_REZ5_Track],
                    [WEIGHT_TARA],
                    [WEIGHT_NETTO],
                    [WEIGHT_TOL_PLUS],
                    [WEIGHT_TOL_MINUS]
                FROM dbo.CZPRO_VPP
                WHERE CountEntries = @VPH_CountEntries
                  AND SOPNUMBE = @VPH_SOPNUMBE
                  --AND LOCNCODE = @VPH_LOCNCODE
                  --AND TermID = @VPH_TermID
                  --AND LSTMod = @VPH_LSTMod;

                OPEN dataCursor;
                FETCH NEXT FROM dataCursor INTO @VPP_CountEntries, @VPP_SOPNUMBE, @VPP_ITEMNMBR, @VPP_ITEMTYPE, @VPP_ITEMDESC, @VPP_ITEMMJ, @VPP_VNDDOCNMP, @VPP_VNDITNUM, @VPP_ORD, @VPP_BarcodeP, @VPP_LOCNCODE, @VPP_QTYSHPPD, @VPP_QTYDOKON, @VPP_QTYPACK, @VPP_QTYPACKMJ, @VPP_TIMEMODE, @VPP_TIMEPREP, @VPP_TIMEUNIT, @VPP_DtProdT, @VPP_DtProdL, @VPP_SerNumT, @VPP_SerNumL, @VPP_VerT, @VPP_VerL, @VPP_TermID, @VPP_LSTMod, @VPP_DEX_ROW_ID, @VPP_Realization_Start, @VPP_Realization_Stop, @VPP_BarcodeT, @VPP_CZ_REZ1_Track, @VPP_CZ_REZ2_Track, @VPP_CZ_REZ3_Track, @VPP_CZ_REZ4_Track, @VPP_CZ_REZ5_Track, @VPP_WEIGHT_TARA, @VPP_WEIGHT_NETTO, @VPP_WEIGHT_TOL_PLUS, @VPP_WEIGHT_TOL_MINUS;

                -- Cyklus WHILE pro zápis záznamů do tabulky CZMST_SE
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- Zde doplňte kód pro vložení záznamu do tabulky CZMST_SE na základě hodnot z tabulky VPP
					-- JaS 20250609 zkontrolovat na reálných datech
					----------------------------------------SE zapis start--------------------------------------------------------------------

EXECUTE dbo.CZMST_SE_insert

                    @VPP_CountEntries,

                    @VPP_SOPNUMBE,--''''Vyroba'''',--@SOPNUMBE,

                    @VPP_ITEMNMBR,

                    ''''V'''',--@ITEMTYPE,

                    @VPP_ITEMDESC,

                    @VPP_VNDDOCNMP,

                    @VPP_VNDITNUM,--66,--@CZ_CarKod,--@VNDITNUM,

                    @VPP_ORD,--66,--@CountEntries,--@ORD,

                    @VPP_BarcodeP,--66,--@CZ_CarKod,

                   '''''''',-- null,--66,--@skl_id,

                    @VPP_LOCNCODE,

                    @VPP_ITEMMJ,--66,--@DMJ,--@MJ,

                    @VPP_QTYSHPPD,--66,--@QUANTITY,--@QTYSHPPD,

                    @VPP_QTYPACK,

                    0,--@CZ_DatVyr_Track,

                    0,--@CZ_DatVyr_Delka,

                    @VPP_SerNumT,-- 66,--@CZ_SerNum_Track,

                    @VPP_SerNumL,-- 0,--@CZ_SerNum_Delka,

                    @VPP_VerT ,--0,--@CZ_SW_Track,

                    @VPP_VerL ,--0,--@CZ_SW_Delka,

                   @VPP_TermID ,-- 0,--@CZ_Doslo,

                    @VPH_SOPDESC ,--'''''''',--@Note,

                    '''''''',--@TYPEPAL,

                   @VPP_QTYPACK ,-- 0,--@QTYPAL,

                    0,--@PRIORITY,

                    0,--@PRINTED,

                    0,--@USERID,

                    66,--@DEX_ROW_ID,

                    @VPP_CZ_REZ1_Track,--66,--@CZ_REZ1_Track,

                    @VPP_CZ_REZ2_Track,--66,--@CZ_REZ2_Track,

                   @VPP_BarcodeP,-- 66,--@ITEMCODE,

                  @VPP_WEIGHT_NETTO ,--  NULL,--@WEIGHT,

                 null,--  @VPP_Realization_Start,

                   null,-- @VPP_Realization_Stop,

                   0;-- 66;--@CZ_Expirace_Track;

----------------------------------------SE zapis konec--------------------------------------------------------------------


                    FETCH NEXT FROM dataCursor INTO @VPP_CountEntries, @VPP_SOPNUMBE, @VPP_ITEMNMBR, @VPP_ITEMTYPE, @VPP_ITEMDESC, @VPP_ITEMMJ, @VPP_VNDDOCNMP, @VPP_VNDITNUM, @VPP_ORD, @VPP_BarcodeP, @VPP_LOCNCODE, @VPP_QTYSHPPD, @VPP_QTYDOKON, @VPP_QTYPACK, @VPP_QTYPACKMJ, @VPP_TIMEMODE, @VPP_TIMEPREP, @VPP_TIMEUNIT, @VPP_DtProdT, @VPP_DtProdL, @VPP_SerNumT, @VPP_SerNumL, @VPP_VerT, @VPP_VerL, @VPP_TermID, @VPP_LSTMod, @VPP_DEX_ROW_ID, @VPP_Realization_Start, @VPP_Realization_Stop, @VPP_BarcodeT, @VPP_CZ_REZ1_Track, @VPP_CZ_REZ2_Track, @VPP_CZ_REZ3_Track, @VPP_CZ_REZ4_Track, @VPP_CZ_REZ5_Track, @VPP_WEIGHT_TARA, @VPP_WEIGHT_NETTO, @VPP_WEIGHT_TOL_PLUS, @VPP_WEIGHT_TOL_MINUS;
                END;

                CLOSE dataCursor;
                DEALLOCATE dataCursor;
            END;
        END;
    END;
END;



SET ANSI_NULLS ON'';

-- Installation step 471
EXEC sys.sp_executesql N''-- =============================================
-- Author:		Jiri skrivanek a Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER [dbo].[InsertEvents] 
   ON  [dbo].[FASK_Events] 
   FOR INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	
	--Deklarace promennych z inserted	
	declare @loginid [nvarchar](20)
	declare @machineid [nvarchar](20) 
	declare @dateeve [datetime] 
	declare @dateevepro [varchar] (21)
	declare @qty [numeric](19, 5)
	declare @qtyReal [numeric](19, 5)
	--declare @description [ntext] 
	declare @barcodeReaded [nchar](50)
	declare @barcodeSended [nchar](50)
	declare @zakazka [nchar](20)
	declare @material [nvarchar] (255)
	declare @popis [nchar](10)
	declare @faskGUID [uniqueidentifier]
	declare @reportType [nchar](1)
	declare @isProcessed [datetime]
	declare @IDO [nchar](10)
	declare @scan1 [nvarchar](255)
	declare @scan2 [nvarchar](255)
	declare @scan3 [nvarchar](255)
	declare @sensor [nvarchar](50)	
	declare @status int

		-- TaD nove parametry
	declare @id int
	declare @VPH [nvarchar](30)
	declare @IS_ID [nvarchar](40)

	declare @qtypack [numeric](19, 5)
	declare @PackType [nvarchar](50)

	declare @NMBRPAL [nvarchar](50)
    declare @WEIGHT [numeric](19,5)

declare @rez1 [nvarchar](100)
declare @rez2 [nvarchar](100)
declare @rez3 [nvarchar](100)
declare @rez4 [nvarchar](100)
declare @rez5 [nvarchar](100)
	
    -- Insert statements for trigger here
    begin tran
        
	select 
		@loginid = i.loginid, 
		@machineid = i.machineid,
		@zakazka = i.zakazka,
		@qty= i.qty,
		@dateeve = i.dateeve,
		@qtyReal = i.qtyReal,
		@barcodeReaded  = i.barcodeSended,
		@popis = i.popis,
		@material = i.material,
		@scan3 = i.scan3,
		@id = i.id,
		@VPH = i.VPH,
		@IS_ID = i.IS_ID,
		@status = i.status,
		@qtypack = i.QTYPACK,
		@PackType = i.PackType,
		@NMBRPAL = i.NMBRPAL,
		@WEIGHT = i.WEIGHT,
                @rez1 = i.REZ_1,
                @rez2 = i.REZ_2,
                @rez3 = i.REZ_3,
                @rez4 = i.REZ_4,
                @rez5 = i.REZ_5
	from INSERTED i;        
    
	declare @pom [char](2)

	IF ( @status = 0 OR  @status = 600 ) 
		BEGIN

			IF ( @popis = ''''True'''' ) 
				BEGIN
					set @pom = 0
				END
				ELSE 
				BEGIN
					set @pom = 1
				END
	
				set @dateevepro = CONVERT(varchar, @dateeve, 120)
				set @dateevepro = SUBSTRING(@dateevepro, 1, 4) + 
								  SUBSTRING(@dateevepro, 6, 2) + 
								  SUBSTRING(@dateevepro, 9, 2) +
								  SUBSTRING(@dateevepro, 12, 2) + 
								  SUBSTRING(@dateevepro, 15, 2) + 
								  SUBSTRING(@dateevepro, 18, 2)

			--	EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyReal, @dateevepro ,@loginid,'''''''','''''''','''''''','''''''', @material
			--	8.7.2021 JiS : do hodnoty rez1 se uklada prubezna hodnota palet za smenu
				declare @rezX char(10)
				select @rezX = substring(@scan3, 1, 5)
				
				declare @qtyOUT [numeric](19, 5)

				IF ( 1 = 1)
					BEGIN
						IF (@PackType = ''''UP'''')
							BEGIN

								IF (@status = 0)
									BEGIN
										SET @qtyOUT = @qtypack
									END
								ELSE IF (@status = 600)
									BEGIN
										SET @qtyOUT = -@qtypack
									END
								
						--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'''''''','''''''', @rezX, '''''''', @material
						EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve, @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
							END
						IF (@PackType = ''''NP'''')
							BEGIN
								SET @qtyOUT = 0
								--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'''''''','''''''', @rezX, '''''''', @material
								EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve , @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
							END
					END
				ELSE
					BEGIN
						SET @qtyOUT = @qtyReal
						--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'''''''','''''''', @rezX, '''''''', @material
						EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve , @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
					END

    	 END  

    commit tran

END'';

-- Installation step 472
EXEC sys.sp_executesql N''/**************************************************************************************/
/****** Object:  Table [dbo].[fask_trg_machinestatesethistory]    ******/

-- =============================================
-- Author:		Ing. Jiří Skřivánek
-- Create date: 20.5.2014
-- Description:	Trigger pro ukládání historie stavu strojů
-- =============================================
CREATE TRIGGER [dbo].[fask_trg_machinestatesethistory] 
   ON  [dbo].[MachineStateSet] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--inserted
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		INSERT INTO MachineStateSetHistory 
			SELECT * FROM inserted
	END        

END

-- JaS 20250714 - pokud tabulka již existovala, zkolabovalo
-- CREATE TABLE [dbo].[MachinesDefinitionMeasurement](
--	[IP] [nvarchar](20) NOT NULL,
--	[IP_M] [nvarchar](20) NOT NULL,
--	[Description_M] [nvarchar](100) NULL,
--	[MType_M] [nvarchar](50) NOT NULL,
--	[PORT_M] [int] NULL,
--	[ID_group_M] [int] NULL
-- ) ON [PRIMARY]'';

-- Installation step 473
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZ_UKOL_UZIV] ENABLE TRIGGER [fask_ukol_uziv_history]'';

-- Installation step 474
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DI] ENABLE TRIGGER [fask_trg_CZMST_DI_HISTORY]'';

-- Installation step 475
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DI] ENABLE TRIGGER [fask_trg_CZMST_DI2FASK_Events]'';

-- Installation step 476
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_DIH] ENABLE TRIGGER [fask_trg_CZMST_DIH_HISTORY]'';

-- Installation step 477
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka] ENABLE TRIGGER [fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY]'';

-- Installation step 478
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Polozky] ENABLE TRIGGER [fask_trg_CZMST_Expedice_Baleni_Polozky_HISTORY]'';

-- Installation step 479
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka] ENABLE TRIGGER [fask_trg_CZMST_Expedice_Hlavicka_HISTORY]'';

-- Installation step 480
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_Expedice_Polozky] ENABLE TRIGGER [fask_trg_CZMST_Expedice_Polozky_HISTORY]'';

-- Installation step 481
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_I1] ENABLE TRIGGER [Trigger_I1_do_SE]'';

-- Installation step 482
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ENABLE TRIGGER [fask_trg_CZMST_PE_HISTORY]'';

-- Installation step 483
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE] ENABLE TRIGGER [Trigger_PE_do_SE]'';

-- Installation step 484
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PE_SN] ENABLE TRIGGER [fask_trg_CZMST_PE_SN_HISTORY]'';

-- Installation step 485
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PEH] ENABLE TRIGGER [fask_trg_CZMST_PEH_HISTORY]'';

-- Installation step 486
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PI] ENABLE TRIGGER [fask_trg_CZMST_PI_HISTORY]'';

-- Installation step 487
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PI_F] ENABLE TRIGGER [fask_trg_CZMST_PI_F_HISTORY]'';

-- Installation step 488
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_PIH] ENABLE TRIGGER [fask_trg_CZMST_PIH_HISTORY]'';

-- Installation step 489
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE] ENABLE TRIGGER [fask_trg_CZMST_SE_HISTORY]'';

-- Installation step 490
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SE_SN] ENABLE TRIGGER [fask_trg_CZMST_SE_SN_HISTORY]'';

-- Installation step 491
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SEH] ENABLE TRIGGER [fask_trg_CZMST_SEH_HISTORY]'';

-- Installation step 492
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI] ENABLE TRIGGER [fask_trg_CZMST_SI_HISTORY]'';

-- Installation step 493
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI] ENABLE TRIGGER [Trigger_SI_delete_I4_PI_Production]'';

-- Installation step 494
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SI] ENABLE TRIGGER [Trigger_SI_do_I4_PI_Production]'';

-- Installation step 495
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZMST_SIH] ENABLE TRIGGER [fask_trg_CZMST_SIH_HISTORY]'';

-- Installation step 496
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[CZPRO_VPH] ENABLE TRIGGER [trg_CZPRO_VPH_ActiveChange_new]'';

-- Installation step 497
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[FASK_Events] ENABLE TRIGGER [InsertEvents]'';

-- Installation step 498
EXEC sys.sp_executesql N''ALTER TABLE [dbo].[MachineStateSet] ENABLE TRIGGER [fask_trg_machinestatesethistory]'';
IF (SELECT COUNT(*) FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type IN (''FN'',''IF'',''TF'')) <> 14
    THROW 51002, N''Object count mismatch: FUNCTION, expected 14.'', 1;
IF (SELECT COUNT(*) FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type = ''U'') <> 146
    THROW 51002, N''Object count mismatch: TABLE, expected 146.'', 1;
IF (SELECT COUNT(*) FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type = ''V'') <> 19
    THROW 51002, N''Object count mismatch: VIEW, expected 19.'', 1;
IF (SELECT COUNT(*) FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type = ''P'') <> 85
    THROW 51002, N''Object count mismatch: PROCEDURE, expected 85.'', 1;
IF (SELECT COUNT(*) FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type = ''TR'') <> 26
    THROW 51002, N''Object count mismatch: TRIGGER, expected 26.'', 1;

IF EXISTS (SELECT 1 FROM sys.sql_expression_dependencies
           WHERE referenced_database_name LIKE N''StwPh[_]%'')
    THROW 51003, N''Unexpected static dependency on a Pohoda database.'', 1;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE is_disabled=1 OR is_not_trusted=1)
    THROW 51004, N''Disabled or untrusted foreign key.'', 1;
IF EXISTS (SELECT 1 FROM sys.triggers WHERE parent_class=1 AND is_disabled=1)
    THROW 51005, N''Disabled trigger.'', 1;
COMMIT TRANSACTION;
SELECT N''FASK schema created; application operations have not been tested.'' AS Result;
SELECT type_desc, COUNT(*) AS ObjectCount
FROM sys.objects WHERE schema_id=SCHEMA_ID(N''dbo'') AND type IN (''U'',''P'',''V'',''FN'',''IF'',''TF'',''TR'')
GROUP BY type_desc;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
';

-- ARCHIV: nasledujici objekty se neprovadeji.

-- -- =============================================
--  -- Author:		Bc. Tadeas Divacky
--  -- Create date: 16.12.2020
--  -- Description:	
--  -- =============================================
--  CREATE FUNCTION [dbo].[fask_func_planovani_VydejFilterToSI] 
--  (
--  	-- Add the parameters for the function here
--  	@ITEMNMBR nvarchar(40) = null,
--  	@SKL_ID nvarchar(20) = null,
--  	@ORD int = null
--  )
--  RETURNS bit
--  AS
--  BEGIN
--  
--  	DECLARE @Result bit;
--  	DECLARE @Typ int;
--  
--  
--  	SET @Result = 0;
--  
--  	--Funkce musí byt, ale defaultně by mnela vracet 0, jakožto FALSE, a tym padem to nebude pšenašet do SI žadne řadky
--  
--  
--  	SELECT distinct @Typ = S.RelSkTyp FROM StwPh_63489040_2025.dbo.SKz as S where S.ID = @ITEMNMBR 
--  	--AND S.RefSklad = @SKL_ID
--  
--  --Vsechny = 0,
--  --Karta = 1,
--  --Textova = 2,
--  --Sluzba = 3,
--  --Komplet = 4,
--  --Vyrobek = 5,
--  --Souprava = 6
--  
--  IF @Typ = 0
--  	BEGIN
--  		SET @Result = 0;
--  	END
--  ELSE IF  @Typ = 1
--  	BEGIN
--  		SET @Result = 0;
--  	END
--  ELSE IF  @Typ = 2
--  	BEGIN
--  		SET @Result = 0;
--  	END
--  ELSE IF  @Typ = 3
--  	BEGIN
--  		SET @Result = 1;
--  	END
--  ELSE IF  @Typ = 5
--  	BEGIN
--  		SET @Result = 0;
--  	END
--  ELSE IF  @Typ = 5
--  	BEGIN
--  		SET @Result = 0;
--  	END
--  
--  return @Result;
--  
--  END

-- -- =============================================
--  -- Author:		Tadeas Divacky
--  -- Create date: 
--  -- Description:	Vrací data pro import do pohody
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_Get_CompareToIS_FromFASK] 
--  (
--  	-- Add the parameters for the function here
--  		@SKL_ID nvarchar(25),
--  		@V_0 bit,
--  		@V_1 bit,
--  		@V_2 bit,
--  		@V_3 bit,
--  		@V_4 bit,
--  		@V_5 bit,
--  		@V_6 bit
--  )
--  RETURNS
--   @returnList TABLE 
--   (
--  	[ITEMNMBR] [nvarchar](40) NULL,
--  	[QTYSHPPD] [numeric](19, 5) NOT NULL,
--  	[SKL_ID] [nvarchar](20) NULL,
--  	[SERLTNUM] [nvarchar](50) NOT NULL,
--  	[Expirace] [datetime] NULL,
--  	[status] int not null
--   )
--  AS
--  BEGIN
--  
--  ----------------------------------------
--  -- Varianta 0  -------------------------
--  ----------------------------------------
--  
--  IF @V_0 = 1
--  	BEGIN 
--  
--  			INSERT INTO @returnList 
--  			(
--  			[ITEMNMBR],
--  			[QTYSHPPD],
--  			[SKL_ID] ,
--  			[SERLTNUM],
--  			[Expirace],
--  			[status]
--  			)
--  			SELECT
--  		 LOK.ITEMNMBR
--  		, SUM(LOK.QTYSHPPD) as QTYSHPPD
--  		, LOK.SKL_ID
--  		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  		, LOK.EXPIRATION as [Expirace]
--  		, 0 as [status]
--  		FROM
--  (
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  		where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  		--where LOK.SKL_ID = @SKL_ID 
--  		group by
--  		  Z.ID,
--  		  Z.RefSklad,
--  		  S.VCislo,
--  		  S.DatExp,
--  		 LOK.ITEMNMBR
--  		, LOK.SKL_ID
--  		, LOK.SERLTNUM
--  		, LOK.EXPIRATION
--  		having Z.ID is null
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 1  -------------------------
--  ----------------------------------------
--  
--  IF @V_1 = 1
--  	BEGIN 
--  
--  			INSERT INTO @returnList 
--  			(
--  			[ITEMNMBR],
--  			[QTYSHPPD],
--  			[SKL_ID] ,
--  			[SERLTNUM],
--  			[Expirace],
--  			[status]
--  			)
--  			SELECT
--  		 LOK.ITEMNMBR
--  		, SUM(LOK.QTYSHPPD) as QTYSHPPD
--  		, LOK.SKL_ID
--  		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  		, LOK.EXPIRATION as [Expirace]
--  		, 1 as [status]
--  		FROM
--  		(
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  		--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  		where LOK.SKL_ID = @SKL_ID 
--  		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  		group by
--  		  Z.ID,
--  		  Z.RefSklad,
--  		  S.VCislo,
--  		  S.DatExp,
--  		 LOK.ITEMNMBR
--  		, LOK.SKL_ID
--  		, LOK.SERLTNUM
--  		, LOK.EXPIRATION
--  		having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) < 0
--  		AND SUM(Z.StavZ) > 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 2  -------------------------
--  ----------------------------------------
--  
--  IF @V_2 = 1
--  	BEGIN 
--  
--  			INSERT INTO @returnList 
--  			(
--  			[ITEMNMBR],
--  			[QTYSHPPD],
--  			[SKL_ID] ,
--  			[SERLTNUM],
--  			[Expirace],
--  			[status]
--  			)
--  			SELECT
--  		 LOK.ITEMNMBR
--  		, SUM(LOK.QTYSHPPD) as QTYSHPPD
--  		, LOK.SKL_ID
--  		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  		, LOK.EXPIRATION as [Expirace]
--  		, 2 as [status]
--  		FROM
--  		(
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  		LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  		--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  		where LOK.SKL_ID = @SKL_ID 
--  		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  		group by
--  		  Z.ID,
--  		  Z.RefSklad,
--  		  S.VCislo,
--  		  S.DatExp,
--  		 LOK.ITEMNMBR
--  		, LOK.SKL_ID
--  		, LOK.SERLTNUM
--  		, LOK.EXPIRATION
--  		having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) > 0
--  		AND SUM(LOK.QTYSHPPD) > 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 3  -------------------------
--  ----------------------------------------
--  
--  IF @V_3 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--   LOK.ITEMNMBR
--  , SUM(LOK.QTYSHPPD) as QTYSHPPD
--  , LOK.SKL_ID
--  , ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  , LOK.EXPIRATION as [Expirace]
--  , 3 as [status]
--  FROM
--  (
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  --where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  where LOK.SKL_ID = @SKL_ID 
--  AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  group by
--    Z.ID,
--    Z.RefSklad,
--    S.VCislo,
--    S.DatExp,
--   LOK.ITEMNMBR
--  , LOK.SKL_ID
--  , LOK.SERLTNUM
--  , LOK.EXPIRATION
--  having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) = 0
--  AND SUM(LOK.QTYSHPPD) > 0
--  AND SUM(Z.StavZ) > 0
--  
--  END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 4  -------------------------
--  ----------------------------------------
--  
--  IF @V_4 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--   LOK.ITEMNMBR
--  , SUM(LOK.QTYSHPPD) as QTYSHPPD
--  , LOK.SKL_ID
--  , ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  , LOK.EXPIRATION as [Expirace]
--  , 4 as [status]
--  FROM
--  (
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  --where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  where LOK.SKL_ID = @SKL_ID 
--  AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  group by
--    Z.ID,
--    Z.RefSklad,
--    S.VCislo,
--    S.DatExp,
--   LOK.ITEMNMBR
--  , LOK.SKL_ID
--  , LOK.SERLTNUM
--  , LOK.EXPIRATION
--  having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) < 0
--  AND SUM(Z.StavZ) = 0
--  
--  END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 5  -------------------------
--  ----------------------------------------
--  
--  IF @V_5 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--   LOK.ITEMNMBR
--  , SUM(LOK.QTYSHPPD) as QTYSHPPD
--  , LOK.SKL_ID
--  , ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  , LOK.EXPIRATION as [Expirace]
--  , 5 as [status]
--  FROM
--  (
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  --where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  where LOK.SKL_ID = @SKL_ID 
--  AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  group by
--    Z.ID,
--    Z.RefSklad,
--    S.VCislo,
--    S.DatExp,
--   LOK.ITEMNMBR
--  , LOK.SKL_ID
--  , LOK.SERLTNUM
--  , LOK.EXPIRATION
--  having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) > 0
--  AND SUM(LOK.QTYSHPPD) = 0
--  
--  END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 6  -------------------------
--  ----------------------------------------
--  
--  IF @V_6 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--   LOK.ITEMNMBR
--  , SUM(LOK.QTYSHPPD) as QTYSHPPD
--  , LOK.SKL_ID
--  , ISNULL(LOK.SERLTNUM,'') as SERLTNUM
--  , LOK.EXPIRATION as [Expirace]
--  , 6 as [status]
--  FROM
--  (
--  	SELECT
--  	SUM(QTYSHPPD) as QTYSHPPD,
--  	ITEMNMBR, 
--  	SERLTNUM, 
--  	SKL_ID, 
--  	EXPIRATION
--  FROM
--  CZMST_SkladLokace_Stav 
--  Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
--  as LOK 
--  LEFT JOIN StwPh_63489040_2025.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
--  LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  --where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
--  where LOK.SKL_ID = @SKL_ID 
--  AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
--  group by
--    Z.ID,
--    Z.RefSklad,
--    S.VCislo,
--    S.DatExp,
--   LOK.ITEMNMBR
--  , LOK.SKL_ID
--  , LOK.SERLTNUM
--  , LOK.EXPIRATION
--  having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) = 0
--  AND SUM(LOK.QTYSHPPD) = 0
--  AND SUM(Z.StavZ) = 0
--  
--  END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  return 
--  END

-- -- =============================================
--  -- Author:		Tadeas Divacky
--  -- Create date: 
--  -- Description:	Vrací data pro import do pohody
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_Get_CompareToIS_FromIS] 
--  (
--  	-- Add the parameters for the function here
--  		@SKL_ID nvarchar(25) ,
--  		@V_0 bit,
--  		@V_1 bit,
--  		@V_2 bit,
--  		@V_3 bit,
--  		@V_4 bit,
--  		@V_5 bit,
--  		@V_6 bit
--  )
--  RETURNS
--   @returnList TABLE 
--   (
--  	[ITEMNMBR] [nvarchar](40) NULL,
--  	[QTYSHPPD_Pohoda] [numeric](19, 5) NOT NULL,
--  	[QTYSHPPD_LokMech] [numeric](19, 5) NULL,
--  	[SKL_ID] [nvarchar](20) NULL,
--  	[SERLTNUM] [nvarchar](50) NOT NULL,
--  	[Expirace] [datetime] NULL,
--  	[status] int not null
--   )
--  AS
--  BEGIN
--  
--  ----------------------------------------
--  -- Varianta 0  -------------------------
--  ----------------------------------------
--  
--  IF @V_0 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  		x.ITEMNMBR,
--  		SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  		SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  		x.SKL_ID,
--  		x.SERLTNUM,
--  		x.EXPIRATION as Expirace,
--  		0 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	--where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  		x.ITEMNMBR,
--  		x.SKL_ID,
--  		x.SERLTNUM,
--  		x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having LOK.ITEMNMBR is null
--  
--  	END
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 1  -------------------------
--  ----------------------------------------
--  
--  IF @V_1 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  1 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) < 0
--  	AND SUM(x.QTYSHPPD) > 0
--  
--  	END
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 2  -------------------------
--  ----------------------------------------
--  
--  IF @V_2 = 1
--  	BEGIN 
--  
--  	INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  2 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) > 0
--  	AND SUM(LOK.QTYSHPPD) > 0
--  
--  	END
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 3  -------------------------
--  ----------------------------------------
--  
--  IF @V_3 = 1
--  	BEGIN 
--  
--  		INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  3 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) = 0
--  	AND SUM(x.QTYSHPPD) > 0
--  	AND SUM(LOK.QTYSHPPD) > 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 4  -------------------------
--  ----------------------------------------
--  
--  IF @V_4 = 1
--  	BEGIN 
--  
--  		INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  4 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) < 0
--  	AND SUM(x.QTYSHPPD) = 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 5  -------------------------
--  ----------------------------------------
--  
--  IF @V_5 = 1
--  	BEGIN 
--  
--  		INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  5 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) > 0
--  	AND SUM(LOK.QTYSHPPD) = 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  ----------------------------------------
--  -- Varianta 6  -------------------------
--  ----------------------------------------
--  
--  IF @V_6 = 1
--  	BEGIN 
--  
--  		INSERT INTO @returnList 
--  	(
--  	[ITEMNMBR],
--  	[QTYSHPPD_Pohoda],
--  	[QTYSHPPD_LokMech],
--  	[SKL_ID] ,
--  	[SERLTNUM],
--  	[Expirace],
--  	[status]
--  	)
--  	SELECT
--  	  x.ITEMNMBR,
--  	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
--  	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION as Expirace,
--  	  6 as [status]
--  	FROM
--  	(
--  	SELECT
--  	Z.ID as ITEMNMBR,
--  	Z.StavZ as QTYSHPPD,
--  	ISNULL(S.VCislo,'') as SERLTNUM,
--  	Z.RefSklad as SKL_ID,
--  	S.DatExp as EXPIRATION
--  	FROM StwPh_63489040_2025.dbo.SKz AS Z
--  	LEFT JOIN StwPh_63489040_2025.dbo.SKzVC as S ON S.RefAg = Z.ID
--  	) as x
--  	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
--  	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
--  	where x.SKL_ID = @SKL_ID 
--  	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
--  	group by
--  	  x.ITEMNMBR,
--  	  x.SKL_ID,
--  	  x.SERLTNUM,
--  	  x.EXPIRATION
--  	, LOK.ITEMNMBR
--  	, LOK.SKL_ID
--  	, LOK.SERLTNUM
--  	, LOK.EXPIRATION
--  	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) = 0
--  	AND SUM(x.QTYSHPPD) = 0
--  	AND SUM(LOK.QTYSHPPD) = 0
--  
--  	END
--  
--  ----------------------------------------
--  ----------------------------------------
--  ----------------------------------------
--  
--  
--  
--  return 
--  END

-- -- =============================================
--   -- Author:	Bc. Tadeas Divacky
--   -- Create date: 22.01.2020
--   -- Description:	Funkce která vraci uživatelske parametry pro filtrovani do načteí obchodniho požadavk pro Planovani
--   -- =============================================
--   CREATE FUNCTION [dbo].[FASK_Get_POHODA_LokMechMapingID]
--   (	
--   	@ITEMNMBR_Zdroj nvarchar(40) = null,
--  	@SKL_ID_Zdroj nvarchar(20) = null,
--  	@SKL_ID_Cil nvarchar(20) = null
--   )
--   RETURNS @Params TABLE
--   (
--  	[ITEMNMBR] nvarchar(40) NULL,
--   	[ITEMDESC] [nvarchar](100) NULL
--   )
--   AS
--   BEGIN
--   
--   
--    insert into @Params ([ITEMNMBR], [ITEMDESC])
--  	SELECT 
--  	N.ID as [ITEMNMBR],
--  	LEFT(N.Nazev,100) as [ITEMDESC] 
--  	FROM
--  	(SELECT IDS, EAN, Nazev,MJ, RefAD, RelSKzVC from StwPh_63489040_2025.dbo.Skz 
--  	where ID = @ITEMNMBR_Zdroj AND RefSklad = @SKL_ID_Zdroj) as O
--  	LEFT JOIN StwPh_63489040_2025.dbo.Skz as N ON 
--  	ISNULL(N.IDS,'') = ISNULL(O.IDS,'') 
--  	AND  ISNULL(N.EAN,'') = ISNULL(O.EAN,'')
--  	AND ISNULL(N.Nazev,'') = ISNULL(O.Nazev,'')
--  	AND ISNULL(N.MJ,'') = ISNULL(O.MJ,'')
--  	AND ISNULL(N.RefAD,'') = ISNULL(O.RefAD,'')
--  	AND ISNULL(N.RelSKzVC,'') = ISNULL(O.RelSKzVC,'')
--  	AND N.ID != @ITEMNMBR_Zdroj
--  	AND N.RefSklad = @SKL_ID_Cil
--  
--    	return
--   
--   END

-- -- =============================================
--  -- Author:		Tadeas Divacky
--  -- Create date: 
--  -- Description:	Vrací data pro pohyby v IS POHODA
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_Get_PohybyFromPOHODA] 
--  (
--  	-- Add the parameters for the function here
--  )
--  RETURNS
--   @returnList TABLE 
--   (
--  	[ITEMNMBR] [nvarchar](100) NULL,
--  	[TypPohybu] [nvarchar](100) NULL,
--  	[ZdrojPohybu] [nvarchar](100) NULL,
--  	[DatumPohybu]datetime null,
--  	[PocetNaPohybu][numeric](19,5) null,
--  	[StavPoPohybu] [numeric](19,5) null,
--  	[CisloDokladu][nvarchar](100) null,
--  	[KdoVytvoril] [nvarchar](100) null,
--  	[Ucetni] [nvarchar](100) null,
--  	[DatumVytvoreni] datetime null,
--  	[DatumUlozeni] datetime null
--   )
--  AS
--  BEGIN
--  
--  
--  ----------------------------------------------
--  ------------------Faktura Vydana -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Faktura Vydaná' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.FA as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 2
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Faktura Prijata -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Faktura Přijatá' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.FA as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 3
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Prijemka -------------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Příjemka' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.SKPP as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 6
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Výdejka -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Výdejka' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.SKPV as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 7
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Výroba -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Výroba' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.SKMV as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 9
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Prevodka -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Převodka' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.SKMP as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 8
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  ----------------------------------------------
--  ------------------Prodejka -------------
--  ----------------------------------------------
--  
--  
--  INSERT INTO @returnList 
--  (
--  	[ITEMNMBR],
--  	[TypPohybu],
--  	[ZdrojPohybu],
--  	[DatumPohybu],
--  	[PocetNaPohybu],
--  	[StavPoPohybu],
--  	[CisloDokladu],
--  	[KdoVytvoril],
--  	[Ucetni],
--  	[DatumVytvoreni],
--  	[DatumUlozeni] 
--  )
--  select 
--  poh.RefSKz as ITEMNMBR,
--  CASE	
--  	WHEN poh.RelOP = 1 THEN 'Příjem'
--  	WHEN poh.RelOP = 2 THEN 'Výdej'
--  	ELSE '-'
--  END as TypPohybu,
--  'Prodejka' as ZdrojPohybu,
--  poh.Datum as DatumPohybu,
--  poh.PohPMJ as PocetNaPohybu,
--  poh.StavZ as  StavPoPohybu,
--  poh.Cislo as CisloDokladu,
--  AGENDA.Creator as KdoVytvoril,
--  AGENDA.Ucetni as Ucetni,
--  AGENDA.DatCreate as DatumVytvoreni,
--  AGENDA.DatSave as DatumUlozeni
--  from StwPh_63489040_2025.dbo.SKzPoh as poh
--  left join StwPh_63489040_2025.dbo.PH as AGENDA ON AGENDA.Cislo = poh.Cislo
--  where RelAg = 10
--  
--  ----------------------------------------------
--  ----------------------------------------------
--  ----------------------------------------------
--  
--  return 
--  END

-- -- =============================================
--  -- Author:	Tadeas Divacky
--  -- Create date: 22.9.2020
--  -- Description:	Funkce upravena pro Hanibal
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_GetDavkyByCarKody]
--  (	
--  	-- Add the parameters for the function here
--  	@TerminalID int ,
--  	@SkladID int,
--  	@CarKod nvarchar(500)
--  )
--  RETURNS @Prijemky TABLE
--  (
--  	[PONUMBER] [nvarchar](30) not NULL,
--  	[Name] [nvarchar](31) NULL,
--  	[CZ_CarKod] [nvarchar](70) not NULL,
--  	[DateTime] [datetime] NULL,
--  	[Desc] [nvarchar](100) not NULL
--  
--  )
--  AS
--  BEGIN
--  
--  IF (@CarKod = '')
--  BEGIN
--  
--  insert into @Prijemky ([PONUMBER], [Name], [CZ_CarKod], [DateTime],[Desc])
--  		SELECT 
--  		--*
--  		Left(o.Cislo, 30) as PONUMBER,
--  		Left(o.Firma, 31) as [Name],
--  		Left(o.Cislo,70) as CZ_CarKod,
--  		o.DatCreate as [datetime],
--  		Left(isnull(o.SText,''),100) as [Desc]
--  		FROM StwPh_63489040_2025.dbo.OBJ o
--  join (
--  select op2.RefAg--, Count(*) pp 
--  	from (
--  		--select distinct op0.refag, op0.refskz from StwPh_63489040_2025.dbo.objpol op0
--  		select 
--  		op0.refag, 
--  		op0.refskz, 
--  		Sum(op0.Mnozstvi) - SUM(op0.Dodano) ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
--  		from StwPh_63489040_2025.dbo.objpol op0
--  		group by op0.RefAg, op0.RefSKz
--  	) op2
--  	where 
--  		1=1
--  		AND op2.refSkz in (
--  			select s.id from StwPh_63489040_2025.dbo.SKz s
--  		where 
--  		--s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
--  		--and
--  		s.refsklad=@SkladID --cislo skladu
--  	)
--  		and op2.ZbyvaDodat > 0
--  group by op2.RefAg--, op.refskz
--  --having Count(*) >= (SELECT count(*) FROM dbo.splitstring(@CarKod, default))
--  ) as objscarkody on objscarkody.refag = o.id
--  where 
--  	1=1
--  	and o.reltpobj in (2)	
--  	and	o.vyrizeno=0 
--  order by o.datcreate 
--  
--  --	return
--  
--  END
--  
--   ELSE
--  
--   BEGIN
--  
--   insert into @Prijemky ([PONUMBER], [Name], [CZ_CarKod], [DateTime],[Desc])
--  		SELECT 
--  		--*
--  		Left(o.Cislo, 30) as PONUMBER,
--  		Left(o.Firma, 31) as [Name],
--  		Left(o.Cislo,70) as CZ_CarKod,
--  		o.DatCreate as [datetime],
--  		Left(isnull(o.SText,''),100) as [Desc]
--  		FROM StwPh_63489040_2025.dbo.OBJ o
--  join 
--  (
--  select op2.RefAg--, Count(*) pp 
--  	from (
--  		--select distinct 
--  		--	op0.refag, 
--  		--	op0.refskz, 
--  		--	op0.Mnozstvi - op0.Dodano ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
--  		--	from StwPh_63489040_2025.dbo.objpol op0
--  		select 
--  			op0.refag, 
--  			op0.refskz, 
--  			Sum(op0.Mnozstvi) - SUM(op0.Dodano) ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
--  			from StwPh_63489040_2025.dbo.objpol op0
--  			group by op0.RefAg, op0.RefSKz
--  		) op2
--  	where 
--  		1=1
--  		and op2.refSkz in (
--  			select s.id from StwPh_63489040_2025.dbo.SKz s
--  		where 
--  		s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
--  		and
--  		s.refsklad=@SkladID --cislo skladu
--  	)
--  		and op2.ZbyvaDodat > 0
--  group by op2.RefAg--, op.refskz
--  having Count(*) >= (SELECT count(*) FROM dbo.splitstring(@CarKod, default))
--  ) as objscarkody on objscarkody.refag = o.id
--  where 
--  	1=1
--  	and o.reltpobj in (2)	
--  	and	o.vyrizeno=0 
--  order by o.datcreate 
--  
--  
--  END
--  
--   	return
--  
--  END

-- -- =============================================
--  -- Author:	Jiří Skřivánek a Tadeas Divacky
--  -- Create date: 14.12.2018
--  -- Description:	
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_GetDodavatelByCarKody]
--  (	
--  	-- Add the parameters for the function here
--  	@TerminalID int ,
--  	@SkladID int,
--  	@CarKod nvarchar(500)
--  )
--  RETURNS @Prijemky TABLE
--  (
--  	[odb_id] [nvarchar](12) not NULL,
--  	[odb_desc] [nvarchar](31) NULL,
--  	[odb_typ] [nvarchar](3) NULL,
--  	[odb_carcode] [nvarchar](21) NULL,
--  	[odb_ico] [nvarchar](20) NULL,
--  	[mena_ID] [nvarchar](10) NULL,
--  		[DEX_ROW_ID] int not NULL
--  )
--  AS
--  BEGIN
--  
--  
--   insert into @Prijemky ([odb_id], [odb_desc], [odb_typ], [odb_carcode],[odb_ico], [mena_ID], [DEX_ROW_ID])
--  		SELECT 
--  		a.ID as odb_id,
--  		isnull(Left(a.Firma, 31),'') as odb_desc,
--  		0 as odb_typ,
--  		isnull(Left(a.Cislo, 21),'') as odb_carcode,
--  		isnull(Left(a.ICO, 20),'') as odb_ico,
--  		null as mena_ID,
--  		a.ID as DEX_ROW_ID
--  		FROM StwPh_63489040_2025.dbo.AD a
--  		where a.ID in 
--  		(
--  		
--  select y.RefAD
--  --, count(*) pocetSpolecnych
--  from 
--  (		
--  	select x.*
--  	, count(*) pocetSpolecnych
--  	from
--  	(
--  			select s.RefAD, s.ID as SkzID
--  			from StwPh_63489040_2025.dbo.SKz s
--  			where s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default)) and s.refsklad=@SkladID
--  
--  			union all
--  
--  			select RefAD, RefAg as SkzID
--  			from StwPh_63489040_2025.dbo.SKzNC n
--  			where n.RefAg in (
--  				select s.ID from StwPh_63489040_2025.dbo.SKz s
--  				where 
--  				s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
--  				and
--  				s.refsklad=@SkladID
--  			)
--  	) as x
--  	group by x.RefAD, x.SkzID
--  ) as y
--  group by y.RefAD
--  having count(*) >= (SELECT Count(*) FROM dbo.splitstring(@CarKod, default))
--  
--  		)
--  
--  
--   	return
--  
--  END

-- -- =============================================
-- -- Author:		Tadeas Divacky
-- -- Create date:		02.11.2021 
-- -- Description:		Vrací data pro import do pohody
-- -- =============================================
-- CREATE FUNCTION [dbo].[FASK_GetGroupFromSI] 
-- (
-- 	-- Add the parameters for the function here
-- 		@CountEntries nvarchar(25) 
-- )
-- RETURNS
--  @returnList TABLE 
--  (
-- 	[CountEntries] [int] NOT NULL,
-- 	[SOPNUMBE] [nvarchar](30) NOT NULL,
-- 	[ITEMNMBR] [nvarchar](40) NULL,
-- 	[ORD] [int] NOT NULL,
-- 	[VNDITNUM] [nvarchar](60) NULL,
-- 	[CZ_CarKod] [nvarchar](70) NULL,
-- 	[SKL_ID] [nvarchar](20) NULL,
-- 	[QTYSHPPD] [numeric](19, 5) NOT NULL,
-- 	[QTYPACK] [numeric](19, 5) NOT NULL,
-- 	[USER_ID] [int] NOT NULL,
-- 	[DEX_ROW_ID] [int] NOT NULL,
-- 	[ID_TERMINAL] [int] NOT NULL,
-- 	[MJ] [nvarchar](10) NOT NULL,
-- 	[INPUT_MODE] [tinyint] NOT NULL,
-- 	[GUID] [uniqueidentifier] NULL,
-- 	[VNDDOCNM] [nvarchar](21) NULL,
-- 	[Expirace] [datetime] NULL,
-- 	[SERLTNUM] [nvarchar](50) NOT NULL
--  )
-- AS
-- BEGIN
-- 
-- 
-- 	DECLARE @RelSKzVC int
-- 	DECLARE @VPrFXTS int
-- 	DECLARE @VPrFVTS int
-- 	DECLARE @CNT int
-- 
-- DECLARE @TMP_Table TABLE
-- (
--  RelSKzVC int, 
--  VPrFXTS int, 
--  VPrFVTS int
-- );
-- 
-- 
-- INSERT INTO @TMP_Table
-- (
-- RelSKzVC,
-- VPrFXTS,
-- VPrFVTS
-- )
-- select S.RelSKzVC, S.VPrFXTS, S.VPrFVTS from 
-- (
-- select ITEMNMBR from CZMST_SI
-- where CountEntries = @CountEntries
-- group by ITEMNMBR
-- ) as x 
-- left join StwPh_04535667_2020.dbo.SKz as S ON S.ID = X.ITEMNMBR
-- group by S.RelSKzVC, S.VPrFXTS, S.VPrFVTS
-- 
-- select 
-- @RelSKzVC = RelSKzVC,
-- @VPrFXTS = VPrFXTS,
-- @VPrFVTS = VPrFVTS,
-- @CNT = count(*) 
-- from @TMP_Table
-- group by RelSKzVC, VPrFXTS, VPrFVTS
-- 
-- IF @CNT = 1
-- 	BEGIN
-- 	--mame jeden radek
-- 
-- 
-- 		IF @RelSKzVC > 0
-- 			BEGIN
-- 			 INSERT INTO @returnList 
-- 			 (
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM]
-- 			 )
-- 			 SELECT
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM] 
-- 				FROM CZMST_SI_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
-- 
-- 			END
-- 		ELSE
-- 			BEGIN
-- 			 INSERT INTO @returnList 
-- 			 (
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM]
-- 			 )
-- 			 SELECT
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				null as [Expirace],
-- 				'' as [SERLTNUM] 
-- 				FROM CZMST_SI_NO_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
-- 			END
-- 	END
-- ELSE
-- 	BEGIN
-- 	-- mame vico radku
-- 	
-- 	declare @cntVicRadku int
-- 
-- 	select @cntVicRadku = count(*) from @TMP_Table where RelSKzVC > 0 group by RelSKzVC
-- 
-- 	IF @cntVicRadku > 0
-- 		BEGIN
-- 			 INSERT INTO @returnList 
-- 			 (
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM]
-- 			 )
-- 			 SELECT
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM] 
-- 				FROM CZMST_SI_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
-- 		END
-- 	ELSE
-- 		BEGIN
-- 			 INSERT INTO @returnList 
-- 			 (
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				[Expirace],
-- 				[SERLTNUM]
-- 			 )
-- 			 SELECT
-- 				[CountEntries],
-- 				[SOPNUMBE],
-- 				[ITEMNMBR],
-- 				[ORD],
-- 				[VNDITNUM] ,
-- 				[CZ_CarKod],
-- 				[SKL_ID] ,
-- 				[QTYSHPPD],
-- 				[QTYPACK] ,
-- 				[USER_ID] ,
-- 				[DEX_ROW_ID],
-- 				[ID_TERMINAL],
-- 				[MJ],
-- 				[INPUT_MODE],
-- 				[GUID],
-- 				[VNDDOCNM],
-- 				null as [Expirace],
-- 				'' as [SERLTNUM] 
-- 				FROM CZMST_SI_NO_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
-- 		END
-- 	END
-- 
-- return 
-- END

-- -- =============================================
--  -- Author:		Tadeáš Divácký
--  -- Create date: 4.5.2021
--  -- Description:	Funkce která vrací list ID služek ktere jsou pod kartou v objednávce
--  -- =============================================
--  CREATE FUNCTION [dbo].[FASK_GetListPolozekPoSluzbe]
--  (
--  	-- Add the parameters for the function here
--  	@ORD int 
--  )
--  RETURNS @List_ORD TABLE 
--  (
--  	ID_Polozky int
--  )
--  AS
--  BEGIN
--  	
--  
--  DECLARE @ID int;
--  DECLARE @RelSkTyp int;
--  
--  
--  DECLARE curPol CURSOR FOR
--  SELECT 
--  o.ID, 
--  zas.RelSkTyp 
--  FROM
--  (SELECT OrderFld, RefAg  FROM StwPh_63489040_2025.dbo.OBJpol as op
--  left join StwPh_63489040_2025.dbo.SKz as z ON z.ID = op.RefSKz
--  where 1 = 1 AND op.ID = @ORD) as x
--  LEFT JOIN StwPh_63489040_2025.dbo.OBJpol as o ON o.RefAg = x.RefAg
--  LEFT JOIN StwPh_63489040_2025.dbo.SKz as zas ON zas.ID = o.RefSKz
--  where 1=1
--  AND o.RefAg = x.RefAg
--  AND o.OrderFld > x.OrderFld
--  order by o.OrderFld
--  
--  OPEN curPol
--  WHILE (1=1) 
--  	BEGIN
--  	
--  	FETCH NEXT FROM curPol INTO @ID, @RelSkTyp 
--  	IF @@FETCH_STATUS <> 0
--  	BEGIN
--  		BREAK
--  	END
--  
--  	IF @RelSkTyp in (3)
--  		BEGIN
--  			insert into @List_ORD(ID_Polozky) values (@ID)
--  		END
--  	ELSE
--  		BEGIN
--  			BREAK
--  		END
--  
--  END
--  	
--  CLOSE curPol
--  DEALLOCATE curPol
--  
--  --SELECT ORD from @List_ORD
--  
--  	RETURN 
--  END

-- -- =============================================
--  -- autor: Jiri Skrivanek
--  -- firma: FASK, spol. s r.o.
--  -- datum: 30.5.2018
--  -- =============================================
--  CREATE PROCEDURE [dbo].[FASK_PrijemGetSkladExpedice]
--      @Itemnmbr			NVarChar(31),			-- ID polozky prijimane
--      @MnozstviZadane		numeric(19,5),			-- pocet prijimanych terminalem
--  	@MnozstviNasnimane		numeric(19,5),			-- pocet prijatych terminalem
--  	-- out hodnoty
--  	@MnozstviDodavatelePozadovano	numeric(19,5) out,	-- požadováno od dodavatele(ů) 
--  													-- => Množství Dodat(objednáno) pro nevyřízené objednávky vydané
--  	@MnozstviDodavateleDodano		numeric(19,5) out,	-- dodáno od dodavatele(ů) 
--  													-- => Množství Dodáno (převedeno) pro nevyřízené objednávky vydané
--  	@MnozstviDodavateleDodat		numeric(19,5) out,	-- zbývá dodat od dodavatele(ů) 
--  													-- => Množství Dodat(objednáno) – Dodáno(převedeno) pro nevyřízené objednávky vydané
--  	@MnozstviOdberateliPozadovano	numeric(19,5) out,	-- požadováno odběrateli 
--  													-- => Množsvtí Dodat z nevyřízených objednávek přijatých
--  													-- => ??? (jen nekryté množství) 
--  													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
--  	@MnozstviOdberatelumDodano		numeric(19,5) out,	-- dodáno odběratelům 
--  													-- => ??? (resp. Na sklad přijato z terminálu?)
--  													-- => Množsvtí Dodáno z nevyřízených objednávek přijatých
--  													-- => zohlednit i příjemky, tedy kolik je materiálu na skladě?
--  	@MnozstviOdberatelumDodat		numeric(19,5) out,	-- zbývá dodat odběratelům
--  													-- => Množsvtí Dodat – Dodáno z nevyřízených objednávek přijatých
--  													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
--  	@Vysledek 			Numeric(19,5)	out		-- Vysledek vypoctu = (<Zbyva dodat odberatelum> - <prijato terminalem>)
--  AS
--  BEGIN
--  	SET NOCOUNT ON;
--  
--  	Set	@MnozstviDodavatelePozadovano=0
--  	Set	@MnozstviDodavateleDodano=0
--  	Set	@MnozstviDodavateleDodat=0
--  	Set	@MnozstviOdberateliPozadovano=0
--  	Set	@MnozstviOdberatelumDodano=0
--  	Set	@MnozstviOdberatelumDodat=0
--  	Set	@Vysledek=0
--  
--  	Declare 
--  		@Rezervovano numeric(19,5),
--  		@Reklamovano numeric(19,5),
--  		@Stav		 numeric(19,5)
--  
--  	--Objednávky (SKz.ObjedP) + Rezervace (SKz.Rezer) + Reklamace (SKz.Reklam)  - Stav zásoby skladem (SKz.stavZ)  = počet kolik dát na expedici
--  	select 
--  		@MnozstviDodavateleDodat =	SKz.ObjedV,
--  		@MnozstviOdberatelumDodat = SKz.ObjedP,
--  		@Rezervovano =				SKz.Rezer,
--  		@Reklamovano =				SKz.Reklam,
--  		@Stav =						SKz.StavZ
--  	from StwPh_63489040_2025.dbo.Skz 
--  	where ID = @Itemnmbr
--  
--  	Set @Vysledek = @MnozstviOdberatelumDodat + @Rezervovano + @Reklamovano - @Stav - @MnozstviNasnimane
--  
--  	RETURN 0	-- ok... procedura prosla korektne ... 
--  
--  END

-- CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY_MaR]
-- 	@ExportTypFilter [nvarchar](100),
-- 	@ExportSkladFilter [nvarchar](100),
-- 	@ExportovatPouzeAktivniPolozky [bit],
-- 	@EXZas_DotahovatAlternativniDodavatele [bit],
-- 	@EvidenceSarzi [bit],
-- 	@EvidenceVyrobnichCisel [bit],
-- 	@PohodaE1 [bit]
-- WITH EXECUTE AS CALLER
-- AS
-- BEGIN
-- 	-- SET NOCOUNT ON added to prevent extra result sets from
-- 	-- interfering with SELECT statements.
-- 	SET NOCOUNT ON;
-- 
-- DECLARE @TrackExp INT;
-- DECLARE @DBName [nvarchar](100) = 'StwPh_63489040_2025'; --chci vsude tam kde se objevuje 'StwPh_63489040_2025' dat tento parametr, projdi cely kod tedy script a zmen mi to abych ho mohl pouzit v produkcnim reseni..vrat mi tedy soubor .sql
-- 
-- IF @PohodaE1 = 0
-- BEGIN
-- 	SET @TrackExp = 0;
-- END
-- ELSE IF COL_LENGTH('StwPh_63489040_2025.dbo.SKz', 'VPrCZExpTrackIS') IS NOT NULL
-- BEGIN
-- 	DECLARE @SQL nvarchar(max);
-- 	SET @SQL = N'SELECT TOP 1 @TrackExpOUT = ISNULL(VPrCZExpTrackIS, 0) FROM StwPh_63489040_2025.dbo.SKz;';
-- 	EXEC sp_executesql @SQL, N'@TrackExpOUT int OUTPUT', @TrackExpOUT=@TrackExp OUTPUT;
-- END
-- ELSE
-- BEGIN
-- 	SET @TrackExp = 0;
-- END
-- 
-- 
-- 
-- 
-- 	DELETE  FROM FASK_ZASOBY
-- 
-- 	--TODO, na aplikační urovni je udelana logika s LOCNCODE a CZ_SerNumTrack
-- 	--TODO je potreba dotahnout, a rozhodnout co vlastně udelat....
-- 
-- 
-- 	IF (@ExportSkladFilter is not null AND @ExportSkladFilter != '' ) OR (@ExportTypFilter is not null  AND @ExportTypFilter != '' )
-- BEGIN --A0
-- 	IF @ExportovatPouzeAktivniPolozky = 1
-- 		BEGIN --B0 
-- 
-- 			--SKz_FillBy_EPAP_DAD
-- 			INSERT INTO [dbo].[FASK_ZASOBY]
-- 			   ([ITEMNMBR]
-- 			   ,[ITEMDESC]
-- 			   ,[ITEMCODE]
-- 			   ,[VNDITNUM]
-- 			   ,[CZ_CarKod]
-- 			   ,[LOCNCODE]
-- 			   ,[SKL_ID]
-- 			   ,[QTY]
-- 			   ,[QTYPACK]
-- 			   ,[MJ]
-- 			   ,[DMJ]
-- 			   ,[TAXRATE]
-- 			   ,[PRICE0]
-- 			   ,[PRICE1]
-- 			   ,[PRICE2]
-- 			   ,[PRICE3]
-- 			   ,[PRICE4]
-- 			   ,[PRICE5]
-- 			   ,[CZ_SerNum_Track]
-- 			   ,[CZ_SerNum_Delka]
-- 			   ,[CZ_Rez1_Track]
-- 			   ,[CZ_Rez2_Track]
-- 			   ,[CZ_Rez3_Track]
-- 			   ,[CZ_Rez4_Track]
-- 			   ,[REZ1]
-- 			   ,[REZ2]
-- 			   ,[REZ3]
-- 			   ,[REZ4]
-- 			   ,[ODB_ID]
-- 			   ,[mena_ID]
-- 			   ,[SERLTNUM]
-- 			   ,[WEIGHT]
-- 			   ,[TIMEFROM]
-- 			   ,[TIMETO]
-- 			   ,[LSTMod]
-- 			   ,[loginid]
-- 			   ,[CZ_Expirace_Track]
-- 			   ,[EXPIRACE])
-- 			   SELECT 
-- 			   Left(SKz.ID,40) as ITEMNMBR,
-- 			   Left(SKz.Nazev, 100) as ITEMDESC,
-- 			   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 			   Left(SKz.EAN, 60) as VNDITNUM,
-- 			   '' as CZ_CarKod,
-- 			   '' as LOCNCODE,  --TODO složizejší
-- 			   SKz.RefSklad as SKL_ID,
-- 			   ISNULL(SKz.StavZ, 0) as QTY,
-- 			   0 as QTYPACK,
-- 			   Left(ISNULL(SKz.MJ, ''),10) as MJ,
-- 			   '' as DMJ,
-- 			   0 as TAXRATE,
-- 			   0 as PRICE0,
-- 			   0 as PRICE1,
-- 			   0 as PRICE2,
-- 			   0 as PRICE3,
-- 			   0 as PRICE4,
-- 			   0 as PRICE5,
-- 			   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 			   0 as CZ_SerNum_Delka,
-- 			   0 as CZ_Rez1_Track,
-- 			   0 as CZ_Rez2_Track, 
-- 			   0 as CZ_Rez3_Track, 
-- 			   0 as CZ_Rez4_Track, 
-- 			   0 as REZ1,
-- 			   0 as REZ2,
-- 			   0 as REZ3,
-- 			   0 as REZ4,
-- 			   Left(SKz.RefAD, 12) as ODB_ID,
-- 			   '' as mena_ID,
-- 			   '' as SERLTNUM,
-- 			   null as WEIGHT,
-- 			   null as TIMEFROM,
-- 			   null as TIMETO,
-- 			   GETDATE() as LSTMod,
-- 			   '' as loginid,
-- 			   --'' as CZ_Expirace_Track,
-- 			   -- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
-- 			   null as EXPIRACE
-- 		FROM StwPh_63489040_2025.dbo.SKz 
-- 		INNER JOIN StwPh_63489040_2025.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
-- 		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))
-- 
-- 			IF @EXZas_DotahovatAlternativniDodavatele = 1
-- 				BEGIN --C0
-- 
-- 					--SKzAlternatives_FillBy_EPAP_DAD
-- 					INSERT INTO [dbo].[FASK_ZASOBY]
-- 				   ([ITEMNMBR]
-- 				   ,[ITEMDESC]
-- 				   ,[ITEMCODE]
-- 				   ,[VNDITNUM]
-- 				   ,[CZ_CarKod]
-- 				   ,[LOCNCODE]
-- 				   ,[SKL_ID]
-- 				   ,[QTY]
-- 				   ,[QTYPACK]
-- 				   ,[MJ]
-- 				   ,[DMJ]
-- 				   ,[TAXRATE]
-- 				   ,[PRICE0]
-- 				   ,[PRICE1]
-- 				   ,[PRICE2]
-- 				   ,[PRICE3]
-- 				   ,[PRICE4]
-- 				   ,[PRICE5]
-- 				   ,[CZ_SerNum_Track]
-- 				   ,[CZ_SerNum_Delka]
-- 				   ,[CZ_Rez1_Track]
-- 				   ,[CZ_Rez2_Track]
-- 				   ,[CZ_Rez3_Track]
-- 				   ,[CZ_Rez4_Track]
-- 				   ,[REZ1]
-- 				   ,[REZ2]
-- 				   ,[REZ3]
-- 				   ,[REZ4]
-- 				   ,[ODB_ID]
-- 				   ,[mena_ID]
-- 				   ,[SERLTNUM]
-- 				   ,[WEIGHT]
-- 				   ,[TIMEFROM]
-- 				   ,[TIMETO]
-- 				   ,[LSTMod]
-- 				   ,[loginid]
-- 				   ,[CZ_Expirace_Track]
-- 				   ,[EXPIRACE])
-- 				   SELECT 
-- 				   Left(SKz.ID,40) as ITEMNMBR,
-- 				   Left(SKz.Nazev, 100) as ITEMDESC,
-- 				   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 				   Left(SKzNC.EAN, 60) as VNDITNUM,
-- 				   '' as CZ_CarKod,
-- 				   '' as LOCNCODE,  --TODO složizejší
-- 				   SKz.RefSklad as SKL_ID,
-- 				   ISNULL(SKz.StavZ, 0) as QTY,
-- 				   0 as QTYPACK,
-- 				   Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
-- 				   '' as DMJ,
-- 				   0 as TAXRATE,
-- 				   0 as PRICE0,
-- 				   0 as PRICE1,
-- 				   0 as PRICE2,
-- 				   0 as PRICE3,
-- 				   0 as PRICE4,
-- 				   0 as PRICE5,
-- 				   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 				   0 as CZ_SerNum_Delka,
-- 				   0 as CZ_Rez1_Track,
-- 				   0 as CZ_Rez2_Track, 
-- 				   0 as CZ_Rez3_Track, 
-- 				   0 as CZ_Rez4_Track, 
-- 				   0 as REZ1,
-- 				   0 as REZ2,
-- 				   0 as REZ3,
-- 				   0 as REZ4,
-- 				   Left(SKz.RefAD, 12) as ODB_ID,
-- 				   '' as mena_ID,
-- 				   '' as SERLTNUM,
-- 				   null as WEIGHT,
-- 				   null as TIMEFROM,
-- 				   null as TIMETO,
-- 				   GETDATE() as LSTMod,
-- 				   '' as loginid,
-- 				   --'' as CZ_Expirace_Track,
-- 				   -- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
-- 				   null as EXPIRACE
-- 			FROM StwPh_63489040_2025.dbo.SKz as SKz 
-- 			INNER JOIN StwPh_63489040_2025.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
-- 			INNER JOIN StwPh_63489040_2025.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
-- 			WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))
-- 
-- 				END--C0
-- 
-- 		END --B0
-- 	ELSE
-- 	BEGIN --B1
-- 	
-- 		--SKz_FillBy_DAD
-- 		INSERT INTO [dbo].[FASK_ZASOBY]
--            ([ITEMNMBR]
--            ,[ITEMDESC]
--            ,[ITEMCODE]
--            ,[VNDITNUM]
--            ,[CZ_CarKod]
--            ,[LOCNCODE]
--            ,[SKL_ID]
--            ,[QTY]
--            ,[QTYPACK]
--            ,[MJ]
--            ,[DMJ]
--            ,[TAXRATE]
--            ,[PRICE0]
--            ,[PRICE1]
--            ,[PRICE2]
--            ,[PRICE3]
--            ,[PRICE4]
--            ,[PRICE5]
--            ,[CZ_SerNum_Track]
--            ,[CZ_SerNum_Delka]
--            ,[CZ_Rez1_Track]
--            ,[CZ_Rez2_Track]
--            ,[CZ_Rez3_Track]
--            ,[CZ_Rez4_Track]
--            ,[REZ1]
--            ,[REZ2]
--            ,[REZ3]
--            ,[REZ4]
--            ,[ODB_ID]
--            ,[mena_ID]
--            ,[SERLTNUM]
--            ,[WEIGHT]
--            ,[TIMEFROM]
--            ,[TIMETO]
--            ,[LSTMod]
--            ,[loginid]
--            ,[CZ_Expirace_Track]
--            ,[EXPIRACE])
-- 		   SELECT 
-- 		   Left(SKz.ID,40) as ITEMNMBR,
-- 		   Left(SKz.Nazev, 100) as ITEMDESC,
-- 		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 		   Left(SKz.EAN, 60) as VNDITNUM,
-- 		   '' as CZ_CarKod,
-- 		   '' as LOCNCODE,  --TODO složizejší
-- 		   SKz.RefSklad as SKL_ID,
-- 		   ISNULL(SKz.StavZ, 0) as QTY,
-- 		   0 as QTYPACK,
-- 		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
-- 		   '' as DMJ,
-- 		   0 as TAXRATE,
--            0 as PRICE0,
--            0 as PRICE1,
--            0 as PRICE2,
--            0 as PRICE3,
--            0 as PRICE4,
--            0 as PRICE5,
-- 		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 		   0 as CZ_SerNum_Delka,
--            0 as CZ_Rez1_Track,
--            0 as CZ_Rez2_Track, 
--            0 as CZ_Rez3_Track, 
--            0 as CZ_Rez4_Track, 
--            0 as REZ1,
--            0 as REZ2,
--            0 as REZ3,
--            0 as REZ4,
-- 		   Left(SKz.RefAD, 12) as ODB_ID,
-- 		   '' as mena_ID,
--            '' as SERLTNUM,
--            null as WEIGHT,
--            null as TIMEFROM,
--            null as TIMETO,
--            GETDATE() as LSTMod,
--            '' as loginid,
--            --'' as CZ_Expirace_Track,
-- 		   -- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
--            null as EXPIRACE
-- 		   FROM StwPh_63489040_2025.dbo.SKz 
-- 		   INNER JOIN StwPh_63489040_2025.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
-- 		   WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))
-- 
-- 		IF @EXZas_DotahovatAlternativniDodavatele = 1
-- 			BEGIN --C1
-- 
-- 				--SKzAlternatives_FillBy_DAD
-- 				INSERT INTO [dbo].[FASK_ZASOBY]
-- 				([ITEMNMBR]
-- 				,[ITEMDESC]
-- 				,[ITEMCODE]
-- 				,[VNDITNUM]
-- 				,[CZ_CarKod]
-- 				,[LOCNCODE]
-- 				,[SKL_ID]
-- 				,[QTY]
-- 				,[QTYPACK]
-- 				,[MJ]
-- 				,[DMJ]
-- 				,[TAXRATE]
-- 				,[PRICE0]
-- 				,[PRICE1]
-- 				,[PRICE2]
-- 				,[PRICE3]
-- 				,[PRICE4]
-- 				,[PRICE5]
-- 				,[CZ_SerNum_Track]
-- 				,[CZ_SerNum_Delka]
-- 				,[CZ_Rez1_Track]
-- 				,[CZ_Rez2_Track]
-- 				,[CZ_Rez3_Track]
-- 				,[CZ_Rez4_Track]
-- 				,[REZ1]
-- 				,[REZ2]
-- 				,[REZ3]
-- 				,[REZ4]
-- 				,[ODB_ID]
-- 				,[mena_ID]
-- 				,[SERLTNUM]
-- 				,[WEIGHT]
-- 				,[TIMEFROM]
-- 				,[TIMETO]
-- 				,[LSTMod]
-- 				,[loginid]
-- 				,[CZ_Expirace_Track]
-- 				,[EXPIRACE])
-- 				SELECT 
-- 				Left(SKz.ID,40) as ITEMNMBR,
-- 				Left(SKz.Nazev, 100) as ITEMDESC,
-- 				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 				Left(SKzNC.EAN, 60) as VNDITNUM,
-- 				'' as CZ_CarKod,
-- 				'' as LOCNCODE,  --TODO složizejší
-- 				SKz.RefSklad as SKL_ID,
-- 				ISNULL(SKz.StavZ, 0) as QTY,
-- 				0 as QTYPACK,
-- 				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
-- 				'' as DMJ,
-- 				0 as TAXRATE,
-- 				0 as PRICE0,
-- 				0 as PRICE1,
-- 				0 as PRICE2,
-- 				0 as PRICE3,
-- 				0 as PRICE4,
-- 				0 as PRICE5,
-- 				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 				0 as CZ_SerNum_Delka,
-- 				0 as CZ_Rez1_Track,
-- 				0 as CZ_Rez2_Track, 
-- 				0 as CZ_Rez3_Track, 
-- 				0 as CZ_Rez4_Track, 
-- 				0 as REZ1,
-- 				0 as REZ2,
-- 				0 as REZ3,
-- 				0 as REZ4,
-- 				Left(SKz.RefAD, 12) as ODB_ID,
-- 				'' as mena_ID,
-- 				'' as SERLTNUM,
-- 				null as WEIGHT,
-- 				null as TIMEFROM,
-- 				null as TIMETO,
-- 				GETDATE() as LSTMod,
-- 				'' as loginid,
-- 				--'' as CZ_Expirace_Track,
-- 				-- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
-- 				null as EXPIRACE
-- 		FROM StwPh_63489040_2025.dbo.SKz as SKz 
-- 		INNER JOIN StwPh_63489040_2025.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
-- 		INNER JOIN StwPh_63489040_2025.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
-- 		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))
-- 
-- 			END--C1
-- 
-- 	END --B1
-- 
-- END--A0
-- ELSE
-- BEGIN -- A1
-- 	IF @ExportovatPouzeAktivniPolozky = 1
-- 	BEGIN
-- 
-- 		--SKz_GetDataByAktivniPolozky
-- 		INSERT INTO [dbo].[FASK_ZASOBY]
--            ([ITEMNMBR]
--            ,[ITEMDESC]
--            ,[ITEMCODE]
--            ,[VNDITNUM]
--            ,[CZ_CarKod]
--            ,[LOCNCODE]
--            ,[SKL_ID]
--            ,[QTY]
--            ,[QTYPACK]
--            ,[MJ]
--            ,[DMJ]
--            ,[TAXRATE]
--            ,[PRICE0]
--            ,[PRICE1]
--            ,[PRICE2]
--            ,[PRICE3]
--            ,[PRICE4]
--            ,[PRICE5]
--            ,[CZ_SerNum_Track]
--            ,[CZ_SerNum_Delka]
--            ,[CZ_Rez1_Track]
--            ,[CZ_Rez2_Track]
--            ,[CZ_Rez3_Track]
--            ,[CZ_Rez4_Track]
--            ,[REZ1]
--            ,[REZ2]
--            ,[REZ3]
--            ,[REZ4]
--            ,[ODB_ID]
--            ,[mena_ID]
--            ,[SERLTNUM]
--            ,[WEIGHT]
--            ,[TIMEFROM]
--            ,[TIMETO]
--            ,[LSTMod]
--            ,[loginid]
--            ,[CZ_Expirace_Track]
--            ,[EXPIRACE])
-- 		   SELECT 
-- 		   Left(SKz.ID,40) as ITEMNMBR,
-- 		   Left(SKz.Nazev, 100) as ITEMDESC,
-- 		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 		   Left(SKz.EAN, 60) as VNDITNUM,
-- 		   '' as CZ_CarKod,
-- 		   '' as LOCNCODE,  --TODO složizejší
-- 		   SKz.RefSklad as SKL_ID,
-- 		   ISNULL(SKz.StavZ, 0) as QTY,
-- 		   0 as QTYPACK,
-- 		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
-- 		   '' as DMJ,
-- 		   0 as TAXRATE,
--            0 as PRICE0,
--            0 as PRICE1,
--            0 as PRICE2,
--            0 as PRICE3,
--            0 as PRICE4,
--            0 as PRICE5,
-- 		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 		   0 as CZ_SerNum_Delka,
--            0 as CZ_Rez1_Track,
--            0 as CZ_Rez2_Track, 
--            0 as CZ_Rez3_Track, 
--            0 as CZ_Rez4_Track, 
--            0 as REZ1,
--            0 as REZ2,
--            0 as REZ3,
--            0 as REZ4,
-- 		   Left(SKz.RefAD, 12) as ODB_ID,
-- 		   '' as mena_ID,
--            '' as SERLTNUM,
--            null as WEIGHT,
--            null as TIMEFROM,
--            null as TIMETO,
--            GETDATE() as LSTMod,
--            '' as loginid,
--            --'' as CZ_Expirace_Track,
-- 		   -- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
--            null as EXPIRACE
-- 	FROM StwPh_63489040_2025.dbo.SKz as SKz WHERE (SKz.Odbyt <> 0)
-- 
-- 		IF @EXZas_DotahovatAlternativniDodavatele = 1
-- 			BEGIN --C1
-- 
-- 				--SKzAlternatives_FillBy_DAD
-- 				INSERT INTO [dbo].[FASK_ZASOBY]
-- 				([ITEMNMBR]
-- 				,[ITEMDESC]
-- 				,[ITEMCODE]
-- 				,[VNDITNUM]
-- 				,[CZ_CarKod]
-- 				,[LOCNCODE]
-- 				,[SKL_ID]
-- 				,[QTY]
-- 				,[QTYPACK]
-- 				,[MJ]
-- 				,[DMJ]
-- 				,[TAXRATE]
-- 				,[PRICE0]
-- 				,[PRICE1]
-- 				,[PRICE2]
-- 				,[PRICE3]
-- 				,[PRICE4]
-- 				,[PRICE5]
-- 				,[CZ_SerNum_Track]
-- 				,[CZ_SerNum_Delka]
-- 				,[CZ_Rez1_Track]
-- 				,[CZ_Rez2_Track]
-- 				,[CZ_Rez3_Track]
-- 				,[CZ_Rez4_Track]
-- 				,[REZ1]
-- 				,[REZ2]
-- 				,[REZ3]
-- 				,[REZ4]
-- 				,[ODB_ID]
-- 				,[mena_ID]
-- 				,[SERLTNUM]
-- 				,[WEIGHT]
-- 				,[TIMEFROM]
-- 				,[TIMETO]
-- 				,[LSTMod]
-- 				,[loginid]
-- 				,[CZ_Expirace_Track]
-- 				,[EXPIRACE])
-- 				SELECT 
-- 				Left(SKz.ID,40) as ITEMNMBR,
-- 				Left(SKz.Nazev, 100) as ITEMDESC,
-- 				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 				Left(SKzNC.EAN, 60) as VNDITNUM,
-- 				'' as CZ_CarKod,
-- 				'' as LOCNCODE,  --TODO složizejší
-- 				SKz.RefSklad as SKL_ID,
-- 				ISNULL(SKz.StavZ, 0) as QTY,
-- 				0 as QTYPACK,
-- 				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
-- 				'' as DMJ,
-- 				0 as TAXRATE,
-- 				0 as PRICE0,
-- 				0 as PRICE1,
-- 				0 as PRICE2,
-- 				0 as PRICE3,
-- 				0 as PRICE4,
-- 				0 as PRICE5,
-- 				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 				0 as CZ_SerNum_Delka,
-- 				0 as CZ_Rez1_Track,
-- 				0 as CZ_Rez2_Track, 
-- 				0 as CZ_Rez3_Track, 
-- 				0 as CZ_Rez4_Track, 
-- 				0 as REZ1,
-- 				0 as REZ2,
-- 				0 as REZ3,
-- 				0 as REZ4,
-- 				Left(SKz.RefAD, 12) as ODB_ID,
-- 				'' as mena_ID,
-- 				'' as SERLTNUM,
-- 				null as WEIGHT,
-- 				null as TIMEFROM,
-- 				null as TIMETO,
-- 				GETDATE() as LSTMod,
-- 				'' as loginid,
-- 				--'' as CZ_Expirace_Track,
-- 				-- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
-- 				null as EXPIRACE
-- 			FROM StwPh_63489040_2025.SKz as SKz INNER JOIN StwPh_63489040_2025.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg WHERE (SKz.Odbyt <> 0)
-- 
-- 			END--C1
-- 
-- 	END
-- 	ELSE
-- 	BEGIN
-- 	
-- 		--SKz_GetDataByOptimalize
-- 		INSERT INTO [dbo].[FASK_ZASOBY]
--            ([ITEMNMBR]
--            ,[ITEMDESC]
--            ,[ITEMCODE]
--            ,[VNDITNUM]
--            ,[CZ_CarKod]
--            ,[LOCNCODE]
--            ,[SKL_ID]
--            ,[QTY]
--            ,[QTYPACK]
--            ,[MJ]
--            ,[DMJ]
--            ,[TAXRATE]
--            ,[PRICE0]
--            ,[PRICE1]
--            ,[PRICE2]
--            ,[PRICE3]
--            ,[PRICE4]
--            ,[PRICE5]
--            ,[CZ_SerNum_Track]
--            ,[CZ_SerNum_Delka]
--            ,[CZ_Rez1_Track]
--            ,[CZ_Rez2_Track]
--            ,[CZ_Rez3_Track]
--            ,[CZ_Rez4_Track]
--            ,[REZ1]
--            ,[REZ2]
--            ,[REZ3]
--            ,[REZ4]
--            ,[ODB_ID]
--            ,[mena_ID]
--            ,[SERLTNUM]
--            ,[WEIGHT]
--            ,[TIMEFROM]
--            ,[TIMETO]
--            ,[LSTMod]
--            ,[loginid]
--            ,[CZ_Expirace_Track]
--            ,[EXPIRACE])
-- 		   SELECT 
-- 		   Left(SKz.ID,40) as ITEMNMBR,
-- 		   Left(SKz.Nazev, 100) as ITEMDESC,
-- 		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 		   Left(SKz.EAN, 60) as VNDITNUM,
-- 		   '' as CZ_CarKod,
-- 		   '' as LOCNCODE,  --TODO složizejší
-- 		   SKz.RefSklad as SKL_ID,
-- 		   ISNULL(SKz.StavZ, 0) as QTY,
-- 		   0 as QTYPACK,
-- 		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
-- 		   '' as DMJ,
-- 		   0 as TAXRATE,
--            0 as PRICE0,
--            0 as PRICE1,
--            0 as PRICE2,
--            0 as PRICE3,
--            0 as PRICE4,
--            0 as PRICE5,
-- 		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 		   0 as CZ_SerNum_Delka,
--            0 as CZ_Rez1_Track,
--            0 as CZ_Rez2_Track, 
--            0 as CZ_Rez3_Track, 
--            0 as CZ_Rez4_Track, 
--            0 as REZ1,
--            0 as REZ2,
--            0 as REZ3,
--            0 as REZ4,
-- 		   Left(SKz.RefAD, 12) as ODB_ID,
-- 		   '' as mena_ID,
--            '' as SERLTNUM,
--            null as WEIGHT,
--            null as TIMEFROM,
--            null as TIMETO,
--            GETDATE() as LSTMod,
--            '' as loginid,
--            --'' as CZ_Expirace_Track,
-- 		   -- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
--            null as EXPIRACE
-- 		   FROM StwPh_63489040_2025.dbo.SKz as SKz  
-- 
-- 		IF @EXZas_DotahovatAlternativniDodavatele = 1
-- 			BEGIN --C1
-- 
-- 				--SKzAlternatives_FillBy_DAD
-- 				INSERT INTO [dbo].[FASK_ZASOBY]
-- 				([ITEMNMBR]
-- 				,[ITEMDESC]
-- 				,[ITEMCODE]
-- 				,[VNDITNUM]
-- 				,[CZ_CarKod]
-- 				,[LOCNCODE]
-- 				,[SKL_ID]
-- 				,[QTY]
-- 				,[QTYPACK]
-- 				,[MJ]
-- 				,[DMJ]
-- 				,[TAXRATE]
-- 				,[PRICE0]
-- 				,[PRICE1]
-- 				,[PRICE2]
-- 				,[PRICE3]
-- 				,[PRICE4]
-- 				,[PRICE5]
-- 				,[CZ_SerNum_Track]
-- 				,[CZ_SerNum_Delka]
-- 				,[CZ_Rez1_Track]
-- 				,[CZ_Rez2_Track]
-- 				,[CZ_Rez3_Track]
-- 				,[CZ_Rez4_Track]
-- 				,[REZ1]
-- 				,[REZ2]
-- 				,[REZ3]
-- 				,[REZ4]
-- 				,[ODB_ID]
-- 				,[mena_ID]
-- 				,[SERLTNUM]
-- 				,[WEIGHT]
-- 				,[TIMEFROM]
-- 				,[TIMETO]
-- 				,[LSTMod]
-- 				,[loginid]
-- 				,[CZ_Expirace_Track]
-- 				,[EXPIRACE])
-- 				SELECT 
-- 				Left(SKz.ID,40) as ITEMNMBR,
-- 				Left(SKz.Nazev, 100) as ITEMDESC,
-- 				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
-- 				Left(SKzNC.EAN, 60) as VNDITNUM,
-- 				'' as CZ_CarKod,
-- 				'' as LOCNCODE,  --TODO složizejší
-- 				SKz.RefSklad as SKL_ID,
-- 				ISNULL(SKz.StavZ, 0) as QTY,
-- 				0 as QTYPACK,
-- 				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
-- 				'' as DMJ,
-- 				0 as TAXRATE,
-- 				0 as PRICE0,
-- 				0 as PRICE1,
-- 				0 as PRICE2,
-- 				0 as PRICE3,
-- 				0 as PRICE4,
-- 				0 as PRICE5,
-- 				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
-- 				0 as CZ_SerNum_Delka,
-- 				0 as CZ_Rez1_Track,
-- 				0 as CZ_Rez2_Track, 
-- 				0 as CZ_Rez3_Track, 
-- 				0 as CZ_Rez4_Track, 
-- 				0 as REZ1,
-- 				0 as REZ2,
-- 				0 as REZ3,
-- 				0 as REZ4,
-- 				Left(SKz.RefAD, 12) as ODB_ID,
-- 				'' as mena_ID,
-- 				'' as SERLTNUM,
-- 				null as WEIGHT,
-- 				null as TIMEFROM,
-- 				null as TIMETO,
-- 				GETDATE() as LSTMod,
-- 				'' as loginid,
-- 				--'' as CZ_Expirace_Track,
-- 				-- SKz.VPrCZExpTrackIS as CZ_Expirace_Track,
-- 			   @TrackExp as CZ_Expirace_Track,
-- 				null as EXPIRACE
-- 			FROM StwPh_63489040_2025.dbo.SKz as SKz INNER JOIN StwPh_63489040_2025.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg
-- 
-- 			END--C1
-- 
-- 
-- 	END
-- END --A1
-- 
-- 
-- SELECT Count(*) from FASK_ZASOBY
-- 
-- END

-- -- =============================================
--  -- Author:		Ing. Rathouzský Matouš
--  -- Create date: 3.11.2025
--  -- Description:	Procedura pro dotažení vazeb z IS pohoda do FASK
--  -- =============================================
-- create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_VazbaMat]
-- 	@TypyVyrobku [nvarchar](max),
-- 	@ListVyrobku [nvarchar](max),
-- 	@TypyMaterialu [nvarchar](max)
-- WITH EXECUTE AS CALLER
-- AS
-- BEGIN
--  	SET NOCOUNT ON;	
--  
--  DELETE FROM FASK_Vyroba_TP;
-- 
--  		declare @cnt int;
--  
--  	select @cnt = COUNT(*) from FASK_Vyroba_TP;
--  
--  	if @cnt > 0
--  		BEGIN
--  			PRINT 'Tabulka [FASK_Vyroba_TP] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z IS POHODA nebudou přidány.'
--  			return -1;
--  		END
--  
--  
--  DECLARE @Polozky TABLE
--  (
--      ID INT PRIMARY KEY IDENTITY,
--      Klic int,
--  	ITEMDESC nvarchar(100),
--  	MJ nvarchar(50)
--  
--  );
--  
--  --Deklarace promennych
--  DECLARE @sp_count INT,
--          @count INT = 0,
--  		@ITEMNMBR nvarchar(31) = 0,
--  		@ITEMDESC nvarchar(100) = 0,
--  		@MJ nvarchar(50) = 0,
--  		@ID_L_Max nvarchar(50) = '99';
--  
--  
--  IF @ListVyrobku = ''
--  	BEGIN
--  		INSERT  @Polozky
--  		SELECT
--  			S.ID as Klic,
--  			S.Nazev as ITEMDESC,
--  			S.MJ as MJ
--  		FROM  StwPh_63489040_2025.dbo.SKz as S
--  		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
--  	END
--  ELSE
--  	BEGIN
--  		INSERT  @Polozky
--  		SELECT
--  			S.ID as Klic,
--  			S.Nazev as ITEMDESC,
--  			S.MJ as MJ
--  		FROM  StwPh_63489040_2025.dbo.SKz as S
--  		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
--  		AND S.ID in (SELECT Name FROM dbo.splitstring(@ListVyrobku, default))
--  	END
--  
--  
--  SET @sp_count = (SELECT COUNT(1) FROM @Polozky)
--  
--  --Cyklus přes všechny
--  	WHILE (@sp_count > @count)
--  	BEGIN
--  		SET @count = @count + 1;
--  
--  		SET @ITEMNMBR = (SELECT Klic
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @ITEMDESC = (SELECT ITEMDESC
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @MJ = (SELECT MJ
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @ID_L_Max = (SELECT 
--  						ISNULL(MAX(ID_L), '99') 
--  						FROM FASK_Vyroba_TP)
--  
--  		SET @ID_L_Max = Convert(nvarchar(50), CONVERT(int, @ID_L_Max) + 1);
--  
--  	INSERT INTO [dbo].[FASK_Vyroba_TP]
--  			   ([ID_H]
--  			   ,[ID_L]
--  			   ,[ITEMNMBR_Def]
--  			   ,[DESC_Def]
--  			   ,[MJ_Def]
--  			   ,[ITEMNMBR_fol]
--  			   ,[DESC_Fol]
--  			   ,[MJ_Fol]
--  			   ,[koef]
--  			   ,[ID_USER]
--  			   ,[dateedit]
--  			   ,[alter]
--  			   ,[PUO])
--  			   SELECT 
--  			   NULL as [ID_H],
--  			   @ID_L_Max as [ID_L],
--  			   @ITEMNMBR as [ITEMNMBR_Def],
--  			   @ITEMDESC as [DESC_Def],
--  			   @MJ as [MJ_Def],
--  			   NULL as [ITEMNMBR_Fol],
--  			   NULL as [DESC_Fol],
--  			   NULL as [MJ_Fol],
--  			   NULL as [koef],
--  			   99 as [ID_USER],
--  			   GETDATE() as [dateedit],
--  			   0 as [alter],
--  			   1 as [PUO]
--  
--  EXECUTE [FASK_proc_EXPORT_SQL_FASK_VazbaMatPln] 
--     @ITEMNMBR
--    ,@ITEMDESC
--    ,@MJ
--    ,@ID_L_Max
--    ,@TypyMaterialu
--  
--  
--  	END
--  
--  END
--  /****************************************************************************/
--  /****** Object:  Table [dbo].[Production_SN]   ******/
--  SET ANSI_NULLS ON

-- -- =============================================
--  -- Author:		Ing. Rathouzský Matouš
--  -- Create date: 3.11.2025
--  -- Description:	Procedura pro dotažení vazeb z IS pohoda do FASK
--  -- =============================================
--  create PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_VazbaMatPln] 
--  @ITEMNMBR nvarchar(31) = null,
--  @ITEMDESC nvarchar(100) = null,
--  @MJ nvarchar(50) = null,
--  @ID_H int,
--  @TypyMaterialu nvarchar(MAX)
--  AS
--  BEGIN
--  	SET NOCOUNT ON;	
--  
--  	DECLARE @PolozkyMat TABLE
--  	(
--  		ID INT PRIMARY KEY IDENTITY,
--  		Klic int,
--  		ITEMDESC nvarchar(100),
--  		MJ nvarchar(50),
--  		QTY numeric(19,5)
--  	);
--  
--  	DECLARE @AllCount INT,
--  			@Inkrement INT = 0,
--  			@ITEMNMBR_pol nvarchar(31) = 0,
--  			@ITEMDESC_pol nvarchar(100) = 0,
--  			@MJ_pol nvarchar(50) = 0,
--  			@QTY numeric(19,5),
--  			@ID_L_Max int;
--  
--  	INSERT  @PolozkyMat
--  	SELECT
--  		S.ID as Klic,
--  		S.Nazev as ITEMDESC,
--  		S.MJ as MJ,
--  		P.Mnozstvi as QTY
--  	FROM StwPh_63489040_2025.dbo.SKzPol as P
--  	left join StwPh_63489040_2025.dbo.SKz as S ON S.ID = P.RefSKz
--  	where P.RefAg = @ITEMNMBR AND 
--  	S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyMaterialu, default))
--  
--  
--  	SET @AllCount = (SELECT COUNT(1) FROM @PolozkyMat)
--  
--  		WHILE (@AllCount > @Inkrement)
--  		BEGIN
--  
--  			SET @Inkrement = @Inkrement + 1;
--  
--  				SET @ITEMNMBR_pol = (SELECT Klic
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @ITEMDESC_pol = (SELECT ITEMDESC
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @MJ_pol = (SELECT MJ
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @QTY = (SELECT QTY
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  		SET @ID_L_Max = (SELECT MAX(ID_L) FROM FASK_Vyroba_TP)
--  
--  		SET @ID_L_Max = @ID_L_Max + 1;
--  
--  			INSERT INTO [dbo].[FASK_Vyroba_TP]
--  		([ID_H]
--  		,[ID_L]
--  		,[ITEMNMBR_Def]
--  		,[DESC_Def]
--  		,[MJ_Def]
--  		,[ITEMNMBR_fol]
--  		,[DESC_Fol]
--  		,[MJ_Fol]
--  		,[koef]
--  		,[ID_USER]
--  		,[dateedit]
--  		,[alter]
--  		,[PUO])
--  		SELECT 
--  		@ID_H as [ID_H],
--  		@ID_L_Max as [ID_L],
--  		@ITEMNMBR as [ITEMNMBR_Def],
--  		@ITEMDESC as [DESC_Def],
--  		@MJ as [MJ_Def],
--  		@ITEMNMBR_pol as [ITEMNMBR_Fol],
--  		@ITEMDESC_pol as [DESC_Fol],
--  		@MJ_pol as [MJ_Fol],
--  		@QTY as [koef],
--  		99 as [ID_USER],
--  		GETDATE() as [dateedit],
--  		0 as [alter],
--  		0 as [PUO]
--  
--  		END		
--  END
--  
--  
--  /*****************************************************************************/
--  /****** Object:  StoredProcedure [dbo].[FASK_procPlnVyrobaTP]    ******/
--  SET ANSI_NULLS ON

-- -- =============================================
--  -- Author:		Tadeas Divacky
--  -- Create date: 26.10.2020
--  -- Description:	Procedura pro dotažení vazev z IS pohoda do FASK
--  -- =============================================
--  CREATE PROCEDURE [dbo].[FASK_proc_Insert_VyrobaTP] 
--  @ITEMNMBR nvarchar(31) = null,
--  @ITEMDESC nvarchar(100) = null,
--  @MJ nvarchar(50) = null,
--  @ID_H int,
--  @TypyMaterialu nvarchar(MAX)
--  AS
--  BEGIN
--  	SET NOCOUNT ON;	
--  
--  	DECLARE @PolozkyMat TABLE
--  	(
--  		ID INT PRIMARY KEY IDENTITY,
--  		Klic int,
--  		ITEMDESC nvarchar(100),
--  		MJ nvarchar(50),
--  		QTY numeric(19,5)
--  	);
--  
--  	DECLARE @AllCount INT,
--  			@Inkrement INT = 0,
--  			@ITEMNMBR_pol nvarchar(31) = 0,
--  			@ITEMDESC_pol nvarchar(100) = 0,
--  			@MJ_pol nvarchar(50) = 0,
--  			@QTY numeric(19,5),
--  			@ID_L_Max int;
--  
--  	INSERT  @PolozkyMat
--  	SELECT
--  		S.ID as Klic,
--  		S.Nazev as ITEMDESC,
--  		S.MJ as MJ,
--  		P.Mnozstvi as QTY
--  	FROM StwPh_63489040_2025.dbo.SKzPol as P
--  	left join StwPh_63489040_2025.dbo.SKz as S ON S.ID = P.RefSKz
--  	where P.RefAg = @ITEMNMBR AND 
--  	S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyMaterialu, default))
--  
--  
--  	SET @AllCount = (SELECT COUNT(1) FROM @PolozkyMat)
--  
--  		WHILE (@AllCount > @Inkrement)
--  		BEGIN
--  
--  			SET @Inkrement = @Inkrement + 1;
--  
--  				SET @ITEMNMBR_pol = (SELECT Klic
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @ITEMDESC_pol = (SELECT ITEMDESC
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @MJ_pol = (SELECT MJ
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  			SET @QTY = (SELECT QTY
--  							FROM    @PolozkyMat
--  							WHERE   ID = @Inkrement);
--  
--  		SET @ID_L_Max = (SELECT MAX(ID_L) FROM FASK_Vyroba_TP)
--  
--  		SET @ID_L_Max = @ID_L_Max + 1;
--  
--  			INSERT INTO [dbo].[FASK_Vyroba_TP]
--  		([ID_H]
--  		,[ID_L]
--  		,[ITEMNMBR_Def]
--  		,[DESC_Def]
--  		,[MJ_Def]
--  		,[ITEMNMBR_fol]
--  		,[DESC_Fol]
--  		,[MJ_Fol]
--  		,[koef]
--  		,[ID_USER]
--  		,[dateedit]
--  		,[alter]
--  		,[PUO])
--  		SELECT 
--  		@ID_H as [ID_H],
--  		@ID_L_Max as [ID_L],
--  		@ITEMNMBR as [ITEMNMBR_Def],
--  		@ITEMDESC as [DESC_Def],
--  		@MJ as [MJ_Def],
--  		@ITEMNMBR_pol as [ITEMNMBR_Fol],
--  		@ITEMDESC_pol as [DESC_Fol],
--  		@MJ_pol as [MJ_Fol],
--  		@QTY as [koef],
--  		99 as [ID_USER],
--  		GETDATE() as [dateedit],
--  		0 as [alter],
--  		0 as [PUO]
--  
--  		END		
--  END
--  
--  
--  /*****************************************************************************/
--  /****** Object:  StoredProcedure [dbo].[FASK_procPlnVyrobaTP]    ******/
--  SET ANSI_NULLS ON

-- CREATE PROCEDURE [dbo].[FASK_procGetAdresa]
--  	-- Add the parameters for the stored procedure here
--  	@SOPNUMBE nvarchar(32)
--  AS
--  BEGIN
--  	-- SET NOCOUNT ON added to prevent extra result sets from
--  	-- interfering with SELECT statements.
--  	--SET NOCOUNT ON;
--  
--  	--Nastavuje se v Web.Config na serveru
--      -- Insert statements for procedure here
--  		select Firma, Firma2, Utvar, Utvar2, Jmeno, Jmeno2, Ulice, Ulice2, PSC, PSC2, Obec, Obec2, ICO, DIC
--  from StwPh_63489040_2025.dbo.OBJ
--  		where
--  		Cislo = @SOPNUMBE
--  END

-- CREATE PROCEDURE [dbo].[FASK_procGetAdresa_SQL]
--  	-- Add the parameters for the stored procedure here
--  	@SOPNUMBE nvarchar(32) = null,
-- 	@ITEMTYPE nvarchar(10) = null
--  AS
--  BEGIN
--  	-- SET NOCOUNT ON added to prevent extra result sets from
--  	-- interfering with SELECT statements.
--  	--SET NOCOUNT ON;
--  
--  	--Nastavuje se v Web.Config na serveru
--      -- Insert statements for procedure here
--  		select Firma, Firma2, Utvar, Utvar2, Jmeno, Jmeno2, Ulice, Ulice2, PSC, PSC2, Obec, Obec2, ICO, DIC
--  from StwPh_63489040_2025.dbo.OBJ
--  		where
--  		Cislo = @SOPNUMBE
--  END

-- CREATE PROCEDURE [dbo].[FASK_procGetpolozka]
--  	-- Add the parameters for the stored procedure here
--  	@ITEMNMBR varchar(31)
--  AS
--  BEGIN
--  	-- SET NOCOUNT ON added to prevent extra result sets from
--  	-- interfering with SELECT statements.
--  	--SET NOCOUNT ON;
--  
--  	--Nastavuje se v Web.Config na serveru parameter VydejkaDetailPolozka
--      -- Insert statements for procedure here
--  		select Doprava
--  from StwPh_63489040_2025.dbo.SKz
--  		where
--  		ID = @ITEMNMBR
--  END

-- CREATE PROCEDURE [dbo].[FASK_procPlnVyrobaTP]
-- 	@TypyVyrobku [nvarchar](max),
-- 	@ListVyrobku [nvarchar](max),
-- 	@TypyMaterialu [nvarchar](max)
-- WITH EXECUTE AS CALLER
-- AS
-- BEGIN
--  	SET NOCOUNT ON;	
--  
--  DELETE FROM FASK_Vyroba_TP;
-- 
--  		declare @cnt int;
--  
--  	select @cnt = COUNT(*) from FASK_Vyroba_TP;
--  
--  	if @cnt > 0
--  		BEGIN
--  			PRINT 'Tabulka [FASK_Vyroba_TP] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z IS POHODA nebudou přidány.'
--  			return -1;
--  		END
--  
--  
--  DECLARE @Polozky TABLE
--  (
--      ID INT PRIMARY KEY IDENTITY,
--      Klic int,
--  	ITEMDESC nvarchar(100),
--  	MJ nvarchar(50)
--  
--  );
--  
--  --Deklarace promennych
--  DECLARE @sp_count INT,
--          @count INT = 0,
--  		@ITEMNMBR nvarchar(31) = 0,
--  		@ITEMDESC nvarchar(100) = 0,
--  		@MJ nvarchar(50) = 0,
--  		@ID_L_Max nvarchar(50) = '99';
--  
--  
--  IF @ListVyrobku = ''
--  	BEGIN
--  		INSERT  @Polozky
--  		SELECT
--  			S.ID as Klic,
--  			S.Nazev as ITEMDESC,
--  			S.MJ as MJ
--  		FROM  StwPh_63489040_2025.dbo.SKz as S
--  		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
--  	END
--  ELSE
--  	BEGIN
--  		INSERT  @Polozky
--  		SELECT
--  			S.ID as Klic,
--  			S.Nazev as ITEMDESC,
--  			S.MJ as MJ
--  		FROM  StwPh_63489040_2025.dbo.SKz as S
--  		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
--  		AND S.ID in (SELECT Name FROM dbo.splitstring(@ListVyrobku, default))
--  	END
--  
--  
--  SET @sp_count = (SELECT COUNT(1) FROM @Polozky)
--  
--  --Cyklus přes všechny
--  	WHILE (@sp_count > @count)
--  	BEGIN
--  		SET @count = @count + 1;
--  
--  		SET @ITEMNMBR = (SELECT Klic
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @ITEMDESC = (SELECT ITEMDESC
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @MJ = (SELECT MJ
--  						FROM    @Polozky
--  						WHERE   ID = @count);
--  
--  		SET @ID_L_Max = (SELECT 
--  						ISNULL(MAX(ID_L), '99') 
--  						FROM FASK_Vyroba_TP)
--  
--  		SET @ID_L_Max = Convert(nvarchar(50), CONVERT(int, @ID_L_Max) + 1);
--  
--  	INSERT INTO [dbo].[FASK_Vyroba_TP]
--  			   ([ID_H]
--  			   ,[ID_L]
--  			   ,[ITEMNMBR_Def]
--  			   ,[DESC_Def]
--  			   ,[MJ_Def]
--  			   ,[ITEMNMBR_fol]
--  			   ,[DESC_Fol]
--  			   ,[MJ_Fol]
--  			   ,[koef]
--  			   ,[ID_USER]
--  			   ,[dateedit]
--  			   ,[alter]
--  			   ,[PUO])
--  			   SELECT 
--  			   NULL as [ID_H],
--  			   @ID_L_Max as [ID_L],
--  			   @ITEMNMBR as [ITEMNMBR_Def],
--  			   @ITEMDESC as [DESC_Def],
--  			   @MJ as [MJ_Def],
--  			   NULL as [ITEMNMBR_Fol],
--  			   NULL as [DESC_Fol],
--  			   NULL as [MJ_Fol],
--  			   NULL as [koef],
--  			   99 as [ID_USER],
--  			   GETDATE() as [dateedit],
--  			   0 as [alter],
--  			   1 as [PUO]
--  
--  EXECUTE [FASK_proc_Insert_VyrobaTP] 
--     @ITEMNMBR
--    ,@ITEMDESC
--    ,@MJ
--    ,@ID_L_Max
--    ,@TypyMaterialu
--  
--  
--  	END
--  
--  END
--  /****************************************************************************/
--  /****** Object:  Table [dbo].[Production_SN]   ******/
--  SET ANSI_NULLS ON
