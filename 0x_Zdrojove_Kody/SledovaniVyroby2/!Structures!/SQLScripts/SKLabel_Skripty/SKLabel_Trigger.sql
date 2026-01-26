SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP TRIGGER replicator;
GO

-- =============================================
-- Author:		Jiri skrivanek
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER replicator 
   ON  FASK_Events 
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
		@material = i.material,
		@IDO = i.IDO,
		@sensor = i.sensor,
		@scan1 = i.scan1,
		@scan2 = i.scan2,
		@scan3 = i.scan3
	from INSERTED i;        
    
    Declare @idstroj nvarchar(50)
    Declare @idosoby nvarchar(50)
    Declare @idzakazky nvarchar(50)
    Declare @idoperace nvarchar(255)
    Declare @idkrok nvarchar(50)
    Declare @mnozstvi nvarchar(50)
    Declare @crole nvarchar(50) 
    Declare @ean nvarchar(50)
    Declare @prole nvarchar(50)
    
    set @idstroj = '\id_stroj{' + rtrim(@machineid) + '}'
    set @idosoby = '\id_osoby{' + rtrim(@loginid) + '}'
    set @idzakazky = '\id_zakazky{' + rtrim(@zakazka) + '}'
    set @idoperace = '\id_operace{' + rtrim(@material) + '}'
    set @idkrok = '\id_krok{' + rtrim(@IDO) + '}'
    set @mnozstvi = '\mnozstvi{' + rtrim(@sensor) + '}'
    set @crole = '\crole{' + rtrim(@scan1) +'}'
    set @ean = '\ean{' + rtrim(@scan2) +'}'
    set @prole = '\p_role{' + rtrim(@scan3) +'}'

    Declare @data varchar(2000)
--    IF (@IDO = '101') --zahajeni operace bez mnozstvi(bez mnozstvi a operace)
--    begin
--		  Set @data = @idstroj + @idosoby + @idzakazky + @idkrok --+ @idoperace + @mnozstvi    
--    end 
--    else 
--    if (@IDO = '102') --test na id operace 102 (bez mnozstvi)
--    begin
--		  Set @data = @idstroj + @idosoby + @idzakazky + @idkrok + @idoperace --+ @mnozstvi    
--    end
--    else 
    if (@IDO = '401') --test na id operace 401 (nacitani dodatecnych scanu)
    begin
		  Set @data = @idstroj + @idosoby + @idzakazky + @idkrok + @idoperace + @crole + @ean + @prole --+ @mnozstvi    
    end
    else --jakakoli dalsi variant vzdy vse dohromady
    begin
		  Set @data = @idstroj + @idosoby + @idzakazky + @idkrok + @idoperace + @mnozstvi    
    end
    
    insert into sklabel_global.dbo.ANET_k2 (cas, typ, data, zprac, cas_zprac) 
    values (getdate(), '100', @data, 0, NULL)
    
    commit tran

END
GO
