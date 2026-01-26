USE [s4s-color-test]
GO

/****** Object:  StoredProcedure [dbo].[procProdejGetLokaci]    Script Date: 04.05.2021 11:46:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- procProdejGetLokaci: puvodne procProdejOverLokaci2
-- Vraci seznam (select) doporucenych lokaci, odkud je mozne brat material (nasledne je tento seznam zobrazen).
-- seznam SERIAL poskládaný od nejstaršího k nejnovìjšímu naskladnìní
-- 22.3.2021 JiS : rozsireni o polozku @locncode, kdy se chce vsechen material na lokaci => itemnmbr a serlnmbr nemusi byt nastaveno
-- =============================================
--ALTER 
--CREATE
--PROCEDURE [dbo].[procProdejGetLokaci]
declare
    @Itemnmbr NVarChar(31), -- cislo zvolene polozky (CZMST095.ITEMNMBR)
    @Skl_id NVarChar(20), -- ID vybraneho skladu (CZMST_DI.SKL_ID)
	@Serltnum NVarChar(21), -- sarze (CZMST095.SERLTNUM)
    @doc_id NVarChar(12), -- id dokladu (CZMST092.doc_id)
	@locncode nvarchar(20) -- zbozi pouze na pozadovane lokaci

--AS
BEGIN
	SET NOCOUNT ON;

	--set @Itemnmbr	= 'BaPe-P63941-09'
	set @Itemnmbr	= 'BaAl-343-87-90D3 schwarz'
	--set @Skl_id		= 'S4'	

	if ltrim(rtrim(@doc_id))   = '' set @doc_id = NULL
	if ltrim(rtrim(@Itemnmbr)) = '' set @Itemnmbr = NULL
	if ltrim(rtrim(@Serltnum)) = '' set @Serltnum = NULL
	if ltrim(rtrim(@skl_id))   = '' set @Skl_id = NULL
	if ltrim(rtrim(@locncode)) = '' set @locncode = NULL

	SELECT
		(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
		Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
		[CENIK].[NAZEV MAT] as [ITEMDESC],
		Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
		[SERIAL].[DAT ZARUKA] as [EXPIRACE],
		[SERIAL].[DAT PRIJEM] as [PRIJEM],
		Coalesce([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM], GetDate()) as [RAZENI],
		Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
		Left([LOKACE].[LOKACE ID], 20) AS [LOCNCODE],
		Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
		IsNull([SERIAL].[BLOK], 0) as [BLOKACE] --[BLOKOVANO]
	FROM
		[LOKACE] 
		LEFT JOIN [SERIAL] ON 1=1
			AND [LOKACE].[MAT ID] = [SERIAL].[MAT ID]
			AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
		LEFT JOIN [CENIK] ON 1=1
			AND [CENIK].[MAT ID] = [LOKACE].[MAT ID]
	WHERE 1=1
		AND isnull([LOKACE].[MAT ID], '')		= Coalesce(@Itemnmbr, isnull([LOKACE].[MAT ID], ''))
		AND isnull([LOKACE].[SERIAL ID], '')	= Coalesce(@Serltnum, isnull([LOKACE].[SERIAL ID], ''))
		AND isnull([LOKACE].[SKLAD ID], '')		= Coalesce(@Skl_id,   isnull([LOKACE].[SKLAD ID], ''))
		AND isnull([LOKACE].[LOKACE ID], '')	= Coalesce(@locncode, isnull([LOKACE].[LOKACE ID], ''))
		--AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
		AND
		(
			(
				1=1
				--and @doc_id IN ('preDoVyr', 'Vydej', 'vratka', 'zmenaLokace', 'zmnLokSTisk')
				AND [LOKACE].[MNOZSTVI] > 0
			)
			--OR @doc_id = 'vratka'
			--OR @doc_id = 'zmenaLokace'
			--OR @doc_id = 'zmnLokSTisk'
		)
		--AND [SERIAL].[BLOK] = 0
	ORDER BY
		--CASE [BLOK] WHEN 0 THEN 'A' ELSE 'Z' END ASC
		--,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
		[LOKACE].[MAT ID] ASC
		,[SERIAL].[DAT ZARUKA] ASC
		,[LOKACE].[SERIAL ID] ASC

END
GO


