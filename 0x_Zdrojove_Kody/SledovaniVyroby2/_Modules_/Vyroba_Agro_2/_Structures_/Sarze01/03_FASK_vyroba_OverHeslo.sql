USE [agro_fask]
GO
/****** Object:  UserDefinedFunction [dbo].[FASK_vyroba_OverHeslo]    Script Date: 08/28/2017 11:12:23 ******/
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
	@inID nchar(25),
	@inHESLO nchar(10)

)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT psswd FROM FASK_logins WHERE id = @inID AND psswd = @inHESLO
)
