/****** Object:  UserDefinedFunction [dbo].[fask_GS1_AI_GET]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 5.6.2019
-- Description:	Dekoduje z retezce pozadovany gs1 ai hodnotu
-- =============================================
CREATE FUNCTION [dbo].[fask_GS1_AI_GET] 
(
	-- Add the parameters for the function here
	@ai nvarchar(max),
	@barcode nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(max)
	SET @Result = null

	IF (ltrim(rtrim(ISNULL(@barcode, ''))) = '')
	BEGIN
		Return @Result
	END

	-- Add the T-SQL statements to compute the return value here
	IF (@ai = 'EAN')
	begin
		SELECT @Result = gs1.EAN from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'EXPIRACE')
	begin
		SELECT @Result = gs1.EXPIRACE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'SARZE')
	begin
		SELECT @Result = gs1.SARZE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'VAHA')
	begin
		SELECT @Result = gs1.CISTAHMOTNOST from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'MNOZSTVI')
	begin
		SELECT @Result = gs1.MNOZSTVI from FASK_ParseGS1(@barcode) gs1
	end

	-- Return the result of the function
	RETURN @Result

END

GO
/**************************************************************************************/