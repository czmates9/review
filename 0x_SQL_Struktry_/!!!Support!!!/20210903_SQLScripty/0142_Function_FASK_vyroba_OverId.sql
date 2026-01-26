/****** Object:  UserDefinedFunction [dbo].[FASK_vyroba_OverId]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 25.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontrol ID
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverId]
(	
	-- Add the parameters for the function here
	@id nvarchar(25) 
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT USERID FROM FASK_logins WHERE USERID = @id
)

GO
/**************************************************************************************/