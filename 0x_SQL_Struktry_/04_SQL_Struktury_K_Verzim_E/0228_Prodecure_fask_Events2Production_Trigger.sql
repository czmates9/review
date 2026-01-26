/****** Object:  StoredProcedure [dbo].[fask_Events2Production_Trigger]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
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
		PRINT 'VYBER SPATNE ZAZNAMY NE'
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
		,[QTYPACKMJ] -- vyjadreni prepoctovy koeficient, budu davat prazdny retezec -- ''
		,[description] --popis, budu davat prazdny retezec ''
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
		,'false'--,[ITEMTYPE] --typ polozky, davat prazdny retezec**
		,Left(ISNULL(@MJ, ''),5)--,[ITEMMJ] --merna jednotka polozky, davat prazdny retezec**
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
		,'' --,[QTYPACKMJ] -- vyjadreni prepoctovy koeficient, budu davat prazdny retezec -- ''
		,'' --,[description] --popis, budu davat prazdny retezec ''
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