-- =============================================
-- Author:		Jiri skrivanek
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InsertEvents] 
   ON  [dbo].[FASK_Events] 
   FOR INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Deklarace promennych z inserted	
	declare @loginid [nvarchar](20)
	declare @machineid [nvarchar](20) 
	declare @dateeve [datetime] 
	declare @dateevepro [varchar] (21)
	declare @qty [numeric](19, 5)
	declare @qtyReal [numeric](19, 5)
	--declare @description [ntext] 
	declare @barcodeReaded [nchar](50)
	declare @barcodeSended [nchar](50)
	declare @zakazka [nchar](20)
	declare @material [nvarchar] (255)
	declare @popis [nchar](10)
	declare @faskGUID [uniqueidentifier]
	declare @reportType [nchar](1)
	declare @isProcessed [datetime]
	declare @IDO [nchar](10)
	declare @scan1 [nvarchar](255)
	declare @scan2 [nvarchar](255)
	declare @scan3 [nvarchar](255)
	declare @sensor [nvarchar](50)	
	
    -- Insert statements for trigger here
    begin tran
        
	select 
		@loginid = i.loginid, 
		@machineid = i.machineid,
		@zakazka = i.zakazka,
		@qty= i.qty,
		@dateeve = i.dateeve,
		@qtyReal = i.qtyReal,
		@barcodeReaded  = i.barcodeReaded,
		@popis = i.popis,
		@material = i.material
	from INSERTED i;        
    
	declare @pom [char](2)

	IF ( @popis = 'True' ) 
	BEGIN
		set @pom = 0
	END
	ELSE 
	BEGIN
		set @pom = 1
	END
	
	set @dateevepro = CONVERT(varchar, @dateeve, 120)
	set @dateevepro = SUBSTRING(@dateevepro, 1, 4) + 
					  SUBSTRING(@dateevepro, 6, 2) + 
					  SUBSTRING(@dateevepro, 9, 2) +
					  SUBSTRING(@dateevepro, 12, 2) + 
					  SUBSTRING(@dateevepro, 15, 2) + 
					  SUBSTRING(@dateevepro, 18, 2)

	EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyReal, @dateevepro ,@loginid,'','','','', @material
    	    
    commit tran

END


GO


