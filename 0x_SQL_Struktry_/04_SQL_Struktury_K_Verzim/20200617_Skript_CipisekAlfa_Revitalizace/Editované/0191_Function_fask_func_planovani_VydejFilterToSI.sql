/****** Object:  UserDefinedFunction [dbo].[fask_func_planovani_VydejFilterToSI]    Script Date: 02.02.2021 9:52:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 16.12.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[fask_func_planovani_VydejFilterToSI] 
(
	-- Add the parameters for the function here
	@ITEMNMBR nvarchar(40) = null,
	@SKL_ID nvarchar(20) = null,
	@ORD int null
)
RETURNS bit
AS
BEGIN

	DECLARE @Result bit;
	DECLARE @Typ int;


	SET @Result = 0;

	--Funkce musí byt, ale defaultně by mnela vracet 0, jakožto FALSE, a tym padem to nebude pšenašet do SI žadne řadky


	SELECT distinct @Typ = S.RelSkTyp FROM StwPh_04535910.dbo.SKz as S where S.ID = @ITEMNMBR 
	--AND S.RefSklad = @SKL_ID

--Vsechny = 0,
--Karta = 1,
--Textova = 2,
--Sluzba = 3,
--Komplet = 4,
--Vyrobek = 5,
--Souprava = 6

IF @Typ = 0
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 1
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 2
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 3
	BEGIN
		SET @Result = 1;
	END
ELSE IF  @Typ = 5
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 5
	BEGIN
		SET @Result = 0;
	END

return @Result;

END
GO

/**************************************************************************************/

