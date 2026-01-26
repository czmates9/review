/****** Object:  StoredProcedure [dbo].[fask_Events2Production_Confirm]    *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
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

	IF OBJECT_ID('tempdb..#EventsTemp', 'U') IS NOT NULL
		DROP TABLE #EventsTemp;

	SELECT 
		GETDATE() [GeneratedDatetime]
		,dbo.fask_GS1_AI_GET('EAN', e.barcodeReaded) EAN
		,dbo.fask_GS1_AI_GET('SARZE', e.barcodeReaded) SARZE
		,dbo.fask_GS1_AI_GET('EXPIRACE', e.barcodeReaded) EXPIRACE
		,dbo.fask_GS1_AI_GET('VAHA', e.barcodeReaded) VAHA
		,* 
	INTO #EventsTemp
	from FASK_Events e 
	where 1=1
	and ((e.isProcessed is NULL) OR (e.productionGuid is null))
	and e.dateeve between ISNULL(@from, @datetimemin) and ISNULL(@to, @datetimemax)

	-- priprava dat k vlozeni do production
	IF OBJECT_ID('tempdb..#Procution2Insert', 'U') IS NOT NULL
		DROP TABLE #Procution2Insert;

	select 
		et.EAN
		, et.SARZE
		, et.EXPIRACE
		, et.material
		, count(*) pocetPolozek
		, sum(convert(numeric(18,5),isnull(et.VAHA, '0')) / 100) vaha
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

END

GO
/**************************************************************************************/