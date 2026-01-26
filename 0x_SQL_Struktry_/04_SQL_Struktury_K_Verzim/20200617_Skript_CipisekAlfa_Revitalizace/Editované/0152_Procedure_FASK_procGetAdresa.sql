/****** Object:  StoredProcedure [dbo].[FASK_procGetAdresa]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FASK_procGetAdresa]
	-- Add the parameters for the stored procedure here
	@SOPNUMBE nvarchar(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	--Nastavuje se v Web.Config na serveru
    -- Insert statements for procedure here
		select Firma, Firma2, Utvar, Utvar2, Jmeno, Jmeno2, Ulice, Ulice2, PSC, PSC2, Obec, Obec2, ICO, DIC
from StwPh_04535667_2020.dbo.OBJ
		where
		Cislo = @SOPNUMBE
END

GO
/**************************************************************************************/