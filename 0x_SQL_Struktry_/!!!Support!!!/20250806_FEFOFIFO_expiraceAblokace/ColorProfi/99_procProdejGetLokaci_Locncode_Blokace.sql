USE [s4s-color-test]
GO

/****** Object:  StoredProcedure [dbo].[procProdejGetLokaci]    Script Date: 05.05.2021 9:54:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- procProdejGetLokaci: puvodne procProdejOverLokaci2
-- Vraci seznam (select) doporucenych lokaci, odkud je mozne brat material (nasledne je tento seznam zobrazen).
-- seznam SERIAL poskládaný od nejstaršího k nejnovìjšímu naskladnìní
-- 28.4.2020 FASK:JiS rozsireni o prvek Expirace
-- =============================================
ALTER
--CREATE   
PROCEDURE [dbo].[procProdejGetLokaci]
    @Itemnmbr NVarChar(31), -- cislo zvolene polozky (CZMST095.ITEMNMBR)
    @Skl_id NVarChar(20), -- ID vybraneho skladu (CZMST_DI.SKL_ID)
	@Serltnum NVarChar(21), -- sarze (CZMST095.SERLTNUM)
    @doc_id NVarChar(12), -- id dokladu (CZMST092.doc_id)
	@locncode nvarchar(20) -- zbozi pouze na pozadovane lokaci

AS
BEGIN


	SET NOCOUNT ON;
	--preDoVyr
	--vratka


		if not Isnull(@Itemnmbr,'') = '' and not Isnull(@Serltnum,'') = '' begin

			SELECT
				(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
				Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
				Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
				[SERIAL].[DAT ZARUKA] as [EXPIRACE],
				[SERIAL].[DAT PRIJEM] as [PRIJEM],
				--IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) as [RAZENI],
				IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) as [RAZENI],
				Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
				Left([LOKACE].[LOKACE ID], 11) AS [LOCNCODE],
				Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
-- FASK BLOK 20201022
				[SERIAL].[BLOK] as [BLOKACE]
			FROM
				[LOKACE] INNER JOIN [SERIAL]
			ON
				[LOKACE].[MAT ID] = [SERIAL].[MAT ID]
				AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
			WHERE
				[LOKACE].[MAT ID] = @Itemnmbr
				AND [LOKACE].[SERIAL ID]= @Serltnum
				AND [LOKACE].[SKLAD ID] = @Skl_id
				--AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
				AND
				(
					(
						@doc_id IN ('preDoVyr', 'Vydej', 'vratka', 'zmenaLokace', 'zmnLokSTisk')
						AND [LOKACE].[MNOZSTVI] > 0
					)
					--OR @doc_id = 'vratka'
					--OR @doc_id = 'zmenaLokace'
					--OR @doc_id = 'zmnLokSTisk'
				)
-- FASK BLOK 20201022
				--AND [SERIAL].[BLOK] = 0
			ORDER BY
				CASE [BLOK] WHEN 0 THEN 'A' ELSE 'Z' END ASC
				--,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
				,IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) ASC
				,[LOKACE].[MAT ID] ASC
				,[LOKACE].[SERIAL ID] ASC
		end

		else if Isnull(@Itemnmbr,'') = '' begin
			SELECT
				(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
				Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
				Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
				[SERIAL].[DAT ZARUKA] as [EXPIRACE],
				[SERIAL].[DAT PRIJEM] as [PRIJEM],
				--IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) as [RAZENI],
				IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) as [RAZENI],
				Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
				Left([LOKACE].[LOKACE ID], 11) AS [LOCNCODE],
				Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
-- FASK BLOK 20201022
				[SERIAL].[BLOK] as [BLOKACE]
			FROM
				[LOKACE] INNER JOIN [SERIAL]
			ON
				[LOKACE].[MAT ID] = [SERIAL].[MAT ID]
				AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
			WHERE
				--[MAT ID] = @Itemnmbr
				[LOKACE].[SERIAL ID]= @Serltnum
				AND [LOKACE].[SKLAD ID] = @Skl_id
				--AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
				AND
				(
					(
						@doc_id IN ('preDoVyr', 'Vydej', 'vratka', 'zmenaLokace', 'zmnLokSTisk')
						AND [LOKACE].[MNOZSTVI] > 0
					)
					--OR @doc_id = 'vratka'
					--OR @doc_id = 'zmenaLokace'
					--OR @doc_id = 'zmnLokSTisk'
				)
-- FASK BLOK 20201022
				--AND [SERIAL].[BLOK] = 0
			ORDER BY
				CASE [BLOK] WHEN 0 THEN 'A' ELSE 'Z' END ASC
				--,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
				,IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) ASC
				,[LOKACE].[MAT ID] ASC
				,[LOKACE].[SERIAL ID] ASC


    	end

		else begin
			SELECT
				(ROW_NUMBER() OVER(ORDER BY [LOKACE].[DAT ZMENA] ASC, [LOKACE].[MAT ID] ASC, [LOKACE].[SERIAL ID] ASC)) AS [Index],
				Left([LOKACE].[MAT ID], 31) AS [ITEMNMBR],
				Left([LOKACE].[SERIAL ID], 21) AS [SERLTNUM],
				[SERIAL].[DAT ZARUKA] as [EXPIRACE],
				[SERIAL].[DAT PRIJEM] as [PRIJEM],
				--IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) as [RAZENI],
				IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) as [RAZENI],
				Cast([LOKACE].[MNOZSTVI] As numeric(19,5)) AS [QTYSHPPD],
				Left([LOKACE].[LOKACE ID], 11) AS [LOCNCODE],
				Left([LOKACE].[SKLAD ID], 20) AS [SKL_ID],
-- FASK BLOK 20201022
				[SERIAL].[BLOK] as [BLOKACE]
			FROM
				[LOKACE] INNER JOIN [SERIAL]
			ON
				[LOKACE].[MAT ID] = [SERIAL].[MAT ID]
				AND [LOKACE].[SERIAL ID] = [SERIAL].[SERIAL ID]
			WHERE
				[LOKACE].[MAT ID] = @Itemnmbr
				--AND [SERIAL ID]= @Serltnum
				AND [LOKACE].[SKLAD ID] = @Skl_id
				--AND [SERIAL].[DAT PRIJEM] > dateadd(month, -12, getdate()) --HD90001389
				AND
				(
					(
						@doc_id IN ('preDoVyr', 'Vydej', 'vratka', 'zmenaLokace', 'zmnLokSTisk')
						AND [LOKACE].[MNOZSTVI] > 0
					)
					--OR @doc_id = 'vratka'
					--OR @doc_id = 'zmenaLokace'
					--OR @doc_id = 'zmnLokSTisk'
				)
-- FASK BLOK 20201022
				--AND [SERIAL].[BLOK] = 0
			ORDER BY
				CASE [BLOK] WHEN 0 THEN 'A' ELSE 'Z' END ASC
				--,IsNull([SERIAL].[DAT ZARUKA], [SERIAL].[DAT PRIJEM]) ASC
				,IsNull((CASE WHEN [SERIAL].[DAT ZARUKA] < GetDate() THEN DateADD(YY,100,[SERIAL].[DAT ZARUKA]) ELSE [SERIAL].[DAT ZARUKA] END), [SERIAL].[DAT PRIJEM]) ASC
				,[LOKACE].[MAT ID] ASC
				,[LOKACE].[SERIAL ID] ASC

		end
END
GO


