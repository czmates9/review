/****** Object:  StoredProcedure [dbo].[fask_vyroba_GetSarze]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Jiri Skrivanek
-- Create date: 3.7.2017
-- Description:	Procedura, generujici aktualni sarzi pro vyrobu
-- =============================================
Create PROCEDURE [dbo].[fask_vyroba_GetSarze] 
	-- Add the parameters for the stored procedure here
	@smenaID nvarchar(20) = '999', 
	@userID nvarchar(20) = '',
	@linkaID nvarchar(10) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @datum datetime = getdate()
	declare @pracovnik nvarchar(10) -- maximum bude 10 ...
	declare @den nvarchar(2) = ''
	declare @mesic nvarchar(2) = ''
	declare @rok nvarchar(4) = ''	
			
	set @den = RIGHT('0' + CONVERT(nvarchar(2), DAY(@datum)), 2)
	set @mesic = RIGHT('0' + CONVERT(nvarchar(2), MONTH(@datum)), 2)
	set @rok = RIGHT('00' + CONVERT(nvarchar(4), YEAR(@datum)), 4)
	--set @pracovnik = RIGHT('0000000000' + @smenaID + @userID + @linkaID, 10)
	--set @pracovnik = @smenaID + @userID + @linkaID
	set @pracovnik = RIGHT('0000' + CONVERT(nvarchar(4), @userID), 4)
	
	declare @sarze nvarchar(255)
	--Set @sarze = @pracovnik + '|' + @den + '|'+ @mesic + '|'+ @rok
	--Set @sarze = @pracovnik + @den + @mesic + @rok
	Set @sarze = @pracovnik + + @rok + @mesic +	@den  	
	SELECT @sarze as Sarze
	
END

GO
/**************************************************************************************/