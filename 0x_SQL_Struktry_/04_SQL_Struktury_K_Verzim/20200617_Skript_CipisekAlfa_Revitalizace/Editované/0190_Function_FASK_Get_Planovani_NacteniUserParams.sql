/****** Object:  UserDefinedFunction [dbo].[FASK_Get_Planovani_NacteniUserParams]    Script Date: 26.01.2021 8:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 -- =============================================
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
 
 
  insert into @Params ([ORD], [UserParam_1], [UserParam_2], [UserParam_3],[UserParam_4], [UserParam_5])
 		SELECT 
		op.ID as [ORD],
		[UserParam_1] = CASE o.VPrZapl    
			WHEN 1 THEN 'Zaplaceno'    
			ELSE 'Nezaplaceno'  
		END ,
		UzivSeznamPol.IDS as [UserParam_2],
		'' as [UserParam_3],
		'' as [UserParam_4],
		'' as [UserParam_5]
 		FROM StwPh_04535667_2020.dbo.OBJ o
		left join StwPh_04535667_2020.dbo.OBJpol op ON op.RefAg = o.ID
 		left join StwPh_04535667_2020.dbo.sVPULpol UzivSeznamPol ON UzivSeznamPol.ID = o.RefVPrDoprava
  	return
 
 END
 
GO


/**************************************************************************************/
