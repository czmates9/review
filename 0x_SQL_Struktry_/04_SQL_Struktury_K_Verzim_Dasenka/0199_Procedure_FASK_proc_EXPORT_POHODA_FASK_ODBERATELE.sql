/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_ODBERATELE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ODBERATELE] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST090

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
		FROM StwPh_04535667_2020.dbo.AD

END

/**************************************************************************************/