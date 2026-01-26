/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bc. Tadeáš Divácký
-- Create date: 15.12.2020
-- Description:	Procedura pro naplneni FASK_ZASOBY
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY] 
	-- Add the parameters for the stored procedure here
@ExportTypFilter nvarchar(100),
@ExportSkladFilter nvarchar(100),
@ExportovatPouzeAktivniPolozky bit,
@EXZas_DotahovatAlternativniDodavatele bit,
@EvidenceSarzi bit,
@EvidenceVyrobnichCisel bit,
@PohodaE1 bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM FASK_ZASOBY

	--TODO, na aplikační urovni je udelana logika s LOCNCODE a CZ_SerNumTrack
	--TODO je potreba dotahnout, a rozhodnout co vlastně udelat....


	IF (@ExportSkladFilter is not null AND @ExportSkladFilter != '' ) OR (@ExportTypFilter is not null  AND @ExportTypFilter != '' )
BEGIN --A0
	IF @ExportovatPouzeAktivniPolozky = 1
		BEGIN --B0 

			--SKz_FillBy_EPAP_DAD
			INSERT INTO [dbo].[FASK_ZASOBY]
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
			   Left(SKz.ID,40) as ITEMNMBR,
			   Left(SKz.Nazev, 100) as ITEMDESC,
			   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
			   Left(SKz.EAN, 60) as VNDITNUM,
			   '' as CZ_CarKod,
			   '' as LOCNCODE,  --TODO složizejší
			   SKz.RefSklad as SKL_ID,
			   ISNULL(SKz.StavZ, 0) as QTY,
			   0 as QTYPACK,
			   Left(ISNULL(SKz.MJ, ''),10) as MJ,
			   '' as DMJ,
			   0 as TAXRATE,
			   0 as PRICE0,
			   0 as PRICE1,
			   0 as PRICE2,
			   0 as PRICE3,
			   0 as PRICE4,
			   0 as PRICE5,
			   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
			   0 as CZ_SerNum_Delka,
			   0 as CZ_Rez1_Track,
			   0 as CZ_Rez2_Track, 
			   0 as CZ_Rez3_Track, 
			   0 as CZ_Rez4_Track, 
			   0 as REZ1,
			   0 as REZ2,
			   0 as REZ3,
			   0 as REZ4,
			   Left(SKz.RefAD, 12) as ODB_ID,
			   '' as mena_ID,
			   '' as SERLTNUM,
			   null as WEIGHT,
			   null as TIMEFROM,
			   null as TIMETO,
			   GETDATE() as LSTMod,
			   '' as loginid,
			   0 as CZ_Expirace_Track,
			   null as EXPIRACE
		FROM StwPh_04535667_2020.dbo.SKz 
		INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

			IF @EXZas_DotahovatAlternativniDodavatele = 1
				BEGIN --C0

					--SKzAlternatives_FillBy_EPAP_DAD
					INSERT INTO [dbo].[FASK_ZASOBY]
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
				   Left(SKz.ID,40) as ITEMNMBR,
				   Left(SKz.Nazev, 100) as ITEMDESC,
				   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				   Left(SKzNC.EAN, 60) as VNDITNUM,
				   '' as CZ_CarKod,
				   '' as LOCNCODE,  --TODO složizejší
				   SKz.RefSklad as SKL_ID,
				   ISNULL(SKz.StavZ, 0) as QTY,
				   0 as QTYPACK,
				   Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				   '' as DMJ,
				   0 as TAXRATE,
				   0 as PRICE0,
				   0 as PRICE1,
				   0 as PRICE2,
				   0 as PRICE3,
				   0 as PRICE4,
				   0 as PRICE5,
				   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				   0 as CZ_SerNum_Delka,
				   0 as CZ_Rez1_Track,
				   0 as CZ_Rez2_Track, 
				   0 as CZ_Rez3_Track, 
				   0 as CZ_Rez4_Track, 
				   0 as REZ1,
				   0 as REZ2,
				   0 as REZ3,
				   0 as REZ4,
				   Left(SKz.RefAD, 12) as ODB_ID,
				   '' as mena_ID,
				   '' as SERLTNUM,
				   null as WEIGHT,
				   null as TIMEFROM,
				   null as TIMETO,
				   GETDATE() as LSTMod,
				   '' as loginid,
				   0 as CZ_Expirace_Track,
				   null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz 
			INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
			INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
			WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

				END--C0

		END --B0
	ELSE
	BEGIN --B1
	
		--SKz_FillBy_DAD
		INSERT INTO [dbo].[FASK_ZASOBY]
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
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
		   FROM StwPh_04535667_2020.dbo.SKz 
		   INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		   WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
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
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
		FROM StwPh_04535667_2020.dbo.SKz as SKz 
		INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
		INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

			END--C1

	END --B1

END--A0
ELSE
BEGIN -- A1
	IF @ExportovatPouzeAktivniPolozky = 1
	BEGIN

		--SKz_GetDataByAktivniPolozky
		INSERT INTO [dbo].[FASK_ZASOBY]
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
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
	FROM StwPh_04535667_2020.dbo.SKz as SKz WHERE (SKz.Odbyt <> 0)

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
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
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg WHERE (SKz.Odbyt <> 0)

			END--C1

	END
	ELSE
	BEGIN
	
		--SKz_GetDataByOptimalize
		INSERT INTO [dbo].[FASK_ZASOBY]
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
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
		   FROM StwPh_04535667_2020.dbo.SKz as SKz  

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
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
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg

			END--C1


	END
END --A1


SELECT Count(*) from FASK_ZASOBY

END

/**************************************************************************************/