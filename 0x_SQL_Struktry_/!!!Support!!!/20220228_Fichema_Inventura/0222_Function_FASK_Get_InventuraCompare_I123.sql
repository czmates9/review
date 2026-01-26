/****** Object:  UserDefinedFunction [dbo].[FASK_Get_InventuraCompare_I123]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data 
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_InventuraCompare_I123] 
(
	-- Add the parameters for the function here
		@CountEntries int,
		@V_0 bit,
		@V_1 bit,
		@V_2 bit,
		@V_3 bit,
		@V_4 bit
)
RETURNS
 @returnList TABLE 
 (
    [CountEntries] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[SKL_DESC] [nvarchar](40) NULL,
	[QUANTITY] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[Expirace] [datetime] NULL,
	[status] int null
 )
AS
BEGIN

----------------------------------------
-- Varianta 0  -------------------------
----------------------------------------

-- Tahle varianta v I4 neexistuje

IF @V_0 = 1
	BEGIN 

		INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		0 as status
		from FASK_Inventura_I123_Compare as I123
		left join FASK_Inventura_I4_Compare as I4 on 
		I4.CountEntries = I123.CountEntries 
		AND I4.ITEMNMBR = I123.ITEMNMBR
		AND I4.VNDITNUM = I123.VNDITNUM
		AND I4.MJ = I123.MJ
		AND I4.SERLNMBR = I123.SERLNMBR
		AND I4.Expirace = I123.Expirace
		where 1 = 1
		AND I4.ITEMNMBR is null
		and I123.CountEntries = @CountEntries
	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 1  -------------------------
----------------------------------------

IF @V_1 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		1 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I4.QUANTITY = I123.QUANTITY
and I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 2  -------------------------
----------------------------------------

IF @V_2 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		2 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I4.QUANTITY < I123.QUANTITY
and I4.CountEntries = @CountEntries

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 3  -------------------------
----------------------------------------

IF @V_3 = 1
	BEGIN 

			INSERT INTO @returnList 
		(
		[CountEntries],
		[ITEMNMBR],
		[ITEMDESC],
		[ITEMCODE],
		[VNDITNUM],
		[SKL_ID],
		[SKL_DESC],
		[QUANTITY],
		[MJ],
		[SERLNMBR],
		[Expirace],
		[status] 
		)
		SELECT
		I123.[CountEntries],
		I123.[ITEMNMBR],
		I123.[ITEMDESC],
		I123.[ITEMCODE],
		I123.[VNDITNUM],
		I123.[SKL_ID],
		I123.[SKL_DESC],
		I123.[QUANTITY],
		I123.[MJ],
		I123.[SERLNMBR],
		I123.[Expirace],
		3 as status
from FASK_Inventura_I123_Compare as I123
left join  FASK_Inventura_I4_Compare as I4 on 
I4.CountEntries = I123.CountEntries 
AND I4.ITEMNMBR = I123.ITEMNMBR
AND I4.VNDITNUM = I123.VNDITNUM
AND I4.MJ = I123.MJ
AND I4.SERLNMBR = I123.SERLNMBR
AND I4.Expirace = I123.Expirace
where 1 = 1
AND I4.QUANTITY > I123.QUANTITY
and I4.CountEntries = @CountEntries

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 4  -------------------------
----------------------------------------

-- Tato varianta pro I123 neexistuje

--IF @V_4 = 1
--	BEGIN 


--END

----------------------------------------
----------------------------------------
----------------------------------------

return 
END


GO

/**************************************************************************************/


