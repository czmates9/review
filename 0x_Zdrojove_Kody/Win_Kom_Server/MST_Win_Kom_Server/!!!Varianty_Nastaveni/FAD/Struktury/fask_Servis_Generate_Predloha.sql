USE [Servis_FAD]
GO

/****** Object:  StoredProcedure [dbo].[fask_Servis_Generate_Predloha]    Script Date: 05/24/2016 07:57:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












-- =============================================
-- Create date: 15.03.2016
-- Description:	Funkce pro vygenerovani nove davky
-- =============================================
CREATE PROCEDURE [dbo].[fask_Servis_Generate_Predloha] 
	-- Add the parameters for the stored procedure here
	@document_number varchar(17) = '', --??? toto je k nicemu???
	@odb_id varchar(12) = '', 
	@okruh_id varchar(20) = '', 
	@UserID int, 
	@CountEntries int output,
	@message varchar(100) output	--V pripade chyby zde muze byt popis chyby k zobrazeni uzivateli ...
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Navratova hodnota procedury
	-- 0 = OK
	-- 1.. = Chyba	
	declare @error int
	SET @error = 0
		
	-- 1) slozeni jedinecneho documentnumber (datum + ID okruhu) ... !! muze nastat problem s delkou, pokud bude ID moc dlouhe !!
	--declare @docnumber varchar(17)
	--SET @docnumber = CONVERT(VARCHAR(8), GETDATE(), 12) + @okruh_id
	declare @docnumber varchar(17)
	--SET @docnumber = SUBSTRING(@odb_id + '-' + @okruh_id, 0, 17) 
	SET @docnumber = (select dbo.fask_get_docnumber(@odb_id, @okruh_id))
	
	-- 2) kontrola existence davky podle documentnumber (zdali existuje a je rozpracovana)
	/*
	IF EXISTS(SELECT TOP 1 1 FROM [CZMST_Servis_Predloha] WHERE [DOCUMENT_NUMBER] = @docnumber) BEGIN --and [Rozpracovano] <> 0) BEGIN		
		SET @error = 1
		SET @message = 'Dávka ' + @docnumber + ' již existuje!'
		RETURN @error
	END
	*/
	IF EXISTS(SELECT TOP 1 1 FROM [CZMST_Servis_Predloha] WHERE [DOCUMENT_NUMBER] = @docnumber and [Rozpracovano] < 100) BEGIN
         declare 
		@ce int,
		@dn nvarchar(20),
		@rozpr tinyint,
		@oid nvarchar(20),
		@uid int,
		@bc nvarchar(20),
		@odbid nvarchar(20)

		SELECT TOP 1 
			@ce = a.CountEntries, 
			@dn = a.DOCUMENT_NUMBER,
			@rozpr = a.Rozpracovano,
			@oid = a.OkruhID,
			@uid = a.UserID,
			@odbid = a.ODB_ID
		FROM [CZMST_Servis_Predloha] a
		WHERE a.[DOCUMENT_NUMBER] = @docnumber 
		and a.[Rozpracovano] < 100


	IF (@ce is not null)
	BEGIN		
		SET @error = 1
		SET @message = 'Dávka ' + convert(nvarchar, @ce) + ' pro ' + @docnumber + ' již existuje! (Stav='+convert(nvarchar,@rozpr)+')'
		RETURN @error
	END
	
	-- 4) zjisteni maximalniho cisla davky a ulozeni
	-- Select isnull(MAX(CountEntries),0)+1 FROM " + TABLE_CZMST_Servis_Predloha
	SET @CountEntries = (SELECT (isnull(MAX(CountEntries), 0) + 1) FROM CZMST_Servis_Predloha)
		
	-- kontrola existence rozpracovane/vygenerovane davky podle id okruhu
	/*
	IF EXISTS(SELECT TOP 1 1 FROM [CZMST_Servis_Predloha] WHERE [OkruhID] = @okruh_id AND [Rozpracovano] < 100 ) BEGIN --and [Rozpracovano]] <> 0) BEGIN		
		SET @error = 1
		SET @message = 'Dávka s okruhem ' + @okruh_id + ' již existuje!'
		RETURN @error
	END
	*/
	
	-- 2) kontrola existence okruhu
	-- 20.5.2016 PeV: vypnuta kontrola na existenci okruhu
	/*
	IF Not EXISTS(SELECT TOP 1 1 FROM [CZMST_Servis_Okruh] WHERE [ID] = @okruh_id) BEGIN		
		SET @error = 1
		SET @message = 'Okruh ' + @okruh_id + ' neexistuje!'
		RETURN @error
	END
	*/
	-- 2) kontrola existence odberatele
	IF Not EXISTS(SELECT TOP 1 1 FROM [CZMST090] WHERE [odb_id] = @odb_id) BEGIN		
		SET @error = 1
		SET @message = 'Odbìratel ' + @odb_id + ' neexistuje!'
		RETURN @error
	END
	
	-- nacteni caroveho kodu okruhu
	declare @barcode nvarchar(50);
	-- 20.5.2016 PeV: nastaveni caroveho kodu okruhu zmeneno na carovy kod odberatele ...
	--set @barcode = (SELECT TOP 1 Barcode FROM [CZMST_Servis_Okruh] WHERE [ID]=@okruh_id);
	set @barcode = (SELECT TOP 1 odb_carcode FROM [CZMST090] WHERE [odb_id]=@odb_id);
	
	-- 3) nastaveni Rozpracovano na 200 u davek, ktere jeste jsou vygenerovany, ale nejsou stazeny (treba nastala zmena v poctu zdroju ...)
	--Update " + TABLE_CZMST_Servis_Predloha + " set Rozpracovano=200 where Rozpracovano=0 and DOCUMENT_NUMBER=" + odb_id + " and OkruhID=" + okruhid
	UPDATE CZMST_Servis_Predloha set Rozpracovano=200 WHERE Rozpracovano=0 and DOCUMENT_NUMBER= @docnumber AND OkruhID = @okruh_id
	
	-- 5) vlozeni nove davky do predlohy
	INSERT INTO CZMST_Servis_Predloha (CountEntries, DOCUMENT_NUMBER, OkruhID, Rozpracovano, UserID, Barcode, ODB_ID)
	VALUES	
	(@CountEntries, @docnumber, @okruh_id, 0, @UserID, @barcode, @odb_id)
	    
	RETURN @error
END













GO


