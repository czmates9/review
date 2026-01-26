/****** Object:  UserDefinedFunction [dbo].[FASK_ParseGS1]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FASK_ParseGS1] ( @stringToParse NVARCHAR(MAX))
RETURNS
 @returnList TABLE 
 (
	 [ORIGINAL] nvarchar(MAX)
	 ,[EAN] nvarchar(500)
	 ,[CISTAHMOTNOST] numeric(19,5)
	 ,[MNOZSTVI] int
	 ,[EXPIRACE] nvarchar(500)
	 ,[SARZE] nvarchar(500)
 )
AS
BEGIN

	declare 
		@id int
		,@name nvarchar(max)
		,@strGS1part nvarchar(max)
		,@strGS1partLength int
		,@strTmp nvarchar(max)

	declare @EAN nvarchar(30)
	declare @CISTAHMOTNOST nvarchar(30)
	declare @MNOZSTVI nvarchar(30)
	declare @EXPIRACE nvarchar(30)
	declare @SARZE nvarchar(30)

	--SELECT 
	--@EAN = Substring(Name,3,14)
	--, @CISTAHMOTNOST = Substring(Name,19, LEN(Name) - 18)
	--FROM dbo.splitstring(@stringToParse, CHAR(29)) where ID = 0


	--SELECT 
	--@EXPIRACE = Substring(Name,3,6)
	--, @SARZE = Substring(Name,11, LEN(Name) - 10 )
	--FROM dbo.splitstring(@stringToParse, CHAR(29)) where ID = 1

	-- JiS musi se to upravit ...
	--select * into #parts from dbo.splitstring(@stringToParse, CHAR(29))

	declare cParse cursor for
	Select p.ID, p.Name from dbo.splitstring(@stringToParse, CHAR(29)) p
	
	open cParse
	fetch next from cParse into @id, @name
	while @@FETCH_STATUS = 0
	begin
		set @strGS1part = @name
		set @strGS1partLength = LEN(@strGS1part)
		while LEN(@strGS1part) > 0
		begin
			set @strTmp = @strGS1part
			
			IF SUBSTRING(@strGS1part, 1, 2) = '02' --02 = GTIN n2 + n14
			BEGIN
				set @EAN = SUBSTRING(@strGS1part, 1 + 2, 14)					
				--nastavit zbytek ...
				set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 14, LEN(@strGS1part))	--celkem 16 znaku
				set @strGS1partLength = LEN(@strGS1part)
				CONTINUE --pokracuji od zacatku
			END
			
			IF SUBSTRING(@strGS1part, 1, 2) = '15'	--15 = Expiration n2+n6
			BEGIN
				set @EXPIRACE = SUBSTRING(@strGS1part, 1+2, 6)				
				set @strGS1part = SUBSTRING(@strGS1part,1 + 2 + 6, LEN(@strGS1part))	--celkem 8 znaku
				set @strGS1partLength = LEN(@strGS1part)
				CONTINUE --pokracuji od zacatku
			END			
			
			IF SUBSTRING(@strGS1part, 1, 2) = '10'	--10 = LOT/SARZE : n2+an..20 (promenny pocet, musi byt na konci, takze ctu vse az do konce, pripadne max 20 znaku a koncim)
			BEGIN
				set @SARZE = SUBSTRING(@strGS1part, 1+2, 20)
				--set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 20, LEN(@strGS1part))	--celkem max 20 znaku
				BREAK --koncim, protoze dal jiz nic byt nesmi ...
			END			

			IF SUBSTRING(@strGS1part, 1, 2) = '37'	--37 = promenne mnozstvi n2+n..8 (promenny pocet, musi byt na konci, takze ctu vse az do konce, pripadne max 8 znaku a koncim)
			BEGIN
				set @MNOZSTVI = CONVERT(int,SUBSTRING(@strGS1part, 1+2, 8))
				--set @strGS1part = SUBSTRING(@strGS1part, 1 + 2 + 8, LEN(@strGS1part))	--celkem max 8 znaku
				BREAK --koncim, protoze dal jiz nic byt nesmi ...
			END			
			
			IF SUBSTRING(@strGS1part, 1, 3) = '310'	--310X = 310X čistá hmotnost / kg / n4+n6
			BEGIN
				declare 
					@descarka int
				set @descarka = CONVERT(int, SUBSTRING(@strGS1part, 1 + 3, 1))
				set @CISTAHMOTNOST = CONVERT(numeric(19,5), SUBSTRING(@strGS1part, 1 + 3 + 1, 6)) / @descarka
				set @strGS1part = SUBSTRING(@strGS1part, 1 + 3 + 1 + 6, LEN(@strGS1part))	--celkem max 10 znaku
				CONTINUE --pokracuji od zacatku
			END			
			
			-- Test jestli je jeste co parsovat ...			
			IF LEN(@strTmp) = LEN(@strGS1part)
			BEGIN
				BREAK
			END					
		end					
				
		fetch next from cParse into @id, @name
	end
	
	close cParse	

 INSERT INTO @returnList
 SELECT @stringToParse, @EAN , @CISTAHMOTNOST, @MNOZSTVI , @EXPIRACE , @SARZE
 
 RETURN
END

GO
/**************************************************************************************/