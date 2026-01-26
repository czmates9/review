/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bc. Tadeáš Divácký
-- Create date: 15.12.2020
-- Description:	Procedura pro naplneni FASK_ZASOBY
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY] 
	-- Add the parameters for the stored procedure here
@ExportTypFilter nvarchar(100),
@ExportSkladFilter nvarchar(100),
@ExportovatPouzeAktivniPolozky bit,
@EXZas_DotahovatAlternativniDodavatele bit,
@EvidenceSarzi bit,
@EvidenceVyrobnichCisel bit
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
			   '' as CZ_CarKod,
			   '' as LOCNCODE, 
			   '1' as SKL_ID, -- Treba uprasnit s AGRO použivany sklad a jeho ID
			   0 as QTY,  -- Treba upresnit....
			   ZAS.PrepKoefPaleta as QTYPACK, -- bude se dotahovat k alert MJ kteru je treba dopresnit
			   Left(ZAS.MjAlter ,5) as MJ, -- treba dopresnit
			   '' as DMJ,
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
			   '' as mena_ID,
			   '' as SERLTNUM,
			   null as WEIGHT,
			   null as TIMEFROM,
			   null as TIMETO,
			   GETDATE() as LSTMod,
			   '' as loginid,
			   0 as CZ_Expirace_Track,
			   null as EXPIRACE
		FROM [agrocs-ekonom].dbo.V_IIS_CI_CiselnikVyrobkuProFask1 as ZAS 
		where ZAS.MjAlter != '' or ZAS.MjAlter is not null
		) as X
		order by x.ITEMNMBR

SELECT Count(*) from FASK_ZASOBY

END

/**************************************************************************************/
