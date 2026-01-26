/****** Object:  UserDefinedFunction [dbo].[FASK_Get_POHODA_LokMechMapingID]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 -- =============================================
 -- Author:	Bc. Tadeas Divacky
 -- Create date: 22.01.2020
 -- Description:	Funkce která vraci uživatelske parametry pro filtrovani do načteí obchodniho požadavk pro Planovani
 -- =============================================
 CREATE FUNCTION [dbo].[FASK_Get_POHODA_LokMechMapingID]
 (	
 	@ITEMNMBR_Zdroj nvarchar(40) = null,
	@SKL_ID_Zdroj nvarchar(20) = null,
	@SKL_ID_Cil nvarchar(20) = null
 )
 RETURNS @Params TABLE
 (
	[ITEMNMBR] nvarchar(40) NULL,
 	[ITEMDESC] [nvarchar](100) NULL
 )
 AS
 BEGIN
 
 
  insert into @Params ([ITEMNMBR], [ITEMDESC])
	SELECT 
	N.ID as [ITEMNMBR],
	LEFT(N.Nazev,100) as [ITEMDESC] 
	FROM
	(SELECT IDS, EAN, Nazev,MJ, RefAD, RelSKzVC from StwPh_04535667_2020.dbo.Skz 
	where ID = @ITEMNMBR_Zdroj AND RefSklad = @SKL_ID_Zdroj) as O
	LEFT JOIN StwPh_04535667_2020.dbo.Skz as N ON 
	ISNULL(N.IDS,'') = ISNULL(O.IDS,'') 
	AND  ISNULL(N.EAN,'') = ISNULL(O.EAN,'')
	AND ISNULL(N.Nazev,'') = ISNULL(O.Nazev,'')
	AND ISNULL(N.MJ,'') = ISNULL(O.MJ,'')
	AND ISNULL(N.RefAD,'') = ISNULL(O.RefAD,'')
	AND ISNULL(N.RelSKzVC,'') = ISNULL(O.RelSKzVC,'')
	AND N.ID != @ITEMNMBR_Zdroj
	AND N.RefSklad = @SKL_ID_Cil

  	return
 
 END

GO
 
/**************************************************************************************/