/****** Object:  UserDefinedFunction [dbo].[FASK_GetListPolozekPoSluzbe]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeáš Divácký
-- Create date: 4.5.2021
-- Description:	Funkce která vrací list ID služek ktere jsou pod kartou v objednávce
-- =============================================
CREATE FUNCTION [dbo].[FASK_GetListPolozekPoSluzbe]
(
	-- Add the parameters for the function here
	@ORD int 
)
RETURNS @List_ORD TABLE 
(
	ID_Polozky int
)
AS
BEGIN
	

DECLARE @ID int;
DECLARE @RelSkTyp int;


DECLARE curPol CURSOR FOR
SELECT 
o.ID, 
zas.RelSkTyp 
FROM
(SELECT OrderFld, RefAg  FROM StwPh_04535667_2020.dbo.OBJpol as op
left join StwPh_04535667_2020.dbo.SKz as z ON z.ID = op.RefSKz
where 1 = 1 AND op.ID = @ORD) as x
LEFT JOIN StwPh_04535667_2020.dbo.OBJpol as o ON o.RefAg = x.RefAg
LEFT JOIN StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = o.RefSKz
where 1=1
AND o.RefAg = x.RefAg
AND o.OrderFld > x.OrderFld
order by o.OrderFld

OPEN curPol
WHILE (1=1) 
	BEGIN
	
	FETCH NEXT FROM curPol INTO @ID, @RelSkTyp 
	IF @@FETCH_STATUS <> 0
	BEGIN
		BREAK
	END

	IF @RelSkTyp in (3)
		BEGIN
			insert into @List_ORD(ID_Polozky) values (@ID)
		END
	ELSE
		BEGIN
			BREAK
		END

END
	
CLOSE curPol
DEALLOCATE curPol

--SELECT ORD from @List_ORD

	RETURN 
END

GO

/**************************************************************************************/

