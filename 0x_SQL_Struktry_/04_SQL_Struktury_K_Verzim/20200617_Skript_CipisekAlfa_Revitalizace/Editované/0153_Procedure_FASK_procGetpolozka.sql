/****** Object:  StoredProcedure [dbo].[FASK_procGetpolozka]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[FASK_procGetpolozka]
	-- Add the parameters for the stored procedure here
	@ITEMNMBR varchar(31)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	--Nastavuje se v Web.Config na serveru parameter VydejkaDetailPolozka
    -- Insert statements for procedure here
		select Doprava
from StwPh_04535667_2020.dbo.SKz
		where
		ID = @ITEMNMBR
END

GO
/**************************************************************************************/