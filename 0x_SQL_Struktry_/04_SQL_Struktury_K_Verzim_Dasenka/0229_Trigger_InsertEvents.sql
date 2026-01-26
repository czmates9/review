/****** Object:  Trigger [dbo].[InsertEvents]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri skrivanek a Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER [dbo].[InsertEvents] 
   ON  [dbo].[FASK_Events] 
   FOR INSERT
AS 
BEGIN
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
	declare @status int

		-- TaD nove parametry
	declare @id int
	declare @VPH [nvarchar](30)
	declare @IS_ID [nvarchar](40)

	declare @qtypack [numeric](19, 5)
	declare @PackType [nvarchar](50)

	declare @NMBRPAL [nvarchar](50)
    declare @WEIGHT [numeric](19,5)

declare @rez1 [nvarchar](100)
declare @rez2 [nvarchar](100)
declare @rez3 [nvarchar](100)
declare @rez4 [nvarchar](100)
declare @rez5 [nvarchar](100)
	
    -- Insert statements for trigger here
    begin tran
        
	select 
		@loginid = i.loginid, 
		@machineid = i.machineid,
		@zakazka = i.zakazka,
		@qty= i.qty,
		@dateeve = i.dateeve,
		@qtyReal = i.qtyReal,
		@barcodeReaded  = i.barcodeSended,
		@popis = i.popis,
		@material = i.material,
		@scan3 = i.scan3,
		@id = i.id,
		@VPH = i.VPH,
		@IS_ID = i.IS_ID,
		@status = i.status,
		@qtypack = i.QTYPACK,
		@PackType = i.PackType,
		@NMBRPAL = i.NMBRPAL,
		@WEIGHT = i.WEIGHT,
                @rez1 = i.REZ_1,
                @rez2 = i.REZ_2,
                @rez3 = i.REZ_3,
                @rez4 = i.REZ_4,
                @rez5 = i.REZ_5
	from INSERTED i;        
    
	declare @pom [char](2)

	IF ( @status = 0 OR  @status = 600 ) 
		BEGIN

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

			--	EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyReal, @dateevepro ,@loginid,'','','','', @material
			--	8.7.2021 JiS : do hodnoty rez1 se uklada prubezna hodnota palet za smenu
				declare @rezX char(10)
				select @rezX = substring(@scan3, 1, 5)
				
				declare @qtyOUT [numeric](19, 5)

				IF ( 1 = 1)
					BEGIN
						IF (@PackType = 'UP')
							BEGIN

								IF (@status = 0)
									BEGIN
										SET @qtyOUT = @qtypack
									END
								ELSE IF (@status = 600)
									BEGIN
										SET @qtyOUT = -@qtypack
									END
								
						--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'','', @rezX, '', @material
						EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve, @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
							END
						IF (@PackType = 'NP')
							BEGIN
								SET @qtyOUT = 0
								--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'','', @rezX, '', @material
								EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve , @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
							END
					END
				ELSE
					BEGIN
						SET @qtyOUT = @qtyReal
						--EXECUTE dbo.CZPRO_A2_insert @machineid, @pom, @barcodeReaded,@qty,@qtyOUT, @dateevepro ,@loginid,'','', @rezX, '', @material
						EXECUTE dbo.fask_Events2Production_Trigger @id, @VPH, @IS_ID, @loginid, @machineid, @qty, @qtyOUT, @barcodeReaded, @dateeve , @NMBRPAL, @PackType, @status, @WEIGHT, @qtypack, @rez1, @rez2, @rez3, @rez4, @rez5
					END

    	 END  

    commit tran

END


GO

ALTER TABLE [dbo].[FASK_Events] ENABLE TRIGGER [InsertEvents]
GO


/**************************************************************************************/
