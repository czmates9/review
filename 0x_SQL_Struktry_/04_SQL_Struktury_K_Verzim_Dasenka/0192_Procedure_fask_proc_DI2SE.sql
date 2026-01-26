/****** Object:  StoredProcedure [dbo].[fask_proc_DI2SE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
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


		if @coSeDeje = 'TO' begin
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
			'' as ITEMTYPE,
			Z.ITEMDESC,
			'' as VNDDOCNM,
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
			'' as Note,
			'' as TYPEPAL,
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
WHERE DI.SERLTNUM IS NOT NULL AND DI.SERLTNUM != '' AND DI.CountEntries = @p2

	end
	-- neznámý cosedeje ;)
	else begin
		return -1
	end

END

GO
/**************************************************************************************/
