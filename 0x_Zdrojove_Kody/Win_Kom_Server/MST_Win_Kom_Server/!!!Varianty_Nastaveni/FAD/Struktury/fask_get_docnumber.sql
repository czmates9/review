USE [Servis_FAD]
GO

/****** Object:  UserDefinedFunction [dbo].[fask_get_docnumber]    Script Date: 05/24/2016 07:57:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[fask_get_docnumber](@odb_id varchar(12), @okruh_id varchar(20))
RETURNS VARCHAR(17)
AS 
BEGIN
	-- 20.5.2016 PeV: zmena, nastavuje se pouze ID odberatele
	-- 24.2.2017 JiS: zmena, nastavuje se dle poslanych parametru ...
	IF (isnull(@okruh_id, '')<>'') 
		return SUBSTRING(ISNULL(@odb_id, '-') + '-' + ISNULL(@okruh_id, ''), 0, 17)
	
	return ISNULL(@odb_id, '-')
END


GO


