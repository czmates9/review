/****** Object:  UserDefinedFunction [dbo].[FASK_vyroba_OverHeslo]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 28.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontroli HESLA dle zadaneho ID 
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverHeslo]
(	
	-- Add the parameters for the function here
	@inID nvarchar(25),
	@inHESLO nvarchar(10)

)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT psswd FROM FASK_logins WHERE USERID = @inID AND psswd = @inHESLO
)

GO
/**************************************************************************************/