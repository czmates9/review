/****** Object:  StoredProcedure [dbo].[FASK_procGetSarzeVyroba]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 3.9.2020
-- Description:	Procedura, generujici aktualni sarzi pro vyrobu
-- =============================================
CREATE PROCEDURE [dbo].[FASK_procGetSarzeVyroba] 
	-- Add the parameters for the stored procedure here
	@smenaID nvarchar(20) = '999', 
	@userID nvarchar(20) = '',
	@linkaID nvarchar(10) = 0,
	@QTY numeric(19,5) = 0,
	@ITEMNMBR nvarchar(40) = ''
AS
BEGIN
	SET NOCOUNT ON;
	

	declare @datum datetime = getdate()
	declare @pracovnik nvarchar(10) -- maximum bude 10 ...
	declare @den nvarchar(2) = ''
	declare @mesic nvarchar(2) = ''
	declare @rok nvarchar(4) = ''	
			
	set @den = RIGHT('0' + CONVERT(nvarchar(2), DAY(@datum)), 2)
	set @mesic = RIGHT('0' + CONVERT(nvarchar(2), MONTH(@datum)), 2)
	set @rok = RIGHT('00' + CONVERT(nvarchar(4), YEAR(@datum)), 4)
	set @pracovnik = RIGHT('0000' + CONVERT(nvarchar(4), @userID), 4)
	
	declare @sarze nvarchar(255)
	Set @sarze = @pracovnik + + @rok + @mesic +	@den  	


	SELECT @sarze as Sarze
	
END

GO
/**************************************************************************************/