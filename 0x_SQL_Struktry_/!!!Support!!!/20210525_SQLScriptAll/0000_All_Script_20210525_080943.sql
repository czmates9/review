/****** Object:  Table [dbo].[CZMST_METAINFO]  ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_METAINFO](
	[DB_Version] [nvarchar](20) NOT NULL,
	[MST_Version] [nvarchar](20) NOT NULL,
	[IS_Provider] [nvarchar](50) NOT NULL,
	[Updated] [datetime] NULL DEFAULT (getdate())
) ON [PRIMARY]

/****************************************************/
/***** Vychozi metainformace **********************************************************/
INSERT INTO [dbo].[CZMST_METAINFO]([DB_Version],[MST_Version],[IS_Provider]) VALUES('7.?','7.?',' MES Server Pohoda Cipisek');
GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[CalculateCheckDigitModulo10]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CalculateCheckDigitModulo10]
(
    @StringToCheck NVARCHAR(max)
)
RETURNS TABLE WITH SCHEMABINDING
RETURN
-- Calculate the check digit for a UPC
WITH Tally (n) AS
(
    SELECT TOP (LEN(@StringToCheck))
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL))
    -- 8,000 row tally table
    FROM (VALUES (0),(0),(0),(0),(0),(0),(0),(0)) a(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) b(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) c(n)
    CROSS JOIN (VALUES (0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) d(n)
)
SELECT StringToCheck=@StringToCheck
    ,CheckDigit = (10 -
        SUM(CASE n%2 
            WHEN 1 THEN 3 
            ELSE 1 END * SUBSTRING(@StringToCheck, n, 1))
        % 10
        ) % 10 -- When check digit is 10 (remainder=0) use 0 as the check digit
FROM Tally;

GO

/****************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[fask_func_convert_to_datetime]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fask_func_convert_to_datetime](@timedone nvarchar(6), @datedone nvarchar(8))
RETURNS DATETIME-- NVARCHAR(17)
AS 
BEGIN
	IF @timedone is null
	BEGIN
		return null;
	END	
	
	IF @datedone is null
	BEGIN
		return null;
	END
	
	if LEN(@timedone) < 6
	BEGIN
		return null;
	END
	
	if LEN(@datedone) < 8
	BEGIN
		return null;
	END
	/*
	DECLARE @dat varchar(14) = '20151003092957';
	declare @year varchar(4) = substring(@dat, 1, 4);
	declare @month varchar(2) = substring(@dat, 5, 2);
	declare @day varchar(2) = substring(@dat, 7, 2);
	declare @hour varchar(2) = substring(@dat, 9, 2);
	declare @min varchar(2) = substring(@dat, 11, 2);
	declare @sec varchar(2) = substring(@dat, 13, 2);
	*/
	--DECLARE @dat varchar(14) = '20151003092957';
	declare @year nvarchar(4) = substring(@datedone, 1, 4);
	declare @month nvarchar(2) = substring(@datedone, 5, 2);
	declare @day nvarchar(2) = substring(@datedone, 7, 2);
	declare @hour nvarchar(2) = substring(@timedone, 1, 2);
	declare @min nvarchar(2) = substring(@timedone, 3, 2);
	declare @sec nvarchar(2) = substring(@timedone, 5, 2);
	
	/* yyyy-mm-dd hh:mi:ss (24h) (ODBC canonical) */
	return CONVERT(datetime, @year + '-' + @month + '-' + @day + ' ' + @hour + ':' + @min + ':' + @sec, 120)
END

GO

/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[splitstring]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit NVARCHAR(MAX) , @delimiter CHAR(1)=',')
RETURNS
 @returnList TABLE 
 (
 [ID] int,
 [Name] [nvarchar] (500)
 )
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @ID int 
 DECLARE @pos INT
 
    SET @ID = 0;

 WHILE CHARINDEX(@delimiter, @stringToSplit) > 0
 BEGIN
 
  SELECT @pos  = CHARINDEX(@delimiter, @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @ID, @name 
  
    SET @ID = @ID + 1;

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @ID, @stringToSplit
 
 RETURN
END

GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_GetDavkyByCarKody] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 22.9.2020
-- Description:	Funkce upravena pro Hanibal
-- =============================================
CREATE FUNCTION [dbo].[FASK_GetDavkyByCarKody]
(	
	-- Add the parameters for the function here
	@TerminalID int ,
	@SkladID int,
	@CarKod nvarchar(500)
)
RETURNS @Prijemky TABLE
(
	[PONUMBER] [nvarchar](30) not NULL,
	[Name] [nvarchar](31) NULL,
	[CZ_CarKod] [nvarchar](70) not NULL,
	[DateTime] [datetime] NULL,
	[Desc] [nvarchar](100) not NULL

)
AS
BEGIN

IF (@CarKod = '')
BEGIN

insert into @Prijemky ([PONUMBER], [Name], [CZ_CarKod], [DateTime],[Desc])
		SELECT 
		--*
		Left(o.Cislo, 30) as PONUMBER,
		Left(o.Firma, 31) as [Name],
		Left(o.Cislo,70) as CZ_CarKod,
		o.DatCreate as [datetime],
		Left(isnull(o.SText,''),100) as [Desc]
		FROM StwPh_04535667_2020.dbo.OBJ o
join (
select op2.RefAg--, Count(*) pp 
	from (
		--select distinct op0.refag, op0.refskz from StwPh_04535667_2020.dbo.objpol op0
		select 
		op0.refag, 
		op0.refskz, 
		Sum(op0.Mnozstvi) - SUM(op0.Dodano) ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
		from StwPh_04535667_2020.dbo.objpol op0
		group by op0.RefAg, op0.RefSKz
	) op2
	where 
		1=1
		AND op2.refSkz in (
			select s.id from StwPh_04535667_2020.dbo.SKz s
		where 
		--s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
		--and
		s.refsklad=@SkladID --cislo skladu
	)
		and op2.ZbyvaDodat > 0
group by op2.RefAg--, op.refskz
--having Count(*) >= (SELECT count(*) FROM dbo.splitstring(@CarKod, default))
) as objscarkody on objscarkody.refag = o.id
where 
	1=1
	and o.reltpobj in (2)	
	and	o.vyrizeno=0 
order by o.datcreate 

--	return

END

 ELSE

 BEGIN

 insert into @Prijemky ([PONUMBER], [Name], [CZ_CarKod], [DateTime],[Desc])
		SELECT 
		--*
		Left(o.Cislo, 30) as PONUMBER,
		Left(o.Firma, 31) as [Name],
		Left(o.Cislo,70) as CZ_CarKod,
		o.DatCreate as [datetime],
		Left(isnull(o.SText,''),100) as [Desc]
		FROM StwPh_04535667_2020.dbo.OBJ o
join 
(
select op2.RefAg--, Count(*) pp 
	from (
		--select distinct 
		--	op0.refag, 
		--	op0.refskz, 
		--	op0.Mnozstvi - op0.Dodano ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
		--	from StwPh_04535667_2020.dbo.objpol op0
		select 
			op0.refag, 
			op0.refskz, 
			Sum(op0.Mnozstvi) - SUM(op0.Dodano) ZbyvaDodat		-- nekompletni dodani materialu/zbozi/polozky
			from StwPh_04535667_2020.dbo.objpol op0
			group by op0.RefAg, op0.RefSKz
		) op2
	where 
		1=1
		and op2.refSkz in (
			select s.id from StwPh_04535667_2020.dbo.SKz s
		where 
		s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
		and
		s.refsklad=@SkladID --cislo skladu
	)
		and op2.ZbyvaDodat > 0
group by op2.RefAg--, op.refskz
having Count(*) >= (SELECT count(*) FROM dbo.splitstring(@CarKod, default))
) as objscarkody on objscarkody.refag = o.id
where 
	1=1
	and o.reltpobj in (2)	
	and	o.vyrizeno=0 
order by o.datcreate 


END

 	return

END

GO
/**********************************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_GetDodavatelByCarKody]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Jiří Skřivánek a Tadeas Divacky
-- Create date: 14.12.2018
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[FASK_GetDodavatelByCarKody]
(	
	-- Add the parameters for the function here
	@TerminalID int ,
	@SkladID int,
	@CarKod nvarchar(500)
)
RETURNS @Prijemky TABLE
(
	[odb_id] [nvarchar](12) not NULL,
	[odb_desc] [nvarchar](31) NULL,
	[odb_typ] [nvarchar](3) NULL,
	[odb_carcode] [nvarchar](21) NULL,
	[odb_ico] [nvarchar](20) NULL,
	[mena_ID] [nvarchar](10) NULL,
		[DEX_ROW_ID] int not NULL
)
AS
BEGIN


 insert into @Prijemky ([odb_id], [odb_desc], [odb_typ], [odb_carcode],[odb_ico], [mena_ID], [DEX_ROW_ID])
		SELECT 
		a.ID as odb_id,
		isnull(Left(a.Firma, 31),'') as odb_desc,
		0 as odb_typ,
		isnull(Left(a.Cislo, 21),'') as odb_carcode,
		isnull(Left(a.ICO, 20),'') as odb_ico,
		null as mena_ID,
		a.ID as DEX_ROW_ID
		FROM StwPh_04535667_2020.dbo.AD a
		where a.ID in 
		(
		
select y.RefAD
--, count(*) pocetSpolecnych
from 
(		
	select x.*
	, count(*) pocetSpolecnych
	from
	(
			select s.RefAD, s.ID as SkzID
			from StwPh_04535667_2020.dbo.SKz s
			where s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default)) and s.refsklad=@SkladID

			union all

			select RefAD, RefAg as SkzID
			from StwPh_04535667_2020.dbo.SKzNC n
			where n.RefAg in (
				select s.ID from StwPh_04535667_2020.dbo.SKz s
				where 
				s.ean in (SELECT Name FROM dbo.splitstring(@CarKod, default))
				and
				s.refsklad=@SkladID
			)
	) as x
	group by x.RefAD, x.SkzID
) as y
group by y.RefAD
having count(*) >= (SELECT Count(*) FROM dbo.splitstring(@CarKod, default))

		)


 	return

END

GO
/**************************************************************************************/
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
/****** Object:  UserDefinedFunction [dbo].[fask_GS1_AI_GET]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 5.6.2019
-- Description:	Dekoduje z retezce pozadovany gs1 ai hodnotu
-- =============================================
CREATE FUNCTION [dbo].[fask_GS1_AI_GET] 
(
	-- Add the parameters for the function here
	@ai nvarchar(max),
	@barcode nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result nvarchar(max)
	SET @Result = null

	IF (ltrim(rtrim(ISNULL(@barcode, ''))) = '')
	BEGIN
		Return @Result
	END

	-- Add the T-SQL statements to compute the return value here
	IF (@ai = 'EAN')
	begin
		SELECT @Result = gs1.EAN from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'EXPIRACE')
	begin
		SELECT @Result = gs1.EXPIRACE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'SARZE')
	begin
		SELECT @Result = gs1.SARZE from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'VAHA')
	begin
		SELECT @Result = gs1.CISTAHMOTNOST from FASK_ParseGS1(@barcode) gs1
	end

	IF (@ai = 'MNOZSTVI')
	begin
		SELECT @Result = gs1.MNOZSTVI from FASK_ParseGS1(@barcode) gs1
	end

	-- Return the result of the function
	RETURN @Result

END

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Corrects]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Corrects](
	[id] [int] NOT NULL,
	[desc] [nvarchar](100) NOT NULL,
	[TMFrom] [real] NULL,
	[TMTo] [real] NULL,
	[Production] [tinyint] NOT NULL,
	[ProductionType] [tinyint] NULL,
 CONSTRAINT [PK_correct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((0)) FOR [TMFrom]
GO
ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((1)) FOR [Production]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZ_UKOL] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CreatorID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[State] [nvarchar](1) NOT NULL,
	[Kind] [nvarchar](1) NULL,
	[Type] [nvarchar](2) NULL,
	[Priority] [int] NOT NULL,
	[PartnerID] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZ_UKOL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N'Nezadáno') FOR [Description]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT (N'N') FOR [State]
GO
ALTER TABLE [dbo].[CZ_UKOL] ADD  DEFAULT ((3)) FOR [Priority]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZ_UKOL_ServiceMan]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_ServiceMan](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[DESC] [nvarchar](100) NULL,
	[OPERATOR] [nvarchar](50) NULL,
	[OPERATOR_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZ_UKOL_ServiceMan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZ_UKOL_STATE  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_STATE](
	[State] [nvarchar](1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[IsStart] [bit] NOT NULL,
	[IsEnd] [bit] NOT NULL,
	[Color] [nvarchar](7) NULL,
 CONSTRAINT [PK_CZ_UKOL_STATE] PRIMARY KEY CLUSTERED 
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZ_UKOL_UZIV]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_UZIV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV] ADD  DEFAULT (N'N') FOR [State]
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[CZ_UKOL_UZIV_HIST]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_UZIV_HIST](
	[ID_HIST] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV_HIST] PRIMARY KEY CLUSTERED 
(
	[ID_HIST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_CountEntries]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_CountEntries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TBL] [nvarchar](20) NOT NULL,
	[CountEntries] [int] NULL,
	[BLOCKED] [bit] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[TBL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_CountEntries] ADD  DEFAULT ((0)) FOR [BLOCKED]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_DI]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DI](
	[CountEntries] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[STR_ID] [nvarchar](30) NULL,
	[DOC_ID] [nvarchar](12) NULL,
	[DOC_ID2] [nvarchar](12) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[TAXAMPIE] [numeric](19, 5) NULL,
	[AMOUNPIE] [numeric](19, 5) NULL,
	[WITHTAX] [tinyint] NULL,
	[PRICEX] [tinyint] NULL,
	[mena_ID] [nvarchar](10) NULL,
	[TAXAMPIEM] [numeric](19, 5) NULL,
	[AMOUNPIEM] [numeric](19, 5) NULL,
	[mena_IDM] [nvarchar](10) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[LOCNCODEDEST] [nvarchar](11) NULL,
	[SKL_ID_DEST] [nvarchar](20) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_DI_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[STR_ID] [nvarchar](30) NULL,
	[DOC_ID] [nvarchar](12) NULL,
	[DOC_ID2] [nvarchar](12) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[TAXAMPIE] [numeric](19, 5) NULL,
	[AMOUNPIE] [numeric](19, 5) NULL,
	[WITHTAX] [tinyint] NULL,
	[PRICEX] [tinyint] NULL,
	[mena_ID] [nvarchar](10) NULL,
	[TAXAMPIEM] [numeric](19, 5) NULL,
	[AMOUNPIEM] [numeric](19, 5) NULL,
	[mena_IDM] [nvarchar](10) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[LOCNCODEDEST] [nvarchar](11) NULL,
	[SKL_ID_DEST] [nvarchar](20) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_DI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[CZMST_DI_RFID]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_DI_RFID](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_DI_RFID] ADD  DEFAULT (getdate()) FOR [Created_S]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_DIH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DIH](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[CZMST_DIH_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DIH_HISTORY](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_EventsTypes]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_EventsTypes](
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[edesc] [nvarchar](100) NULL,
	[ebarcode] [nvarchar](21) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_EventsUser]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_EventsUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eguid] [uniqueidentifier] NOT NULL,
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[etime] [datetime] NOT NULL,
	[termid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[modul] [nvarchar](20) NULL,
	[countentries] [int] NULL,
	[docnmbr] [nvarchar](30) NULL,
	[itemnmbr] [nvarchar](40) NULL,
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[eguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Buffer]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Buffer](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Hlavicka]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka](
	[ID] [uniqueidentifier] NOT NULL,
	[Rozpracovano] [tinyint] NOT NULL DEFAULT ((0)),
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[Rozpracovano] [tinyint] NOT NULL DEFAULT ((0)),
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Polozky]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Polozky](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Baleni_Polozky_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Hlavicka]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Hlavicka](
	[ID] [uniqueidentifier] NOT NULL,
	[PrepravceID] [nvarchar](20) NULL,
	[PrepravceSPZ] [nvarchar](20) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka] ADD  DEFAULT ((0)) FOR [Rozpracovano]
GO
ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Hlavicka_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Hlavicka_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[PrepravceID] [nvarchar](20) NULL,
	[PrepravceSPZ] [nvarchar](20) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Hlavicka_HISTORY] ADD  DEFAULT ((0)) FOR [Rozpracovano]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Polozky]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Polozky](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDHB] [uniqueidentifier] NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Polozky] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Expedice_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Polozky_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[IDH] [uniqueidentifier] NOT NULL,
	[IDHB] [uniqueidentifier] NULL,
	[IDPol] [uniqueidentifier] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYMJ] [numeric](19, 5) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[NMBRBAL] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Expedice_Polozky_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I1]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I1](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[ITEMDESC] [nvarchar](100) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[DMJ] [nvarchar](200) NULL,
	[DATEDONE] [datetime] NOT NULL,
	[IntegerValue] [smallint] NOT NULL,
	[TIMESPRT] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Find] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TerminalID] [tinyint] NOT NULL,
	[O_TID] [tinyint] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I1H]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I1H](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[State] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I1P]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_I1P](
	[Countentries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[TAX] [numeric](19, 5) NULL,
	[PRICE0] [numeric](19, 5) NULL,
	[PRICE1] [numeric](19, 5) NULL,
	[PRICE2] [numeric](19, 5) NULL,
	[PRICE3] [numeric](19, 5) NULL,
	[PRICE4] [numeric](19, 5) NULL,
	[PRICE5] [numeric](19, 5) NULL,
	[PRICE0H] [nvarchar](50) NULL,
	[PRICE1H] [nvarchar](50) NULL,
	[PRICE2H] [nvarchar](50) NULL,
	[PRICE3H] [nvarchar](50) NULL,
	[PRICE4H] [nvarchar](50) NULL,
	[PRICE5H] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I2]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I2](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I3]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I3](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[VENDORID] [nvarchar](15) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[VENDNAME] [nvarchar](31) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_I4]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I4](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[QUANTITYMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[O_Checked] [bit] NOT NULL DEFAULT ((0)),
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Palety]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Palety](
	[ID] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL DEFAULT (N''),
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PE_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL,
	[CZ_REZ2_Track] [tinyint] NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT (N'') FOR [SERLTNUM]
GO
ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ1_Track]
GO
ALTER TABLE [dbo].[CZMST_PE_HISTORY] ADD  DEFAULT ((0)) FOR [CZ_REZ2_Track]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PE_SN]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE_SN](
	[CountEntries] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PEH]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PEH](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[DOKLTYPE] [nvarchar](10) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_PEH] ADD  DEFAULT ('') FOR [DOKLTYPE]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PEH_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PEH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[DOKLTYPE] [nvarchar](10) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_PEH_HISTORY] ADD  DEFAULT ('') FOR [DOKLTYPE]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PI]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PI](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ORD] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[USER_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PI_F]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PI_F](
	[IMG_NAME] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[CZMST_PI_F_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PI_F_HISTORY](
	[IMG_NAME] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PI_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ORD] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[USER_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PIH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_PIH](
	[CountEntries] [int] NOT NULL,
	[DATUMDOKLADU] [datetime] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_PIH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_PIH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[DATUMDOKLADU] [datetime] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_RFID_ITEMS]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_RFID_ITEMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[SEQUENCENMBR] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_RFID_ITEMS] ADD  DEFAULT ((0)) FOR [SEQUENCENMBR]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_RFID_ITEMS_ASSIGNS]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_RFID_ITEMS_ASSIGNS] ADD  DEFAULT (getdate()) FOR [Created_S]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SE]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[Note] [nvarchar](100) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[QTYPAL] [numeric](19, 5) NULL,
	[PRIORITY] [tinyint] NOT NULL DEFAULT ((3)),
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Realization_Start] [datetime] NULL DEFAULT (getdate()),
	[Realization_Stop] [datetime] NULL DEFAULT (getdate()),
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SE_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[Note] [nvarchar](100) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[QTYPAL] [numeric](19, 5) NULL,
	[PRIORITY] [tinyint] NOT NULL DEFAULT ((3)),
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SE_SN]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE_SN](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SE_SN] ADD  DEFAULT ((1)) FOR [QTY]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SE_SN_HISTORY] ADD  CONSTRAINT [DF__CZMST_SE_SN__QTY__40058253]  DEFAULT ((1)) FOR [QTY]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SEH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SEH](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SEH_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SEH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Cinnost]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Cinnost](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[TYPEVALUE] [nvarchar](20) NULL,
	[Mandatory] [tinyint] NOT NULL,
	[RequiredLength] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_Servis_Cinnost] ADD  DEFAULT ((1)) FOR [Mandatory]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_CinnostNext]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_CinnostNext](
	[ID] [nvarchar](20) NOT NULL,
	[IDNext] [nvarchar](20) NULL,
	[IDValue] [nvarchar](20) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Dyn_Table]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Dyn_Table](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_Servis_Dyn_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Dynamic_Table_Definition]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Dynamic_Table_Definition](
	[FullName] [nvarchar](50) NOT NULL,
	[TypeName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_Dynamic_Table_Definition] PRIMARY KEY CLUSTERED 
(
	[FullName] ASC,
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Okruh]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Okruh](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[Barcode] [nvarchar](50) NULL,
	[ZdrojSeznamID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_Okruh] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Predloha]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Predloha](
	[CountEntries] [int] NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NOT NULL,
	[UserID] [int] NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT ((0)) FOR [Rozpracovano]
GO
ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[CZMST_Servis_Stav]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Stav](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZMST_Servis_Stav] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_StavNext]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_StavNext](
	[ID] [nvarchar](20) NOT NULL,
	[IDNext] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_StavNext_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[IDNext] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_Zdroj]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Zdroj](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Misto] [nvarchar](20) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Type] [nvarchar](2) NULL,
 CONSTRAINT [PK_CZMST_Servis_Zdroj] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_ZdrojPohyb]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojPohyb](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NOT NULL,
	[IDUser] [int] NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL,
	[dateeveS] [datetime] NULL,
	[dateExported] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_Servis_ZdrojPohyb_Guid] ON [dbo].[CZMST_Servis_ZdrojPohyb]
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


ALTER TABLE [dbo].[CZMST_Servis_ZdrojPohyb] ADD  DEFAULT (getdate()) FOR [dateeveS]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_ZdrojSeznam]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojSeznam](
	[ID] [nvarchar](20) NOT NULL,
	[ZdrojID] [nvarchar](20) NOT NULL,
	[Poradi] [int] NULL,
	[IDStav] [nvarchar](20) NULL,
	[IDCinnost] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZMST_Servis_ZdrojSeznam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ZdrojID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_Servis_ZdrojStav]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojStav](
	[IDZdroj] [nvarchar](20) NOT NULL,
	[IDStav] [nvarchar](20) NOT NULL,
	[IDCinnost] [nvarchar](20) NULL,
	[Modified] [datetime] NOT NULL,
	[IDTerminal] [int] NULL,
	[IDUser] [int] NULL,
	[GUID] [uniqueidentifier] NULL,
	[CinnostValue] [nvarchar](50) NULL,
	[CinnostType] [nvarchar](2) NULL,
	[CinnostOznaceni] [nvarchar](50) NULL,
	[CountEntries] [int] NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NULL,
	[GPS_X] [float] NULL,
	[GPS_Y] [float] NULL,
	[GPS_Z] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SI]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SI](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ODBER_ID] [nvarchar](12) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USER_ID] [int] NOT NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SI_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ODBER_ID] [nvarchar](12) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USER_ID] [int] NOT NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SI_RFID]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SI_RFID](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SEQUENCENMBR] [int] NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NULL,
	[DOCUMENTNMBR] [nvarchar](20) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[M_ID] [nvarchar](100) NULL,
	[M_TID] [nvarchar](100) NULL,
	[M_EPC] [nvarchar](100) NULL,
	[M_USER] [nvarchar](150) NULL,
	[M_RESERVED] [nvarchar](100) NULL,
	[O_M_ID] [nvarchar](100) NULL,
	[O_M_TID] [nvarchar](100) NULL,
	[O_M_EPC] [nvarchar](100) NULL,
	[O_M_USER] [nvarchar](150) NULL,
	[O_M_RESERVED] [nvarchar](100) NULL,
	[TerminalID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Created_T] [datetime] NOT NULL,
	[Created_S] [datetime] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_SI_RFID] ADD  DEFAULT (getdate()) FOR [Created_S]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SIH] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SIH](
	[CountEntries] [int] NOT NULL,
	[TISKARNA_NAME] [nvarchar](30) NULL,
	[PRAC_ID] [nvarchar](30) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SIH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SIH_HISTORY](
	[CountEntries] [int] NOT NULL,
	[TISKARNA_NAME] [nvarchar](30) NULL,
	[PRAC_ID] [nvarchar](30) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SkladLokace_LokaceTypy]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceTypy](
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[IS_RECEIVE] [bit] NOT NULL,
	[IS_DEFAULT] [bit] NOT NULL,
	[IS_NORMAL] [bit] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SkladLokace_LokaceVariantySortiment]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SkladLokace_Mapa]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_Mapa](
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[TYPE] [nvarchar](2) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SkladLokace_Stav]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_Stav](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[QTYSHPPD_DEF] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DATECHANGE] [datetime] NOT NULL,
	[EXPIRATION] [datetime] NULL,
	[QTYSHPPD_DEF_DATE] [datetime] NULL,
	[QTY_OWNER] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[PRAC_ID_OWNER] [nvarchar](30) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SkladLokace_StavPohyb]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_StavPohyb](
	[id] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[POHYB_TYPE] [nvarchar](2) NULL,
	[POHYB_SRC] [nvarchar](2) NULL,
	[SOURCE] [nvarchar](2) NULL,
	[CountEntries] [int] NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[SKL_ID_SRC] [nvarchar](20) NULL,
	[SKL_ID_DST] [nvarchar](20) NULL,
	[LOCNCODE_SRC] [nvarchar](11) NULL,
	[LOCNCODE_DST] [nvarchar](11) NULL,
	[UserID] [int] NOT NULL,
	[TermID] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[dateeveS] [datetime] NOT NULL,
	[dateeveT] [datetime] NOT NULL,
 CONSTRAINT [PK_CZMST_SkladLokace_StavPohyb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

CREATE NONCLUSTERED INDEX [IX_FASK_CZMST_SkladLokace_StavPohyb_Guid] ON [dbo].[CZMST_SkladLokace_StavPohyb]
(
	[guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SSCC_PARAMETERS]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SSCC_PARAMETERS](
	[ID_SSCC] [int] NOT NULL,
	[DESC_SSCC] [nvarchar](20) NULL,
	[LV] [numeric](1, 0) NULL,
	[GCP] [numeric](9, 0) NULL,
	[GCP_count] [numeric](9, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_SSCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_SSCC_SEQUENCE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SSCC_SEQUENCE](
	[seq_id] [int] NOT NULL,
	[sequence_count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[seq_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[CZMST_get_sscc_func]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE FUNCTION [dbo].[CZMST_get_sscc_func] 
(
	@seq_id int,
	@sscc_count int 
)
RETURNS nvarchar(20)
AS
BEGIN

		DECLARE @LV numeric(1,0);
		declare @GCP numeric(9,0);
		declare @GCP_count numeric(9,0);
				
		select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		from CZMST_SSCC_PARAMETERS as par
		where par.ID_SSCC = @seq_id
		
		declare @sscc nvarchar(20)
					
		set @sscc = '00'
		set @sscc = @sscc + convert(nvarchar(1),@LV)
		set @sscc = @sscc + RIGHT('000000000' + convert(nvarchar(9),@GCP), @GCP_count)
		--set @sscc = @sscc + convert(nvarchar(9),@sscc_count)
		set @sscc = @sscc + RIGHT('000000000' + CONVERT(nvarchar(20), @sscc_count), (20-1-LEN(@sscc)))
		
		set @sscc = @sscc + convert(nvarchar(1),(select CheckDigit from CalculateCheckDigitModulo10(@sscc)))
		
		RETURN @sscc
		
END

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_TERMINAL]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_TERMINAL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ID_TISKARNA] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_TERMINAL_AKT]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_TERMINAL_AKT](
	[ID_TERMINAL] [int] NOT NULL,
	[IP] [nvarchar](100) NOT NULL,
	[DATEREQ] [datetime] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_TERMINAL_DEFINITION]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_TERMINAL_DEFINITION](
	[ID_TERMINAL] [int] NOT NULL,
	[DB_TYPE] [nvarchar](10) NOT NULL DEFAULT ('Sqlite'),
 CONSTRAINT [PK_CZMST_TERMINAL_DEFINITION] PRIMARY KEY CLUSTERED 
(
	[ID_TERMINAL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_TISKARNA]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_TISKARNA](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[LOCATION] [nvarchar](50) NULL,
	[IP] [nvarchar](100) NULL,
	[PORT] [nvarchar](10) NULL,
	[COM] [nvarchar](50) NULL,
	[SOUBOR] [nvarchar](max) NULL,
	[TIMEOUT] [int] NULL,
	[BARCODE] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST090]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST090](
	[odb_id] [nvarchar](12) NOT NULL,
	[odb_desc] [nvarchar](31) NULL,
	[odb_typ] [nvarchar](3) NULL,
	[odb_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[odb_ico] [nvarchar](20) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[odb_misto] [nvarchar](100) NULL DEFAULT (''),
	[odb_ulice] [nvarchar](100) NULL DEFAULT (''),
	[odb_cisloOr] [nvarchar](15) NULL DEFAULT (''),
	[odb_psc] [nvarchar](15) NULL DEFAULT (''),
	[odb_dic] [nvarchar](15) NULL DEFAULT (''),
	[odb_Odberatel] [bit] NULL DEFAULT ((0)),
	[odb_Dodavatel] [bit] NULL DEFAULT ((0))
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST091]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST091](
	[str_id] [nvarchar](30) NOT NULL,
	[str_desc] [nvarchar](40) NULL,
	[str_typ] [nvarchar](3) NULL,
	[str_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[skl_id] [nvarchar](20) NULL,
	[odb_id] [nvarchar](12) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST092]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST092](
	[doc_id] [nvarchar](12) NOT NULL,
	[doc_id2] [nvarchar](12) NOT NULL DEFAULT (''),
	[doc_desc] [nvarchar](31) NULL,
	[doc_typ] [nvarchar](3) NULL,
	[doc_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL DEFAULT (''),
	[cfg_odb] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_str] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_prac] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_mn2sn] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_disp] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_disp_dest] [tinyint] NOT NULL DEFAULT ((0)),
	[cfg_palety] [tinyint] NOT NULL,
	[cfg_paleta_id] [tinyint] NOT NULL,
	[cfg_zakazka_id] [tinyint] NOT NULL,
	[cfg_mena_id] [tinyint] NULL,
	[cfg_tisk] [tinyint] NOT NULL,
	[cfg_prevod_sklad] [tinyint] NOT NULL,
	[cfg_tisk_soupis] [tinyint] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[cfg_lokace] [tinyint] NULL,
	[cfg_lokace_ciselnik] [tinyint] NULL,
	[cfg_lokace_dest] [tinyint] NULL,
	[cfg_onl_dop_pal] [tinyint] NULL,
	[cfg_onl_over_lokace] [tinyint] NULL,
	[cfg_onl_over_lokace_dest] [tinyint] NULL,
	[cfg_mnozstvi_ze_zbozi] [tinyint] NULL,
	[cfg_predvyplnit_mnozstvi] [tinyint] NULL,
	[cfg_skl_id_dest] [tinyint] NULL,
	[predvyplnit_skl_id_dest] [nvarchar](20) NULL,
	[cfg_lok_mech] [tinyint] NULL,
	[cfg_lok_mech_pohyb_type] [nvarchar](1) NULL,
	[cfg_skl_id_dest_prevzit] [tinyint] NULL,
	[cfg_lokace_dest_ciselnik] [tinyint] NULL,
	[predvyplnit_locncodedest] [nvarchar](11) NULL,
	[cfg_sklady] [tinyint] NULL,
	[cfg_onl_dop_lokace_dest] [tinyint] NULL,
	[cfg_generovat_sn] [tinyint] NULL,
	[cfg_parsovat_ck] [tinyint] NULL,
	[cfg_sn_na_davku] [tinyint] NULL,
	[cfg_lok_mech_online_pohyby] [tinyint] NULL,
	[cfg_onl_palety_generovat] [tinyint] NULL,
	[cfg_tisk_palety] [tinyint] NULL,
	[cfg_sklady_zmena] [tinyint] NULL,
	[cfg_delka_SN] [int] NULL,
	[cfg_Navrh] [tinyint] NULL,
	[cfg_FIFO_FEFO_check] [tinyint] NULL DEFAULT(0)
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST093]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST093](
	[skl_id] [nvarchar](20) NOT NULL,
	[skl_desc] [nvarchar](40) NULL,
	[skl_typ] [nvarchar](3) NULL DEFAULT (''),
	[skl_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST094]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST094](
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[Barcode] [nvarchar](50) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST096]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST096](
	[prac_id] [nvarchar](30) NOT NULL,
	[prac_desc] [nvarchar](40) NULL,
	[prac_typ] [nvarchar](3) NULL,
	[prac_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST097]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST097](
	[mena_ID] [nvarchar](10) NOT NULL,
	[mena_text] [nvarchar](30) NOT NULL,
	[mena_hlavni] [tinyint] NOT NULL DEFAULT ((0)),
	[mena_kurz] [numeric](19, 5) NULL DEFAULT ((0)),
	[mena_kurzDatum] [date] NULL,
 CONSTRAINT [PK_CZMST097] PRIMARY KEY CLUSTERED 
(
	[mena_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZPRO_VPH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZPRO_VPH](
	[CountEntries] [int] NOT NULL DEFAULT ('1'),
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[SOPDESC] [nvarchar](100) NULL,
	[VNDDOCNMH] [nvarchar](21) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[DateProd] [smallint] NOT NULL,
	[Rez1] [nvarchar](50) NOT NULL,
	[Rez2] [nvarchar](50) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [tinyint] NOT NULL DEFAULT ((1)),
 CONSTRAINT [PK_CZPRO_VPH] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[CZPRO_VPP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZPRO_VPP](
	[CountEntries] [int] NOT NULL DEFAULT ('1'),
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMMJ] [nvarchar](5) NULL,
	[VNDDOCNMP] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL DEFAULT ((0)),
	[BarcodeP] [nvarchar](31) NOT NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYDOKON] [numeric](19, 5) NOT NULL DEFAULT ((0)),
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[TIMEMODE] [int] NOT NULL DEFAULT ((0)),
	[TIMEPREP] [real] NOT NULL,
	[TIMEUNIT] [real] NOT NULL,
	[DtProdT] [tinyint] NOT NULL,
	[DtProdL] [smallint] NOT NULL,
	[SerNumT] [tinyint] NOT NULL,
	[SerNumL] [smallint] NOT NULL,
	[VerT] [tinyint] NOT NULL,
	[VerL] [smallint] NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[LSTMod] [datetime] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL
 CONSTRAINT [PK_CZPRO_VPP] PRIMARY KEY CLUSTERED 
(
	[SOPNUMBE] ASC,
	[ITEMNMBR] ASC,
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_AGENDA]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_AGENDA](
	[AGENDAID] [nvarchar](50) NULL,
	[NAME] [nvarchar](50) NULL,
	[DESCIPTION] [nvarchar](max) NULL,
	[AUTH] [tinyint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/
/***** Role prav *********************************************************************/
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'K_', N'Hlavní Konzole', N'Přístup ke konzole', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'M_', N'MST', N'Přístup k terminálu MST', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'V_', N'Výroba', N'Přístup k terminálu Výroby', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'K_Admin_', N'Hlavní Konzole administrátor', N'Přístup jako admin ke konzole', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'K_IT_', N'Hlavní Konzole IT', N'Přístup do IT části Konzole ... ', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'M_Admin_', N'MST administrátor', N'Přistup k Admin časti terminalu MST', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'V_Admin_', N'Výroba administrátor', N'Přistup k Admin časti terminalu Výroba', 0)
GO
INSERT [dbo].[FASK_AGENDA] ([AGENDAID], [NAME], [DESCIPTION], [AUTH]) VALUES (N'V_VS_', N'Výroba Vedoucí směny', N'Přiznak Vedoucí smeny na  výrobe', 0)
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Events]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[description] [nvarchar](max) NULL,
	[barcodeReaded] [nvarchar](50) NOT NULL,
	[barcodeSended] [nvarchar](50) NOT NULL,
	[zakazka] [nvarchar](20) NULL,
	[popis] [nvarchar](10) NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[reportType] [nvarchar](1) NOT NULL,
	[isProcessed] [datetime] NULL,
	[IDO] [nvarchar](10) NULL,
	[scan1] [nvarchar](255) NULL,
	[scan2] [nvarchar](255) NULL,
	[scan3] [nvarchar](255) NULL,
	[sensor] [nvarchar](50) NULL,
	[material] [nvarchar](255) NULL,
	[productionGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_FASK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Events_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Logins]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Logins](
	[USERID] [nvarchar](20) NOT NULL,
	[firstname] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[psswd] [nvarchar](50) NOT NULL,
	[CREATED] [datetime] NULL DEFAULT (getdate()),
	[VALIDFROM] [datetime] NULL DEFAULT (getdate()),
	[VALIDTO] [datetime] NULL,
 CONSTRAINT [PK_FASK_Logins] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Logins_Auth]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_Logins_Auth](
	[USERID] [nvarchar](20) NOT NULL,
	[AGENDAID] [nvarchar](50) NULL,
	[AUTH] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/***** Vychodi SUPER admin uživatel FASK ***********************************************/
INSERT [dbo].[FASK_Logins] ([USERID], [firstname], [surname], [psswd]) VALUES (N'0', N'FASK', N'FASK', N'1')
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'M_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'K_IT_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'M_Admin_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'K_Admin_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'K_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'V_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'V_Admin_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'V_VS_', 0)
GO
INSERT [dbo].[FASK_Logins_Auth] ([USERID], [AGENDAID], [AUTH]) VALUES (N'0', N'_', 0)
GO
/***************************************************************************************/
/****** Object:  Table [dbo].[FASK_Machines]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Machines](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FASK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_POHODA_RESPONSE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_POHODA_RESPONSE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Resp_ID] [nvarchar](50) NULL,
	[Resp_Name] [nvarchar](255) NULL,
	[Resp_DateTime] [datetime] NULL,
	[Resp_ICO] [nvarchar](50) NULL,
	[Resp_Key] [nvarchar](50) NULL,
	[Resp_Note] [nvarchar](255) NULL,
	[Resp_State] [nvarchar](20) NULL,
	[Item_ID] [nvarchar](50) NULL,
	[Item_Note] [nvarchar](255) NULL,
	[Item_State] [nvarchar](20) NULL,
	[Item_Type] [nvarchar](50) NULL,
	[Detail_State] [nvarchar](20) NULL,
	[Detail_Type_Errno] [nvarchar](20) NULL,
	[Detail_Type_Note] [nvarchar](255) NULL,
	[Detail_Type_State] [nvarchar](20) NULL,
	[Detail_Type_VProcesed] [nvarchar](255) NULL,
	[Detail_Type_VRequested] [nvarchar](255) NULL,
	[Detail_Type_XPath] [nvarchar](255) NULL,
	[Produced_Detail_ID] [nvarchar](50) NULL,
	[Produced_Detail_ActionType] [nvarchar](50) NULL,
	[Produced_Detail_Code] [nvarchar](50) NULL,
	[Produced_Detail_Number] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_RADY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_RADY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Default] [bit] NULL,
	[PlatnostOd] [datetime] NULL,
	[PlatnostDo] [datetime] NULL,
	[Modul] [nvarchar](20) NOT NULL,
	[Modul_ID] [nvarchar](20) NULL,
	[Modul_ID2] [nvarchar](20) NULL,
	[Modul_Funkce] [nvarchar](20) NULL,
	[Rada_ID] [int] NULL,
	[Rada_Nazev] [nvarchar](100) NULL,
	[Rada_Prefix] [nvarchar](10) NULL,
	[Rada_Count] [int] NULL,
	[Filtr_SkladID] [nvarchar](20) NULL,
	[Filtr_UserID] [nvarchar](10) NULL,
	[Vloz_Stredisko0] [nvarchar](20) NULL,
	[Vloz_Stredisko1] [nvarchar](20) NULL,
	[Vloz_Cinnost] [nvarchar](20) NULL,
	[Vloz_Zakazka] [nvarchar](20) NULL,
	[Kontrola_Disponability] [bit] NULL,
	[Vyber_Typ_Prevodka] [tinyint] NULL,
	[Prodej_Prijemka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Vydejka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Prevodka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Prijemka_Tisk_ID_sablona] [int] NULL,
	[Prodej_Vydejka_Tisk_ID_sablona] [int] NULL,
	[Prodej_Prevodka_Tisk_ID_sablona] [int] NULL,
	[Import_Doklad_IS] [bit] NOT NULL DEFAULT(1)
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_StatusTypes]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_UserEvents]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[rez_1] [nvarchar](50) NULL,
	[rez_2] [nvarchar](50) NULL,
 CONSTRAINT [PK_FASK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FASK_UserEvents_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Vyroba_PVH]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_PVH](
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[SOPDESC] [nvarchar](100) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[Active] [tinyint] NOT NULL DEFAULT ((1)),
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TypDok] [nvarchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Vyroba_PVP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_PVP](
	[OBJ_NMBR] [nvarchar](17) NULL,
	[OBJ_DESC] [nvarchar](100) NULL,
	[OBJ_TYPE] [nvarchar](11) NULL,
	[OBJ_COMPANY] [nvarchar](255) NULL,
	[OBJ_DATE_FROM] [datetime] NULL,
	[OBJ_DATE_TO] [datetime] NULL,
	[OBJ_ORD] [int] NULL,
	[OBJ_ITEM_ORD] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DATE_ZAPLANOVANI] [datetime] NULL,
	[VP_PRPS] [bit] NOT NULL DEFAULT ((0)),
	[VP_PRPS_QTY] [numeric](19, 5) NULL,
	[VP_PRPS_SOPNUMBE] [nvarchar](100) NULL,
	[VP_PRDCT_QTY] [numeric](19, 5) NULL,
	[USERID] [int] NULL,
	[Ref_PVH] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_Vyroba_TP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_TP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_H] [nvarchar](50) NULL,
	[ID_L] [nvarchar](50) NULL,
	[ITEMNMBR_Def] [nvarchar](31) NULL,
	[DESC_Def] [nvarchar](100) NULL,
	[MJ_Def] [nvarchar](50) NULL,
	[ITEMNMBR_fol] [nvarchar](31) NULL,
	[DESC_Fol] [nvarchar](100) NULL,
	[MJ_Fol] [nvarchar](50) NULL,
	[koef] [nvarchar](50) NULL,
	[ID_USER] [nvarchar](50) NULL,
	[dateedit] [datetime] NULL,
	[alter] [nvarchar](1) NULL,
	[PUO] [nvarchar](1) NULL,
 CONSTRAINT [PK_FASK_Vyroba_TP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_ZASOBY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_ZASOBY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[DMJ] [nvarchar](200) NOT NULL CONSTRAINT [DF__FASK_ZASOBY__DMJ__20CCCE1C]  DEFAULT (''),
	[TAXRATE] [numeric](4, 2) NULL,
	[PRICE0] [numeric](18, 2) NULL,
	[PRICE1] [numeric](18, 2) NULL,
	[PRICE2] [numeric](18, 2) NULL,
	[PRICE3] [numeric](18, 2) NULL,
	[PRICE4] [numeric](18, 2) NULL,
	[PRICE5] [numeric](18, 2) NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_Rez1_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__21C0F255]  DEFAULT ((0)),
	[CZ_Rez2_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__22B5168E]  DEFAULT ((0)),
	[CZ_Rez3_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__23A93AC7]  DEFAULT ((0)),
	[CZ_Rez4_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__249D5F00]  DEFAULT ((0)),
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
	[REZ3] [nvarchar](50) NULL,
	[REZ4] [nvarchar](50) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[TIMEFROM] [datetime] NULL,
	[TIMETO] [datetime] NULL,
	[LSTMod] [datetime] NULL,
	[loginid] [nvarchar](20) NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL Default(0),
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_ZASOBY_MENY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_ZASOBY_MENY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[mena_ID] [nvarchar](10) NOT NULL,
	[PRICE] [numeric](18, 2) NULL,
	[PRICEX] [int] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_ZASOBY_PARAMETRY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_ZASOBY_PARAMETRY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[VPrFVTS] [bit] NOT NULL,
	[VPrFPTS] [bit] NOT NULL,
	[VPrFDTS] [bit] NOT NULL,
	[VPrFITS] [bit] NOT NULL,
	[VPrFXTS] [bit] NOT NULL,
	[RefVPrFVTS] [int] NULL,
	[RefVPrFPTS] [int] NULL,
	[RefVPrFDTS] [int] NULL,
	[RefVPrFITS] [int] NULL,
	[RefVPrFXTS] [int] NULL,
	[VPrTIMEPREP] [float] NULL,
	[VPrTIMEUNIT] [float] NULL,
	[RefVPrTIMEMODE] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFVTS]  DEFAULT ((0)) FOR [VPrFVTS]
GO
ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFPTS]  DEFAULT ((0)) FOR [VPrFPTS]
GO
ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFDTS]  DEFAULT ((0)) FOR [VPrFDTS]
GO
ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFITS]  DEFAULT ((0)) FOR [VPrFITS]
GO
ALTER TABLE [dbo].[FASK_ZASOBY_PARAMETRY] ADD  CONSTRAINT [DF_FASK_ZASOBY_PARAMETRY_VPrFXTS]  DEFAULT ((0)) FOR [VPrFXTS]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Groups]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Groups](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[INVENTUR]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[INVENTUR](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[KATEGORIE] [nvarchar](2) NULL,
	[I_CISLO] [nvarchar](11) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[STRED] [nvarchar](6) NULL,
	[OSOBA] [int] NULL,
	[LOKACE1] [nvarchar](25) NULL,
	[LOKACE2] [nvarchar](25) NULL,
	[KANCELAR] [nvarchar](6) NULL,
	[EAN] [nvarchar](50) NULL,
	[KUSU] [numeric](12, 0) NULL,
	[KLIC_LOK] [int] NULL,
	[ID_INV] [numeric](20, 0) NULL,
	[OS_ZPR] [nvarchar](20) NULL,
	[ID_TERM] [numeric](20, 0) NULL,
	[CAS_ZPR] [smalldatetime] NULL,
 CONSTRAINT [PK__INVENTUR__JKR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[KANCL]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KANCL](
	[KANCL] [nvarchar](6) NOT NULL,
	[STRE] [nvarchar](6) NULL,
	[TEXT] [nvarchar](50) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[EAN] [nvarchar](50) NULL,
 CONSTRAINT [PK__KANCL__JKR] PRIMARY KEY CLUSTERED 
(
	[KANCL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/

/****** Object:  Table [dbo].[LOKACE]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOKACE](
	[LOKACE1] [nvarchar](25) NULL,
	[LOKACE2] [nvarchar](25) NULL,
	[EANL] [nvarchar](20) NULL,
	[NAZEV] [nvarchar](50) NOT NULL,
	[KLIC_LOK] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[KLIC_KAT] [int] NULL,
	[KLIC_K_OLD] [int] NULL,
 CONSTRAINT [PK__LOKACE__JKR] PRIMARY KEY CLUSTERED 
(
	[KLIC_LOK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Machines]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Machines](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[MAJETEK]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MAJETEK](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[KATEGORIE] [nvarchar](2) NULL,
	[I_CISLO] [nvarchar](11) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[STRED] [nvarchar](6) NULL,
	[OSOBA] [int] NULL,
	[LOKACE1] [nvarchar](25) NULL,
	[LOKACE2] [nvarchar](25) NULL,
	[KANCELAR] [nvarchar](6) NULL,
	[EAN] [nvarchar](50) NULL,
	[KUSU] [numeric](12, 0) NULL,
	[KLIC_LOK] [int] NULL,
	[ID_INV] [nvarchar](20) NULL,
	[ID_TERM] [numeric](20, 0) NULL,
 CONSTRAINT [PK__MAJETEK__JKR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Operations]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Operations](
	[id] [nvarchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[OSOBY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OSOBY](
	[OSOBA_ZODP] [int] NOT NULL,
	[TITUL] [nvarchar](6) NULL,
	[PRIJMENI] [nvarchar](20) NULL,
	[JMENO] [nvarchar](12) NULL,
 CONSTRAINT [PK__OSOBY__JKR] PRIMARY KEY CLUSTERED 
(
	[OSOBA_ZODP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Production]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL DEFAULT (''),
	[ITEMMJ] [nvarchar](5) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NULL,
	[TIMEMODE] [int] NULL DEFAULT ((0)),
	[TIMEPREPSTART] [datetime] NULL,
	[TIMEPREPSTOP] [datetime] NULL,
	[TIMEPREP] [real] NULL,
	[TIMEUNIT] [real] NULL,
	[TIMESTART] [datetime] NULL,
	[TIMESTOP] [datetime] NULL,
	[TIMECORSTART] [datetime] NULL,
	[TIMECORSTOP] [datetime] NULL,
	[TIMECOR] [real] NULL,
	[TIMECRID] [int] NULL,
	[TIMECRIDTYPE] [tinyint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[operationid] [nvarchar](16) NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[description] [nvarchar](max) NULL,
	[BarcodeP] [nvarchar](31) NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[ISOK] [datetime] NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[SOUBEHGUID] [uniqueidentifier] NULL,
	[CORRGUID] [uniqueidentifier] NULL,
	[qtyOld] [numeric](19, 5) NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[EXPIRATION] [nvarchar](50) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[Production_Sources]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production_Sources](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNAME] [nvarchar](51) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL DEFAULT (''),
	[SKL_ID] [nvarchar](20) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[GUID_Production] [uniqueidentifier] NULL,
	[GUID] [uniqueidentifier] NULL,
	[USER_ID] [nvarchar](10) NULL,
	[TERMINAL_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[ISOK] [datetime] NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[StatusTypes]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[UCSTR]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UCSTR](
	[STREDISKO] [nvarchar](6) NOT NULL,
	[NAZEV] [nvarchar](50) NULL,
	[UCETNI] [nvarchar](3) NULL,
	[STRED2] [nvarchar](3) NULL,
	[CINNOST] [nvarchar](3) NULL,
	[ZAK] [nvarchar](1) NULL,
	[AKTIVNI] [tinyint] NOT NULL,
	[KLIC_STA] [int] NULL,
	[CASZAPSANI] [datetime] NULL,
	[STRUKT] [tinyint] NOT NULL,
	[STRUKTSEZN] [nvarchar](800) NULL,
 CONSTRAINT [PK__UCSTR__JKR] PRIMARY KEY CLUSTERED 
(
	[STREDISKO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[UserEvents]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[REZ1] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[VLoginsGroups]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VLoginsGroups](
	[loginid] [nvarchar](20) NOT NULL,
	[groupid] [nvarchar](10) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[VMachinesOperations]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VMachinesOperations](
	[machineid] [nvarchar](20) NOT NULL,
	[operationid] [nvarchar](16) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_stav_naplneni_inventury]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_stav_naplneni_inventury]
AS
SELECT
CONVERT(nvarchar(2), 'I') AS Type, 
zbozi.ITEMDESC, 
CONVERT(nvarchar(30), '') AS DOCUMENT_NUMBER, 
stav.ITEMNMBR, 
stav.QTYSHPPD_DEF AS QTYSHPPD, 
stav.SKL_ID, 
stav.LOCNCODE, 
stav.SERLTNUM, 
NULL AS CountEntries, 
i4.CZ_CarKod, 
i4.USERID AS USER_ID, 
i4.ID_TERMINAL, 
stav.QTYSHPPD_DEF_DATE AS dateeve, 
zbozi.ITEMCODE, 
i4.VNDITNUM, 
i4.WEIGHT, 
i4.GUID, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_Stav AS stav LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = stav.ITEMNMBR LEFT OUTER JOIN
dbo.CZMST_I4 AS i4 ON stav.ITEMNMBR = i4.ITEMNMBR AND stav.LOCNCODE = i4.LOCNCODE
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_lokacni_mechanismus_pohyby]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_lokacni_mechanismus_pohyby]
AS
SELECT 
pohyb.POHYB_TYPE AS Type, 
zbozi.ITEMDESC, 
pohyb.DOCUMENT_NUMBER, 
pohyb.ITEMNMBR, 
pohyb.QTYSHPPD, 
pohyb.SKL_ID_SRC AS SKL_ID, 
pohyb.LOCNCODE_SRC AS LOCNCODE, 
pohyb.SERLTNUM, 
pohyb.CountEntries, 
zbozi.CZ_CarKod AS CZ_CarKod, 
pohyb.UserID AS USER_ID, 
pohyb.TermID AS ID_TERMINAL, 
pohyb.dateeveT AS dateeve, 
zbozi.ITEMCODE, 
zbozi.VNDITNUM, 
zbozi.WEIGHT, 
pohyb.guid, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_StavPohyb AS pohyb LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = pohyb.ITEMNMBR
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_prijem]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_prijem]
AS
SELECT
CONVERT(nvarchar(2), 'PP') AS Type, 
pe.ITEMDESC, 
pi.PONUMBER AS DOCUMENT_NUMBER, 
pi.ITEMNMBR, 
pi.QTYSHPPD, 
pi.SKL_ID, 
pi.LOCNCODE, 
pi.SERLTNUM, 
pi.CountEntries, 
pi.CZ_CarKod, 
pi.USER_ID, 
pi.ID_TERMINAL,
dbo.fask_func_convert_to_datetime(pi.TIMEDONE, pi.DATEDONE) AS dateeve, 
pi.ITEMCODE, 
pi.VNDITNUM, 
pi.WEIGHT, 
pi.GUID, 
pi.VNDDOCNM
FROM
dbo.CZMST_PI AS pi LEFT OUTER JOIN
dbo.CZMST_PE AS pe ON pe.ITEMNMBR = pi.ITEMNMBR AND pe.ORD = pi.ORD AND pe.CountEntries = pi.CountEntries
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_prodej]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_prodej]
AS
SELECT     
CONVERT(nvarchar(2), 'R') as [Type], 
zbozi.ITEMDESC as ITEMDESC, 
CONVERT(nvarchar(30), '') as [DOCUMENT_NUMBER], 
di.ITEMNMBR, 
di.QTYSHPPD, 
di.SKL_ID, 
di.LOCNCODE, 
di.SERLTNUM, 
di.CountEntries, 
di.CZ_CarKod, 
di.USER_ID, 
di.ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
di.ITEMCODE,
di.VNDITNUM,
di.WEIGHT,
--null as WEIGHT,
di.GUID,
convert(nvarchar(21),null) as VNDDOCNM
FROM         CZMST_DI di
LEFT JOIN FASK_ZASOBY zbozi on zbozi.itemnmbr = di.itemnmbr
;

GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_vydej]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_vydej]
AS
SELECT     
CONVERT(nvarchar(2), 'VP') as [Type], 
se.ITEMDESC as ITEMDESC, 
si.SOPNUMBE as [DOCUMENT_NUMBER], 
si.ITEMNMBR, 
-si.QTYSHPPD as QTYSHPPD, 
si.SKL_ID, si.LOCNCODE, 
si.SERLTNUM, 
si.CountEntries, 
si.CZ_CarKod, 
USER_ID, 
ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
si.ITEMCODE,
si.VNDITNUM,
si.WEIGHT,
si.GUID,
si.VNDDOCNM

FROM         dbo.CZMST_SI si
left join CZMST_SE se on se.ITEMNMBR = si.itemnmbr and se.ORD = si.ORD and se.CountEntries = si.CountEntries
;

GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_pohyby_aktualni]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_pohyby_aktualni]
AS
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_vydej
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prodej
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_lokacni_mechanismus_pohyby
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_stav_naplneni_inventury
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prijem
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_prodej_history]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* View do prodeje */
CREATE VIEW [dbo].[fask_view_prodej_history]
AS
SELECT     
CONVERT(nvarchar(2), 'R') as [Type], 
zbozi.ITEMDESC as ITEMDESC, 
CONVERT(nvarchar(30), '') as [DOCUMENT_NUMBER], 
di.ITEMNMBR, 
di.QTYSHPPD, 
di.SKL_ID, 
di.LOCNCODE, 
di.SERLTNUM, 
di.CountEntries, 
di.CZ_CarKod, 
di.USER_ID, 
di.ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
di.ITEMCODE,
di.VNDITNUM,
di.WEIGHT as WEIGHT,
--null as WEIGHT,
di.GUID,
convert(nvarchar(21),null) as VNDDOCNM
FROM         CZMST_DI_HISTORY di
LEFT JOIN FASK_ZASOBY zbozi on zbozi.itemnmbr = di.itemnmbr
;

GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_vydej_history]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* View do vydeje */
CREATE VIEW [dbo].[fask_view_vydej_history]
AS
SELECT     
CONVERT(nvarchar(2), 'VP') as [Type], 
se.ITEMDESC as ITEMDESC, 
si.SOPNUMBE as [DOCUMENT_NUMBER], 
si.ITEMNMBR, 
-si.QTYSHPPD as QTYSHPPD, 
si.SKL_ID, 
si.LOCNCODE, 
si.SERLTNUM, 
si.CountEntries, 
si.CZ_CarKod, 
USER_ID, ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
si.ITEMCODE,
si.VNDITNUM, 
si.WEIGHT,
si.GUID,
si.VNDDOCNM
FROM         dbo.CZMST_SI_history si
left join CZMST_SE_history se on se.ITEMNMBR = si.itemnmbr and se.ORD = si.ORD and se.CountEntries = si.CountEntries
;
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_prijem_history]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_prijem_history]
AS
SELECT
CONVERT(nvarchar(2), 'PP') AS Type, 
pe.ITEMDESC, 
pi.PONUMBER AS DOCUMENT_NUMBER, 
pi.ITEMNMBR, 
pi.QTYSHPPD, 
pi.SKL_ID, 
pi.LOCNCODE, 
pi.SERLTNUM, 
pi.CountEntries, 
pi.CZ_CarKod, 
pi.USER_ID, 
pi.ID_TERMINAL,
dbo.fask_func_convert_to_datetime(pi.TIMEDONE, pi.DATEDONE) AS dateeve, 
pi.ITEMCODE, 
pi.VNDITNUM, 
pi.WEIGHT, 
pi.GUID, 
pi.VNDDOCNM
FROM
dbo.CZMST_PI_HISTORY AS pi LEFT OUTER JOIN
dbo.CZMST_PE_HISTORY AS pe ON pe.ITEMNMBR = pi.ITEMNMBR AND pe.ORD = pi.ORD AND pe.CountEntries = pi.CountEntries
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_pohyby_archivni]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_pohyby_archivni]
AS
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_vydej_history
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_prodej_history
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_prijem_history
GO
/**************************************************************************************/
/****** Object:  View [dbo].[fask_view_pohyby_aktualni_a_archivni]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_pohyby_aktualni_a_archivni]
AS
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_aktualni
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_archivni
GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_vyroba_OverHeslo]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 28.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontroli HESLA dle zadaneho ID 
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverHeslo]
(	
	-- Add the parameters for the function here
	@inID nvarchar(25),
	@inHESLO nvarchar(10)

)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT psswd FROM FASK_logins WHERE USERID = @inID AND psswd = @inHESLO
)

GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_vyroba_OverId]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 25.8.2017
-- Description:	Jedna se o Table-Value funkci ktera provadi kontrol ID
-- =============================================
CREATE FUNCTION [dbo].[FASK_vyroba_OverId]
(	
	-- Add the parameters for the function here
	@id nvarchar(25) 
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT USERID FROM FASK_logins WHERE USERID = @id
)

GO
/**************************************************************************************/
/****** Object:  View [dbo].[CZPRO_VPH_View]    *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CZPRO_VPH_View]
AS
SELECT
	   vph.[CountEntries]
      ,vph.[SOPNUMBE]
      ,vph.[SOPTYPE]
      ,vph.[SOPDESC]
      ,vph.[VNDDOCNMH]
      ,vph.[BarcodeH]
      ,vph.[LOCNCODE]
      ,vph.[DateProd]
      ,vph.[Rez1]
      ,vph.[Rez2]
      ,vph.[TermID]
      ,vph.[LSTMod]
      ,vph.[DEX_ROW_ID]
FROM         
	CZPRO_VPH vph
WHERE ISNULL(vph.Active,1)=1

GO
/**************************************************************************************/
/****** Object:  View [dbo].[CZPRO_VPP_View]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CZPRO_VPP_View]
AS
SELECT    
	vpp.CountEntries, 
	vpp.SOPNUMBE, 
	vpp.ITEMNMBR, 
	vpp.ITEMTYPE, 
	vpp.ITEMDESC, 
	vpp.ITEMMJ,
	vpp.VNDDOCNMP, 
	vpp.VNDITNUM, 
	vpp.ORD, 
	vpp.BarcodeP, 
	vpp.LOCNCODE, 
	vpp.QTYSHPPD, 
	vpp.QTYPACK, 
	vpp.QTYPACKMJ,
	vpp.TIMEMODE,
	vpp.TIMEPREP, 
	vpp.TIMEUNIT, 
	vpp.DtProdT, 
	vpp.DtProdL, 
	vpp.SerNumT, 
	vpp.SerNumL, 
	vpp.VerT, 
	vpp.VerL, 
	vpp.TermID, 
	case 
		when vpp.LSTMod > isnull(psum.LSTMod, getdate()) then vpp.LSTMod
		else isnull(psum.LSTMod, getdate())
	end as LSTMod, 
	vpp.DEX_ROW_ID, 
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, --Pocet kusu odvedenych automaticky terminaly a pripadnym rucnim odvodem ze systemu
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO	--Slouzi k informaci, zda zapocitat pripravny cas do dalsiho vystupu(>0 => nezapocitat pripravny cas)
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO , MAX(dateeve) as LSTMod
	FROM   dbo.Production
	WHERE  dateeve > (Select TOP 1 LSTMod from CZPRO_VPP as vpp2 where dbo.Production.CountEntries = vpp2.CountEntries and dbo.Production.SOPNUMBE = vpp2.SOPNUMBE AND dbo.Production.ITEMNMBR = vpp2.ITEMNMBR)
	GROUP BY CountEntries, SOPNUMBE, ITEMNMBR
) AS psum ON psum.CountEntries = vpp.CountEntries and psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR
LEFT JOIN CZPRO_VPH AS vph ON vph.CountEntries=vpp.CountEntries and vph.SOPNUMBE=vpp.SOPNUMBE
WHERE ISNULL(vph.Active,1)=1

GO
/**************************************************************************************/
/****** Object:  View [dbo].[FASK_Logins_View_Prava]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FASK_Logins_View_Prava]
AS
SELECT        L.USERID, L.firstname, L.surname, L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, A.AGENDAID
FROM            dbo.FASK_Logins AS L LEFT OUTER JOIN
                         dbo.FASK_Logins_Auth AS A ON A.USERID = L.USERID

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[CZMST_get_sscc_sequence_proc]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_get_sscc_sequence_proc] 
 @sequence int  ,
 @count int , 
 @terminal int ,
 @endSSCC int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
	BEGIN TRANSACTION
	BEGIN TRY
	
		DECLARE @sequence_count int;
		
		--DECLARE @LV numeric(1,0);
		--declare @GCP numeric(9,0);
		--declare @GCP_count numeric(9,0);
		-- return an error if sequence does not exist
		-- so we will know if someone truncates the table
		
		-- set @sequence_count = -1
		
		select @sequence_count = seq.sequence_count  
		from CZMST_SSCC_SEQUENCE as seq  
		where seq_id = @sequence
		
		IF @sequence_count is NULL 
		BEGIN
			set @sequence_count = 0
			insert into CZMST_SSCC_SEQUENCE (seq_id, sequence_count) values (@sequence, @sequence_count)
		END		
		
		--select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		--from CZMST_SSCC_PARAMETERS as par
		--where par.ID_SSCC = @sequence
		
		--SET @endSSCC = '00' + convert(nvarchar(1),@LV) + convert(nvarchar(9),@GCP) + convert(nvarchar(9),@sequence_count) ;
		SET @endSSCC = @sequence_count + @count
	
		UPDATE CZMST_SSCC_SEQUENCE
		SET    sequence_count = @endSSCC
		WHERE  seq_id = @sequence

		COMMIT TRANSACTION

	RETURN @endSSCC		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @endSSCC = NULL
		RETURN @endSSCC
	END CATCH
END

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[CZMST_Next_CountEntries]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 18.4.2016
-- Description:	Generuje nasledujici cislo davky pro modul transakcne
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Next_CountEntries]
 @module nvarchar(20),
 @CountEntries int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
		DECLARE @ce int;
				
		select @ce = CountEntries
		from CZMST_CountEntries
		where TBL = @module

		IF @ce is NULL 
		BEGIN
			SET @CountEntries = 1
			INSERT INTO CZMST_CountEntries (TBL, CountEntries) VALUES (@module, @CountEntries)
		END
		ELSE
		BEGIN
			SET @CountEntries = @ce + 1
			UPDATE CZMST_CountEntries
			SET    CountEntries = @CountEntries
			WHERE  TBL = @module
		END
END

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[fask_CZPRO_LastUserAction]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 7.7.2014
-- Description:	Last user production
-- =============================================
CREATE PROCEDURE [dbo].[fask_CZPRO_LastUserAction] 
	-- Add the parameters for the stored procedure here
	@loginid nvarchar(20), 
	@machineid nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 * from Production
	where loginid=@loginid and machineid=@machineid
	order by dateeve desc
END

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[fask_Events2Production_Confirm]    *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 5.6.2019
-- Description:	Procedura pro prevod vyroby ze sledovani do Production
-- =============================================
CREATE PROCEDURE [dbo].[fask_Events2Production_Confirm] 
	-- Add the parameters for the stored procedure here
	@from datetime = null, 
	@to datetime = null
AS
BEGIN
	
	/*
	1. do temp struktury vytahnout data z Events, se kterymi budu pracovat
	2. z temp struktury vytvorit sumaci za klic
	3. tyto sumy vlozit do Production, guid, ktery je pridelen zaznamenat do temp struktury k polozkam dle klice
	4. zaznamu z temp strukutry promitnout zpet do Events (vazebni guid dle id)
	*/

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare
		@datetimemin datetime,
		@datetimemax datetime

	set @datetimemin = CONVERT(datetime, 0)
	set @datetimemax = GETDATE()

    -- Insert statements for procedure here
	-- SELECT @from, @to

	IF OBJECT_ID('tempdb..#EventsTemp', 'U') IS NOT NULL
		DROP TABLE #EventsTemp;

	SELECT 
		GETDATE() [GeneratedDatetime]
		,dbo.fask_GS1_AI_GET('EAN', e.barcodeReaded) EAN
		,dbo.fask_GS1_AI_GET('SARZE', e.barcodeReaded) SARZE
		,dbo.fask_GS1_AI_GET('EXPIRACE', e.barcodeReaded) EXPIRACE
		,dbo.fask_GS1_AI_GET('VAHA', e.barcodeReaded) VAHA
		,* 
	INTO #EventsTemp
	from FASK_Events e 
	where 1=1
	and ((e.isProcessed is NULL) OR (e.productionGuid is null))
	and e.dateeve between ISNULL(@from, @datetimemin) and ISNULL(@to, @datetimemax)

	-- priprava dat k vlozeni do production
	IF OBJECT_ID('tempdb..#Procution2Insert', 'U') IS NOT NULL
		DROP TABLE #Procution2Insert;

	select 
		et.EAN
		, et.SARZE
		, et.EXPIRACE
		, et.material
		, count(*) pocetPolozek
		, sum(convert(numeric(18,5),isnull(et.VAHA, '0')) / 100) vaha
		, NEWID() productionGuid
	into #Procution2Insert
	from #EventsTemp et
	group by et.EAN, et.SARZE, et.EXPIRACE, et.material

	-- vlozeni do Production
	insert into Production (
		CountEntries
		,ITEMNMBR	-- EAN
		,BarcodeP	-- EAN taky
		,[description] -- popis obsahujici informaci o sarzi zadane na stroji
		,loginid	-- cislo, urcujici server(stanici)? mel byt prihlaseny uzivatel
		,dateeve	-- aktualni cas 
		,qty		-- pocet polozek
		,qtyReal	-- celkova vaha
		,UserID		-- cislo, urcujici server(stanici)?
		,TermID		-- cislo, urcujici server(stanici)?
		,[GUID]		-- guid noveho zaznamu
		,SERLTNUM	-- sarze (doplnit do struktury)
		,Expiration -- expirace (doplnit do struktury)
	)
	select 
		1	CountEntries
		,p.EAN ITEMNMBR	-- EAN
		,p.EAN BarcodeP	-- EAN taky
		,p.material [description] -- popis obsahujici informaci o sarzi zadane na stroji
		,0 loginid	-- cislo, urcujici server(stanici)? mel byt prihlaseny uzivatel
		,getdate() dateeve	-- aktualni cas 
		,p.pocetPolozek qty		-- pocet polozek
		,p.vaha qtyReal	-- celkova vaha
		,0 UserID		-- cislo, urcujici server(stanici)?
		,0 TermID		-- cislo, urcujici server(stanici)?
		,p.productionGuid [GUID]		-- guid noveho zaznamu
		,p.SARZE Serltnum	-- sarze (doplnit do struktury)
		,p.EXPIRACE Expiration -- expirace (doplnit do struktury)
		from #Procution2Insert p

	-- aktualizace #EventsTemp
	update #EventsTemp
	set productionGuid = p.productionGuid
	from #Procution2Insert p
	where 1=1
		and #EventsTemp.EAN = p.EAN
		and #EventsTemp.SARZE = p.Sarze
		and #EventsTemp.EXPIRACE = p.EXPIRACE
		and #EventsTemp.material = p.material

	-- update Fask_Events
	update FASK_Events
	set 
		fask_events.productionGuid = et.productionGuid
		,fask_events.isProcessed = et.GeneratedDatetime
	from #EventsTemp et
	where 1=1
		and FASK_Events.id = et.id

	-- konec zpracovani procedury
	drop table #EventsTemp
	drop table #Procution2Insert
	--select 0 as OK
	--select 0 as OKddd
	RETURN 0

END

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_PrijemGetSkladExpedice]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- autor: Jiri Skrivanek
-- firma: FASK, spol. s r.o.
-- datum: 30.5.2018
-- =============================================
CREATE PROCEDURE [dbo].[FASK_PrijemGetSkladExpedice]
    @Itemnmbr			NVarChar(31),			-- ID polozky prijimane
    @MnozstviZadane		numeric(19,5),			-- pocet prijimanych terminalem
	@MnozstviNasnimane		numeric(19,5),			-- pocet prijatych terminalem
	-- out hodnoty
	@MnozstviDodavatelePozadovano	numeric(19,5) out,	-- požadováno od dodavatele(ů) 
													-- => Množství Dodat(objednáno) pro nevyřízené objednávky vydané
	@MnozstviDodavateleDodano		numeric(19,5) out,	-- dodáno od dodavatele(ů) 
													-- => Množství Dodáno (převedeno) pro nevyřízené objednávky vydané
	@MnozstviDodavateleDodat		numeric(19,5) out,	-- zbývá dodat od dodavatele(ů) 
													-- => Množství Dodat(objednáno) – Dodáno(převedeno) pro nevyřízené objednávky vydané
	@MnozstviOdberateliPozadovano	numeric(19,5) out,	-- požadováno odběrateli 
													-- => Množsvtí Dodat z nevyřízených objednávek přijatých
													-- => ??? (jen nekryté množství) 
													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@MnozstviOdberatelumDodano		numeric(19,5) out,	-- dodáno odběratelům 
													-- => ??? (resp. Na sklad přijato z terminálu?)
													-- => Množsvtí Dodáno z nevyřízených objednávek přijatých
													-- => zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@MnozstviOdberatelumDodat		numeric(19,5) out,	-- zbývá dodat odběratelům
													-- => Množsvtí Dodat – Dodáno z nevyřízených objednávek přijatých
													-- => ? zohlednit i příjemky, tedy kolik je materiálu na skladě?
	@Vysledek 			Numeric(19,5)	out		-- Vysledek vypoctu = (<Zbyva dodat odberatelum> - <prijato terminalem>)
AS
BEGIN
	SET NOCOUNT ON;

	Set	@MnozstviDodavatelePozadovano=0
	Set	@MnozstviDodavateleDodano=0
	Set	@MnozstviDodavateleDodat=0
	Set	@MnozstviOdberateliPozadovano=0
	Set	@MnozstviOdberatelumDodano=0
	Set	@MnozstviOdberatelumDodat=0
	Set	@Vysledek=0

	Declare 
		@Rezervovano numeric(19,5),
		@Reklamovano numeric(19,5),
		@Stav		 numeric(19,5)

	--Objednávky (SKz.ObjedP) + Rezervace (SKz.Rezer) + Reklamace (SKz.Reklam)  - Stav zásoby skladem (SKz.stavZ)  = počet kolik dát na expedici
	select 
		@MnozstviDodavateleDodat =	SKz.ObjedV,
		@MnozstviOdberatelumDodat = SKz.ObjedP,
		@Rezervovano =				SKz.Rezer,
		@Reklamovano =				SKz.Reklam,
		@Stav =						SKz.StavZ
	from StwPh_04535667_2020.dbo.Skz 
	where ID = @Itemnmbr

	Set @Vysledek = @MnozstviOdberatelumDodat + @Rezervovano + @Reklamovano - @Stav - @MnozstviNasnimane

	RETURN 0	-- ok... procedura prosla korektne ... 

END

GO
/**************************************************************************************/
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
/****** Object:  StoredProcedure [dbo].[proc_CZMST_RFID_Next]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
	CREATE PROCEDURE  [dbo].[proc_CZMST_RFID_Next]
	 @itemnmbr nvarchar(40),
	 @countnumbers int,
	 @minsequencenmbr int OUTPUT,
	 @maxsequencenmbr int OUTPUT
	AS
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
			DECLARE @id int;
			DECLARE @sequencenmbr int;
				
			select @id = ID
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr

			IF @id is NULL 
			BEGIN
				INSERT INTO CZMST_RFID_ITEMS (ITEMNMBR) VALUES (@itemnmbr)
			END

			select @id = ID, @sequencenmbr = SEQUENCENMBR
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr
		
			SET @minsequencenmbr = @sequencenmbr + 1;
			SET @maxsequencenmbr = @sequencenmbr + @countnumbers;
		
			UPDATE CZMST_RFID_ITEMS
			SET    SEQUENCENMBR = @maxsequencenmbr
			WHERE  ID = @id

			-- vraci pocet vracenych cisel, melo by byt stejne jako je @countnumbers
			RETURN (@maxsequencenmbr - @minsequencenmbr + 1);
	END

GO
/**************************************************************************************/

/****** Object:  Trigger [dbo].[fask_ukol_uziv_history]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie pri zmene stavu ukolu uzivatele *****/
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 
-- Description:	historie a aktualizace
-- =============================================
CREATE TRIGGER [dbo].[fask_ukol_uziv_history] 
   ON  [dbo].[CZ_UKOL_UZIV] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare 
           @ID int
           ,@UkolID int
           ,@UserID int
           ,@State nvarchar(1)
           ,@DateChanged datetime
           ,@UserIDChanged int
           ,@Note nvarchar(200)
           ,@DateNotify datetime
           ,@DateFinished datetime

INSERT INTO [CZ_UKOL_UZIV_HIST]
           ([ID]
           ,[UkolID]
           ,[UserID]
           ,[State]
           ,[DateChanged]
           ,[UserIDChanged]
           ,[Note]
           ,[DateNotify]
           ,[DateFinished])
           ( select 
				ID 
				,UkolID
				,UserID
				,[State]
				,DateChanged
				,UserIDChanged
				,Note
				,DateNotify
				,DateFinished
				from inserted
           )
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_DI_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie vystupnich dat pro modul prodej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_DI_HISTORY] 
   ON  [dbo].[CZMST_DI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_di_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_DIH_HISTORY]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat hlavicek pro modul prodej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_DIH_HISTORY] 
   ON  [dbo].[CZMST_DIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_dih_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie hlavicek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Baleni_Hlavicka] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Baleni_Hlavicka_HISTORY select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Baleni_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie polozek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Baleni_Polozky_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Baleni_Polozky] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Baleni_Polozky_HISTORY select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Hlavicka_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie hlavicek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Hlavicka_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Hlavicka] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Hlavicka_HISTORY select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie polozek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Polozky_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Polozky] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Polozky_HISTORY select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PE_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat davky prijmu *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PE_HISTORY] 
   ON  [dbo].[CZMST_PE] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pe_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie seriovych cisel prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PE_SN_HISTORY] 
   ON  [dbo].[CZMST_PE_SN] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pe_sn_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PEH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat hlavicek pro modul prijem *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PEH_HISTORY] 
   ON  [dbo].[CZMST_PEH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_peh_history select * from deleted
END

GO
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PI_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PI_HISTORY] 
   ON  [dbo].[CZMST_PI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pi_history select * from deleted

END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PI_F_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie fotek prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PI_F_HISTORY] 
   ON  [dbo].[CZMST_PI_F] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pi_f_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_PIH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PIH_HISTORY] 
   ON  [dbo].[CZMST_PIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pih_history select * from deleted

END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_SE_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***** Trigger pro ukladani historie dat predlohy pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SE_HISTORY] 
   ON  [dbo].[CZMST_SE] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_se_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_SE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat predlohy seriovych cisel pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SE_SN_HISTORY] 
   ON  [dbo].[CZMST_SE_SN] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_se_sn_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_SEH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie hlavicek pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SEH_HISTORY] 
   ON  [dbo].[CZMST_SEH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_seh_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_SI_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***** Trigger pro ukladani historie vystupnich dat pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SI_HISTORY] 
   ON  [dbo].[CZMST_SI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_si_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  Trigger [dbo].[fask_trg_CZMST_SIH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie vystupnich dat pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SIH_HISTORY] 
   ON  [dbo].[CZMST_SIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_sih_history select * from deleted
END

GO
/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[fask_proc_EditFuncProc]     ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tadeas Divacky
-- Create date:		25.8.2020 
-- Description:	Jedná se o proceduru sloužící pro hromadnou editaci Funkci a Procedur
-- =============================================
CREATE PROCEDURE [dbo].[fask_proc_EditFuncProc] 
	@TEXT_OLD nvarchar(100),
	@TEXT_NEW nvarchar(100)
	AS
BEGIN
	SET NOCOUNT ON;


DECLARE @sp_names TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(MAX)
);

--Pomocná tabulka
DECLARE @HelpText TABLE
(
    Val NVARCHAR(MAX)
);


--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
        @sp_name NVARCHAR(128),
        @text NVARCHAR(MAX);

INSERT  @sp_names
SELECT
    sch.name+'.'+ob.name AS Name
FROM 
     sys.objects AS ob
     LEFT JOIN sys.schemas AS sch ON
            sch.schema_id = ob.schema_id
     LEFT JOIN sys.sql_modules AS mod ON
            mod.object_id = ob.object_id
WHERE mod.definition IS NOT NULL
AND ob.type_desc in (
'SQL_INLINE_TABLE_VALUED_FUNCTION',
'SQL_SCALAR_FUNCTION',
'SQL_TABLE_VALUED_FUNCTION',
'SQL_STORED_PROCEDURE'
)

SET @sp_count = (SELECT COUNT(1) FROM @sp_names)

--Cyklus přes všechny
WHILE (@sp_count > @count)
BEGIN
    SET @count = @count + 1; -- inkrement pro projiti všech procedur
    SET @text = N''; -- Prazdny text

	--Vytažení Name procedruz podle jedinečneho ID pořadí
    SET @sp_name = (SELECT  name
                    FROM    @sp_names
                    WHERE   ID = @count);

	-- Vytažení Obsahu textu tela procedury
    INSERT INTO @HelpText
    EXEC sp_HelpText @sp_name;

	--Vytažení obsahu  textu procedury
    SELECT  @text = COALESCE(@text + ' ' + Val, Val)
    FROM    @HelpText;

	--Smazani tmp promenne 
    DELETE FROM @HelpText;


    IF @text LIKE '%' + @TEXT_OLD + '%'
    BEGIN
		IF @text LIKE '%CREATE PROCEDURE%'
			BEGIN
				SET @text = REPLACE(@text, 'CREATE PROCEDURE', 'ALTER PROCEDURE');
			END
		ELSE IF @text LIKE '%CREATE FUNCTION%'
			BEGIN
				SET @text = REPLACE(@text, 'CREATE FUNCTION', 'ALTER FUNCTION');
			END

			SET @text = REPLACE(@text, @TEXT_OLD, @TEXT_NEW);

			EXECUTE sp_executesql @text;
    END
END

END
GO
/**************************************************************************************/

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
/***** FAKE machine, co se nepouživa, ale prostě musí byt ***********************************************/

INSERT INTO [dbo].[Machines] ([id] ,[name] ,[description]) VALUES (N'1', N'1',N'1')

/***************************************************************************************/
/***** FAKE machine, co se nepouživa, ale prostě musí byt ***********************************************/

INSERT INTO [dbo].[FASK_Machines] ([id] ,[name] ,[description]) VALUES (N'1', N'1',N'1')

/********************************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_DobrePodlahy_Fill_I4]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 12.10.2020
-- Description:	Procedrua pro FAKE naplneni Inventury do I4
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_Fill_I4_From_I1I2I3]
	-- Add the parameters for the stored procedure here
	@CountEntries int = 1,
	@LOCNCODE_FAKE nvarchar(20) = '99',
	@SERLTNUM_FAKE nvarchar(20) = '66'	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_I4];

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [CZMST_I4] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z inventury nebudou přidány.'
			return -1;
		END

	/*************************/
	--Varianta kdy je položka vedena na šarže, ale šarže neni vyplnena
	--Vyplnuje se FAKE šarže 66
	/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				@SERLTNUM_FAKE as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 2
			AND I2.SERLNMBR is null
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na množství, ale šarže neni vyplnen			
			/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				'' as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 0
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na šarže, a ma šaržu
			/************************/


						INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				ISNULL(case I3.QTYPACK when 0 then SUM(I2.QTY) else I3.QTYPACK * SUM(I2.QTY) END,0) as [QUANTITY],
				SUM(ISNULL(I2.QTY,0)) as QUANTITYMJ,
				I3.QTYPACK,
				ISNULL(I2.SERLNMBR,''),
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
			FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			left join (SELECT SUM(I2.QTY) as QTY_SUM, I1.ITEMNMBR, I1.CountEntries FROM CZMST_I1 as I1
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			group by I1.ITEMNMBR, I1.CountEntries
			) as I_SUM ON I_SUM.ITEMNMBR = I1.ITEMNMBR AND I_SUM.CountEntries = I1.CountEntries
			where I1.CZ_SerNum_Track = 2 AND I2.SERLNMBR is not null AND I1.CountEntries = @CountEntries
			Group by  I3.VNDITNUM, I1.ITEMDESC, I1.ITEMCODE, I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.MJ, I3.QTYPACK, I1.CZ_SerNum_Track, I2.SERLNMBR,  I1.QUANTITY, I_SUM.QTY_SUM
			order by I3.VNDITNUM

			/***************************/
			/*** Experiment s prayzdnim stanem a fake sarži***/

			--			INSERT INTO CZMST_I4 (
			--	[CountEntries]
			--	,[CE_Orig]
			--	,[ITEMNMBR]
			--	,[CZ_CarKod]
			--	,[LOCNCODE]
			--	,[SKL_ID]
			--	,[VNDITNUM]
			--	,[MJ]
			--	,[QUANTITY]
			--	,[QUANTITYMJ]
			--	,[QTYPACK]
			--	,[SERLNMBR]
			--	,[DATEDONE]
			--	,[TIMEDONE]
			--	,[USERID]
			--	,[GUID]
			--	,[O_Checked]
			--	,[INPUT_MODE]
			--	,[ID_TERMINAL]
			--	,[ITEMCODE]
			--	,[REZ_1]
			--	,[REZ_2]
			--	,[WEIGHT]
			--	,[Expirace])
			--SELECT 
			--	2 as CountEntries,
			--	null as CE_Orig,
			--	I1.ITEMNMBR,
			--	I1.CZ_CarKod,
			--	'99' as [LOCNCODE],
			--	I1.SKL_ID,
			--	I3.VNDITNUM,
			--	I3.MJ,
			--	0 as [QUANTITY],
			--	0 as [QUANTITYMJ],
			--	0 as [QTYPACK],
			--	'66' as [SERLNMBR],
			--	convert(varchar, getdate(), 112) as [DATEDONE],
			--	replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
			--	0 as [USERID],
			--	NEWID() as [GUID],
			--	0 as [O_Checked],
			--	0 as [INPUT_MODE],
			--	99 as [ID_TERMINAL],
			--	I1.ITEMCODE,
			--	'' as [REZ_1],
			--	'' as [REZ_2],
			--	null as [WEIGHT],
			--	null as [Expirace]
			--	FROM CZMST_I1 as I1
			--left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			--left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			--WHERE I1.CZ_SerNum_Track = 2
			--AND I2.SERLNMBR is not null
			--AND I1.CountEntries = 1
			--Group by I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.VNDITNUM, I3.MJ, I1.ITEMDESC, I1.ITEMCODE


SELECT Count(*) FROM CZMST_I4 where CountEntries = @CountEntries

END


/******************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_NaplnLokMechZ_INV] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bc Tadeas Divacky
-- Create date: 
-- Description:	Procedura Pro naplneni LokMech z Inventury
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_NaplnLokMechZ_INV] 
	@CountEntries int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [CZMST_SkladLokace_Stav] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z inventury nebudou přidány.'
			return -1;
		END

		INSERT INTO [CZMST_SkladLokace_Stav]
			([ITEMNMBR],
			[ITEMDESC],
			[QTYSHPPD_DEF],
			[QTYSHPPD],
			[SERLTNUM],
			[SKL_ID],
			[LOCNCODE],
			[DATECHANGE],
			[EXPIRATION],
			[QTYSHPPD_DEF_DATE],
			[QTY_OWNER],
			[PRAC_ID_OWNER])
		SELECT
			i4.ITEMNMBR as ITEMNMBR,
			ISNULL(i1.ITEMDESC, '') as ITEMDESC,
			SUM(i4.QUANTITY) as QTYSHPPD_DEF,
			SUM(i4.QUANTITY) as QTYSHPPD,
			ISNULL(i4.SERLNMBR, '') as SERLTNUM,
			ISNULL(i4.skl_id, '') as SKL_ID,
			ISNULL(i4.LOCNCODE, '') as LOCNCODE,
			GETDATE() as DATECHANGE,
			i4.Expirace as EXPIRATION,
			GETDATE() as QTYSHPPD_DEF_DATE,
			0 as [QTY_OWNER],
			null as [PRAC_ID_OWNER]
			from [CZMST_I4] i4
			left join [CZMST_I1] i1 on i1.ITEMNMBR = i4.ITEMNMBR
			WHERE  I4.CountEntries = @CountEntries
			GROUP BY i4.ITEMNMBR, i1.ITEMDESC, i4.SERLNMBR, i4.skl_id, i4.LOCNCODE, i4.Expirace


		select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

		PRINT 'Lokační mechanismus úspešně naplněn inventurními daty dne ' + CAST(GETDATE() AS NVARCHAR(50))  + '. Do lokačního mechanismu bylo přidáno ' + CAST(@cnt AS NVARCHAR(50)) + ' nových záznamů.';

END

/*********************************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_Insert_VyrobaTP] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 26.10.2020
-- Description:	Procedura pro dotažení vazev z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_Insert_VyrobaTP] 
@ITEMNMBR nvarchar(31) = null,
@ITEMDESC nvarchar(100) = null,
@MJ nvarchar(50) = null,
@ID_H int,
@TypyMaterialu nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;	

	DECLARE @PolozkyMat TABLE
	(
		ID INT PRIMARY KEY IDENTITY,
		Klic int,
		ITEMDESC nvarchar(100),
		MJ nvarchar(50),
		QTY numeric(19,5)
	);

	DECLARE @AllCount INT,
			@Inkrement INT = 0,
			@ITEMNMBR_pol nvarchar(31) = 0,
			@ITEMDESC_pol nvarchar(100) = 0,
			@MJ_pol nvarchar(50) = 0,
			@QTY numeric(19,5),
			@ID_L_Max int;

	INSERT  @PolozkyMat
	SELECT
		S.ID as Klic,
		S.Nazev as ITEMDESC,
		S.MJ as MJ,
		P.Mnozstvi as QTY
	FROM StwPh_04535667_2020.dbo.SKzPol as P
	left join StwPh_04535667_2020.dbo.SKz as S ON S.ID = P.RefSKz
	where P.RefAg = @ITEMNMBR AND 
	S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyMaterialu, default))


	SET @AllCount = (SELECT COUNT(1) FROM @PolozkyMat)

		WHILE (@AllCount > @Inkrement)
		BEGIN

			SET @Inkrement = @Inkrement + 1;

				SET @ITEMNMBR_pol = (SELECT Klic
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @ITEMDESC_pol = (SELECT ITEMDESC
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @MJ_pol = (SELECT MJ
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @QTY = (SELECT QTY
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

		SET @ID_L_Max = (SELECT MAX(ID_L) FROM FASK_Vyroba_TP)

		SET @ID_L_Max = @ID_L_Max + 1;

			INSERT INTO [dbo].[FASK_Vyroba_TP]
		([ID_H]
		,[ID_L]
		,[ITEMNMBR_Def]
		,[DESC_Def]
		,[MJ_Def]
		,[ITEMNMBR_fol]
		,[DESC_Fol]
		,[MJ_Fol]
		,[koef]
		,[ID_USER]
		,[dateedit]
		,[alter]
		,[PUO])
		SELECT 
		@ID_H as [ID_H],
		@ID_L_Max as [ID_L],
		@ITEMNMBR as [ITEMNMBR_Def],
		@ITEMDESC as [DESC_Def],
		@MJ as [MJ_Def],
		@ITEMNMBR_pol as [ITEMNMBR_Fol],
		@ITEMDESC_pol as [DESC_Fol],
		@MJ_pol as [MJ_Fol],
		@QTY as [koef],
		99 as [ID_USER],
		GETDATE() as [dateedit],
		0 as [alter],
		0 as [PUO]

		END		
END


/*****************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_procPlnVyrobaTP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 26.10.2020
-- Description:	Procedura pro dotažení vazev z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_procPlnVyrobaTP]
@TypyVyrobku nvarchar(MAX),
@ListVyrobku nvarchar(MAX),
@TypyMaterialu nvarchar(MAX) 
AS
BEGIN
	SET NOCOUNT ON;	

		declare @cnt int;

	select @cnt = COUNT(*) from FASK_Vyroba_TP;

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [FASK_Vyroba_TP] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z IS POHODA nebudou přidány.'
			return -1;
		END


DECLARE @Polozky TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Klic int,
	ITEMDESC nvarchar(100),
	MJ nvarchar(50)

);

--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
		@ITEMNMBR nvarchar(31) = 0,
		@ITEMDESC nvarchar(100) = 0,
		@MJ nvarchar(50) = 0,
		@ID_L_Max nvarchar(50) = '99';


IF @ListVyrobku = ''
	BEGIN
		INSERT  @Polozky
		SELECT
			S.ID as Klic,
			S.Nazev as ITEMDESC,
			S.MJ as MJ
		FROM  StwPh_04535667_2020.dbo.SKz as S
		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
	END
ELSE
	BEGIN
		INSERT  @Polozky
		SELECT
			S.ID as Klic,
			S.Nazev as ITEMDESC,
			S.MJ as MJ
		FROM  StwPh_04535667_2020.dbo.SKz as S
		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
		AND S.ID in (SELECT Name FROM dbo.splitstring(@ListVyrobku, default))
	END


SET @sp_count = (SELECT COUNT(1) FROM @Polozky)

--Cyklus přes všechny
	WHILE (@sp_count > @count)
	BEGIN
		SET @count = @count + 1;

		SET @ITEMNMBR = (SELECT Klic
						FROM    @Polozky
						WHERE   ID = @count);

		SET @ITEMDESC = (SELECT ITEMDESC
						FROM    @Polozky
						WHERE   ID = @count);

		SET @MJ = (SELECT MJ
						FROM    @Polozky
						WHERE   ID = @count);

		SET @ID_L_Max = (SELECT 
						ISNULL(MAX(ID_L), '99') 
						FROM FASK_Vyroba_TP)

		SET @ID_L_Max = Convert(nvarchar(50), CONVERT(int, @ID_L_Max) + 1);

	INSERT INTO [dbo].[FASK_Vyroba_TP]
			   ([ID_H]
			   ,[ID_L]
			   ,[ITEMNMBR_Def]
			   ,[DESC_Def]
			   ,[MJ_Def]
			   ,[ITEMNMBR_fol]
			   ,[DESC_Fol]
			   ,[MJ_Fol]
			   ,[koef]
			   ,[ID_USER]
			   ,[dateedit]
			   ,[alter]
			   ,[PUO])
			   SELECT 
			   NULL as [ID_H],
			   @ID_L_Max as [ID_L],
			   @ITEMNMBR as [ITEMNMBR_Def],
			   @ITEMDESC as [DESC_Def],
			   @MJ as [MJ_Def],
			   NULL as [ITEMNMBR_Fol],
			   NULL as [DESC_Fol],
			   NULL as [MJ_Fol],
			   NULL as [koef],
			   99 as [ID_USER],
			   GETDATE() as [dateedit],
			   0 as [alter],
			   1 as [PUO]

EXECUTE [FASK_proc_Insert_VyrobaTP] 
   @ITEMNMBR
  ,@ITEMDESC
  ,@MJ
  ,@ID_L_Max
  ,@TypyMaterialu


	END

END
/****************************************************************************/
/****** Object:  Table [dbo].[Production_SN]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production_SN](
	[GUID_Production] [uniqueidentifier] NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[Expirace] [datetime] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Production_SN] ADD  DEFAULT ((1)) FOR [QTY]
GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_ZASOBY_STAV]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_ZASOBY_STAV](
	[KOD] [nvarchar](12) NOT NULL DEFAULT (''),
	[NAZEV] [nvarchar](50) NOT NULL DEFAULT (''),
	[KOD_LOK] [nvarchar](3) NOT NULL DEFAULT (''),
	[STAV] [decimal](12, 3) NOT NULL DEFAULT ((0)),
	[CENA] [decimal](15, 3) NOT NULL DEFAULT ((0)),
	[CENA_ZUST] [decimal](10, 3) NOT NULL DEFAULT ((0)),
	[REZERVACE] [decimal](12, 3) NOT NULL DEFAULT ((0)),
	[TS] [datetime2](7) NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_FASK_ZASOBY_STAV] PRIMARY KEY CLUSTERED 
(
	[KOD] ASC,
	[KOD_LOK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_PLANOVANI]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_PLANOVANI](
	[GUID] [uniqueidentifier] NOT NULL,
	[DESC] [nvarchar](200) NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_PLANOVANI_PARAMS]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS](
	[GUID_PLANOVANI] [uniqueidentifier] NOT NULL,
	[ColumnName] [nvarchar](100) NOT NULL,
	[Value] [BIT] NOT NULL DEFAULT(0)
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  Table [dbo].[FASK_PLANOVANI_PARAMS_Name]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS_Name](
	[Column_Name] [nvarchar](100) NOT NULL,
	[DESC] [nvarchar](200) NOT NULL,
	[dateedit] [datetime] NOT NULL DEFAULT(getdate())
) ON [PRIMARY]

GO
/**************************************************************************************/
/****** Object:  View [dbo].[FASK_PLANOVANI_View]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[FASK_PLANOVANI_View]
AS
SELECT        N.[DESC] AS ColumnName_Lokalizace, P.Value, P.GUID_PLANOVANI, P.ColumnName AS ColumnName_Original
FROM            dbo.FASK_PLANOVANI_PARAMS AS P LEFT OUTER JOIN
                         dbo.FASK_PLANOVANI_PARAMS_Name AS N ON N.Column_Name = P.ColumnName
GO

/**************************************************************************************/

/****** Object:  UserDefinedFunction [dbo].[fask_func_PriznakSledovaniZasoby]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 16.12.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[fask_func_PriznakSledovaniZasoby] 
(
	-- Add the parameters for the function here
	@RelSKzVC int = null,
	@ID int,
	@EvidenceSarzi bit,
	@EvidenceVyrobnichCisel bit,
	@PohodaE1 bit
)
RETURNS int
AS
BEGIN
	
	DECLARE @VPrFXTS int ;
	DECLARE @VPrFDTS int ;
	DECLARE @RefVPrFXTS int ;
	DECLARE @RefVPrFDTS int ;

	DECLARE @Result int;
	SET @Result = 0;

	SET @Result = ISNULL(@RelSKzVC, 0);
	
	IF @Result = 0
		BEGIN

			IF @PohodaE1 = 1
				BEGIN
					SELECT distinct 
					@VPrFXTS = VPrFXTS,
					@VPrFDTS = VPrFDTS,
					@RefVPrFXTS = RefVPrFXTS,
					@RefVPrFDTS = RefVPrFDTS
					 FROM StwPh_04535667_2020.dbo.SKz where ID = @ID

					IF ISNULL(@VPrFXTS, 0) = 1
						BEGIN
							SET @Result = ISNULL(@RefVPrFXTS,1) - 1;

								IF @Result = 1
									BEGIN
										IF @EvidenceVyrobnichCisel = 0
											BEGIN
												SET @Result = 0;
											END
									END
								IF @Result = 2
									BEGIN
										IF @EvidenceSarzi = 0
											BEGIN
												SET @Result = 0;
											END
									END
						END
					ELSE
						BEGIN
							IF ISNULL(@VPrFDTS, 0) = 1
								BEGIN
									SET @Result = ISNULL(@RefVPrFDTS,1) - 1;

										IF @Result = 1
											BEGIN
												IF @EvidenceVyrobnichCisel = 0
													BEGIN
														SET @Result = 0;
													END
											END
										IF @Result = 2
											BEGIN
												IF @EvidenceSarzi = 0
													BEGIN
														SET @Result = 0;
													END
											END
								END
						END
				END
		END

	RETURN @Result

END
GO

/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bc. Tadeáš Divácký
-- Create date: 15.12.2020
-- Description:	Procedura pro naplneni FASK_ZASOBY
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_ZASOBY] 
	-- Add the parameters for the stored procedure here
@ExportTypFilter nvarchar(100),
@ExportSkladFilter nvarchar(100),
@ExportovatPouzeAktivniPolozky bit,
@EXZas_DotahovatAlternativniDodavatele bit,
@EvidenceSarzi bit,
@EvidenceVyrobnichCisel bit,
@PohodaE1 bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM FASK_ZASOBY

	--TODO, na aplikační urovni je udelana logika s LOCNCODE a CZ_SerNumTrack
	--TODO je potreba dotahnout, a rozhodnout co vlastně udelat....


	IF (@ExportSkladFilter is not null AND @ExportSkladFilter != '' ) OR (@ExportTypFilter is not null  AND @ExportTypFilter != '' )
BEGIN --A0
	IF @ExportovatPouzeAktivniPolozky = 1
		BEGIN --B0 

			--SKz_FillBy_EPAP_DAD
			INSERT INTO [dbo].[FASK_ZASOBY]
			   ([ITEMNMBR]
			   ,[ITEMDESC]
			   ,[ITEMCODE]
			   ,[VNDITNUM]
			   ,[CZ_CarKod]
			   ,[LOCNCODE]
			   ,[SKL_ID]
			   ,[QTY]
			   ,[QTYPACK]
			   ,[MJ]
			   ,[DMJ]
			   ,[TAXRATE]
			   ,[PRICE0]
			   ,[PRICE1]
			   ,[PRICE2]
			   ,[PRICE3]
			   ,[PRICE4]
			   ,[PRICE5]
			   ,[CZ_SerNum_Track]
			   ,[CZ_SerNum_Delka]
			   ,[CZ_Rez1_Track]
			   ,[CZ_Rez2_Track]
			   ,[CZ_Rez3_Track]
			   ,[CZ_Rez4_Track]
			   ,[REZ1]
			   ,[REZ2]
			   ,[REZ3]
			   ,[REZ4]
			   ,[ODB_ID]
			   ,[mena_ID]
			   ,[SERLTNUM]
			   ,[WEIGHT]
			   ,[TIMEFROM]
			   ,[TIMETO]
			   ,[LSTMod]
			   ,[loginid]
			   ,[CZ_Expirace_Track]
			   ,[EXPIRACE])
			   SELECT 
			   Left(SKz.ID,40) as ITEMNMBR,
			   Left(SKz.Nazev, 100) as ITEMDESC,
			   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
			   Left(SKz.EAN, 60) as VNDITNUM,
			   '' as CZ_CarKod,
			   '' as LOCNCODE,  --TODO složizejší
			   SKz.RefSklad as SKL_ID,
			   ISNULL(SKz.StavZ, 0) as QTY,
			   0 as QTYPACK,
			   Left(ISNULL(SKz.MJ, ''),10) as MJ,
			   '' as DMJ,
			   0 as TAXRATE,
			   0 as PRICE0,
			   0 as PRICE1,
			   0 as PRICE2,
			   0 as PRICE3,
			   0 as PRICE4,
			   0 as PRICE5,
			   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
			   0 as CZ_SerNum_Delka,
			   0 as CZ_Rez1_Track,
			   0 as CZ_Rez2_Track, 
			   0 as CZ_Rez3_Track, 
			   0 as CZ_Rez4_Track, 
			   0 as REZ1,
			   0 as REZ2,
			   0 as REZ3,
			   0 as REZ4,
			   Left(SKz.RefAD, 12) as ODB_ID,
			   '' as mena_ID,
			   '' as SERLTNUM,
			   null as WEIGHT,
			   null as TIMEFROM,
			   null as TIMETO,
			   GETDATE() as LSTMod,
			   '' as loginid,
			   0 as CZ_Expirace_Track,
			   null as EXPIRACE
		FROM StwPh_04535667_2020.dbo.SKz 
		INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

			IF @EXZas_DotahovatAlternativniDodavatele = 1
				BEGIN --C0

					--SKzAlternatives_FillBy_EPAP_DAD
					INSERT INTO [dbo].[FASK_ZASOBY]
				   ([ITEMNMBR]
				   ,[ITEMDESC]
				   ,[ITEMCODE]
				   ,[VNDITNUM]
				   ,[CZ_CarKod]
				   ,[LOCNCODE]
				   ,[SKL_ID]
				   ,[QTY]
				   ,[QTYPACK]
				   ,[MJ]
				   ,[DMJ]
				   ,[TAXRATE]
				   ,[PRICE0]
				   ,[PRICE1]
				   ,[PRICE2]
				   ,[PRICE3]
				   ,[PRICE4]
				   ,[PRICE5]
				   ,[CZ_SerNum_Track]
				   ,[CZ_SerNum_Delka]
				   ,[CZ_Rez1_Track]
				   ,[CZ_Rez2_Track]
				   ,[CZ_Rez3_Track]
				   ,[CZ_Rez4_Track]
				   ,[REZ1]
				   ,[REZ2]
				   ,[REZ3]
				   ,[REZ4]
				   ,[ODB_ID]
				   ,[mena_ID]
				   ,[SERLTNUM]
				   ,[WEIGHT]
				   ,[TIMEFROM]
				   ,[TIMETO]
				   ,[LSTMod]
				   ,[loginid]
				   ,[CZ_Expirace_Track]
				   ,[EXPIRACE])
				   SELECT 
				   Left(SKz.ID,40) as ITEMNMBR,
				   Left(SKz.Nazev, 100) as ITEMDESC,
				   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				   Left(SKzNC.EAN, 60) as VNDITNUM,
				   '' as CZ_CarKod,
				   '' as LOCNCODE,  --TODO složizejší
				   SKz.RefSklad as SKL_ID,
				   ISNULL(SKz.StavZ, 0) as QTY,
				   0 as QTYPACK,
				   Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				   '' as DMJ,
				   0 as TAXRATE,
				   0 as PRICE0,
				   0 as PRICE1,
				   0 as PRICE2,
				   0 as PRICE3,
				   0 as PRICE4,
				   0 as PRICE5,
				   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				   0 as CZ_SerNum_Delka,
				   0 as CZ_Rez1_Track,
				   0 as CZ_Rez2_Track, 
				   0 as CZ_Rez3_Track, 
				   0 as CZ_Rez4_Track, 
				   0 as REZ1,
				   0 as REZ2,
				   0 as REZ3,
				   0 as REZ4,
				   Left(SKz.RefAD, 12) as ODB_ID,
				   '' as mena_ID,
				   '' as SERLTNUM,
				   null as WEIGHT,
				   null as TIMEFROM,
				   null as TIMETO,
				   GETDATE() as LSTMod,
				   '' as loginid,
				   0 as CZ_Expirace_Track,
				   null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz 
			INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
			INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
			WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

				END--C0

		END --B0
	ELSE
	BEGIN --B1
	
		--SKz_FillBy_DAD
		INSERT INTO [dbo].[FASK_ZASOBY]
           ([ITEMNMBR]
           ,[ITEMDESC]
           ,[ITEMCODE]
           ,[VNDITNUM]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[QTY]
           ,[QTYPACK]
           ,[MJ]
           ,[DMJ]
           ,[TAXRATE]
           ,[PRICE0]
           ,[PRICE1]
           ,[PRICE2]
           ,[PRICE3]
           ,[PRICE4]
           ,[PRICE5]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_Rez1_Track]
           ,[CZ_Rez2_Track]
           ,[CZ_Rez3_Track]
           ,[CZ_Rez4_Track]
           ,[REZ1]
           ,[REZ2]
           ,[REZ3]
           ,[REZ4]
           ,[ODB_ID]
           ,[mena_ID]
           ,[SERLTNUM]
           ,[WEIGHT]
           ,[TIMEFROM]
           ,[TIMETO]
           ,[LSTMod]
           ,[loginid]
           ,[CZ_Expirace_Track]
           ,[EXPIRACE])
		   SELECT 
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
		   FROM StwPh_04535667_2020.dbo.SKz 
		   INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		   WHERE (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
				([ITEMNMBR]
				,[ITEMDESC]
				,[ITEMCODE]
				,[VNDITNUM]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[QTY]
				,[QTYPACK]
				,[MJ]
				,[DMJ]
				,[TAXRATE]
				,[PRICE0]
				,[PRICE1]
				,[PRICE2]
				,[PRICE3]
				,[PRICE4]
				,[PRICE5]
				,[CZ_SerNum_Track]
				,[CZ_SerNum_Delka]
				,[CZ_Rez1_Track]
				,[CZ_Rez2_Track]
				,[CZ_Rez3_Track]
				,[CZ_Rez4_Track]
				,[REZ1]
				,[REZ2]
				,[REZ3]
				,[REZ4]
				,[ODB_ID]
				,[mena_ID]
				,[SERLTNUM]
				,[WEIGHT]
				,[TIMEFROM]
				,[TIMETO]
				,[LSTMod]
				,[loginid]
				,[CZ_Expirace_Track]
				,[EXPIRACE])
				SELECT 
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
		FROM StwPh_04535667_2020.dbo.SKz as SKz 
		INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC  ON SKz.ID = SKzNC.RefAg
		INNER JOIN StwPh_04535667_2020.dbo.sSklad AS s ON s.ID = SKz.RefSklad 
		WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (SELECT Name FROM dbo.splitstring(@ExportSkladFilter , default))) AND (SKz.RelSkTyp IN (SELECT Name FROM dbo.splitstring(@ExportTypFilter, default)))

			END--C1

	END --B1

END--A0
ELSE
BEGIN -- A1
	IF @ExportovatPouzeAktivniPolozky = 1
	BEGIN

		--SKz_GetDataByAktivniPolozky
		INSERT INTO [dbo].[FASK_ZASOBY]
           ([ITEMNMBR]
           ,[ITEMDESC]
           ,[ITEMCODE]
           ,[VNDITNUM]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[QTY]
           ,[QTYPACK]
           ,[MJ]
           ,[DMJ]
           ,[TAXRATE]
           ,[PRICE0]
           ,[PRICE1]
           ,[PRICE2]
           ,[PRICE3]
           ,[PRICE4]
           ,[PRICE5]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_Rez1_Track]
           ,[CZ_Rez2_Track]
           ,[CZ_Rez3_Track]
           ,[CZ_Rez4_Track]
           ,[REZ1]
           ,[REZ2]
           ,[REZ3]
           ,[REZ4]
           ,[ODB_ID]
           ,[mena_ID]
           ,[SERLTNUM]
           ,[WEIGHT]
           ,[TIMEFROM]
           ,[TIMETO]
           ,[LSTMod]
           ,[loginid]
           ,[CZ_Expirace_Track]
           ,[EXPIRACE])
		   SELECT 
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
	FROM StwPh_04535667_2020.dbo.SKz as SKz WHERE (SKz.Odbyt <> 0)

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
				([ITEMNMBR]
				,[ITEMDESC]
				,[ITEMCODE]
				,[VNDITNUM]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[QTY]
				,[QTYPACK]
				,[MJ]
				,[DMJ]
				,[TAXRATE]
				,[PRICE0]
				,[PRICE1]
				,[PRICE2]
				,[PRICE3]
				,[PRICE4]
				,[PRICE5]
				,[CZ_SerNum_Track]
				,[CZ_SerNum_Delka]
				,[CZ_Rez1_Track]
				,[CZ_Rez2_Track]
				,[CZ_Rez3_Track]
				,[CZ_Rez4_Track]
				,[REZ1]
				,[REZ2]
				,[REZ3]
				,[REZ4]
				,[ODB_ID]
				,[mena_ID]
				,[SERLTNUM]
				,[WEIGHT]
				,[TIMEFROM]
				,[TIMETO]
				,[LSTMod]
				,[loginid]
				,[CZ_Expirace_Track]
				,[EXPIRACE])
				SELECT 
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg WHERE (SKz.Odbyt <> 0)

			END--C1

	END
	ELSE
	BEGIN
	
		--SKz_GetDataByOptimalize
		INSERT INTO [dbo].[FASK_ZASOBY]
           ([ITEMNMBR]
           ,[ITEMDESC]
           ,[ITEMCODE]
           ,[VNDITNUM]
           ,[CZ_CarKod]
           ,[LOCNCODE]
           ,[SKL_ID]
           ,[QTY]
           ,[QTYPACK]
           ,[MJ]
           ,[DMJ]
           ,[TAXRATE]
           ,[PRICE0]
           ,[PRICE1]
           ,[PRICE2]
           ,[PRICE3]
           ,[PRICE4]
           ,[PRICE5]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_Rez1_Track]
           ,[CZ_Rez2_Track]
           ,[CZ_Rez3_Track]
           ,[CZ_Rez4_Track]
           ,[REZ1]
           ,[REZ2]
           ,[REZ3]
           ,[REZ4]
           ,[ODB_ID]
           ,[mena_ID]
           ,[SERLTNUM]
           ,[WEIGHT]
           ,[TIMEFROM]
           ,[TIMETO]
           ,[LSTMod]
           ,[loginid]
           ,[CZ_Expirace_Track]
           ,[EXPIRACE])
		   SELECT 
		   Left(SKz.ID,40) as ITEMNMBR,
		   Left(SKz.Nazev, 100) as ITEMDESC,
		   Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
		   Left(SKz.EAN, 60) as VNDITNUM,
		   '' as CZ_CarKod,
		   '' as LOCNCODE,  --TODO složizejší
		   SKz.RefSklad as SKL_ID,
		   ISNULL(SKz.StavZ, 0) as QTY,
		   0 as QTYPACK,
		   Left(ISNULL(SKz.MJ, ''),10) as MJ,
		   '' as DMJ,
		   0 as TAXRATE,
           0 as PRICE0,
           0 as PRICE1,
           0 as PRICE2,
           0 as PRICE3,
           0 as PRICE4,
           0 as PRICE5,
		   dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
		   0 as CZ_SerNum_Delka,
           0 as CZ_Rez1_Track,
           0 as CZ_Rez2_Track, 
           0 as CZ_Rez3_Track, 
           0 as CZ_Rez4_Track, 
           0 as REZ1,
           0 as REZ2,
           0 as REZ3,
           0 as REZ4,
		   Left(SKz.RefAD, 12) as ODB_ID,
		   '' as mena_ID,
           '' as SERLTNUM,
           null as WEIGHT,
           null as TIMEFROM,
           null as TIMETO,
           GETDATE() as LSTMod,
           '' as loginid,
           0 as CZ_Expirace_Track,
           null as EXPIRACE
		   FROM StwPh_04535667_2020.dbo.SKz as SKz  

		IF @EXZas_DotahovatAlternativniDodavatele = 1
			BEGIN --C1

				--SKzAlternatives_FillBy_DAD
				INSERT INTO [dbo].[FASK_ZASOBY]
				([ITEMNMBR]
				,[ITEMDESC]
				,[ITEMCODE]
				,[VNDITNUM]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[QTY]
				,[QTYPACK]
				,[MJ]
				,[DMJ]
				,[TAXRATE]
				,[PRICE0]
				,[PRICE1]
				,[PRICE2]
				,[PRICE3]
				,[PRICE4]
				,[PRICE5]
				,[CZ_SerNum_Track]
				,[CZ_SerNum_Delka]
				,[CZ_Rez1_Track]
				,[CZ_Rez2_Track]
				,[CZ_Rez3_Track]
				,[CZ_Rez4_Track]
				,[REZ1]
				,[REZ2]
				,[REZ3]
				,[REZ4]
				,[ODB_ID]
				,[mena_ID]
				,[SERLTNUM]
				,[WEIGHT]
				,[TIMEFROM]
				,[TIMETO]
				,[LSTMod]
				,[loginid]
				,[CZ_Expirace_Track]
				,[EXPIRACE])
				SELECT 
				Left(SKz.ID,40) as ITEMNMBR,
				Left(SKz.Nazev, 100) as ITEMDESC,
				Left(ISNULL(SKz.IDS, ''), 70) as ITEMCODE,
				Left(SKzNC.EAN, 60) as VNDITNUM,
				'' as CZ_CarKod,
				'' as LOCNCODE,  --TODO složizejší
				SKz.RefSklad as SKL_ID,
				ISNULL(SKz.StavZ, 0) as QTY,
				0 as QTYPACK,
				Left(ISNULL(SKzNC.MJEAN, ''), 10) as MJ,
				'' as DMJ,
				0 as TAXRATE,
				0 as PRICE0,
				0 as PRICE1,
				0 as PRICE2,
				0 as PRICE3,
				0 as PRICE4,
				0 as PRICE5,
				dbo.fask_func_PriznakSledovaniZasoby(SKz.RelSKzVC, SKz.ID,@EvidenceSarzi,@EvidenceVyrobnichCisel, @PohodaE1) as CZ_SerNum_Track, -- Snad funguje
				0 as CZ_SerNum_Delka,
				0 as CZ_Rez1_Track,
				0 as CZ_Rez2_Track, 
				0 as CZ_Rez3_Track, 
				0 as CZ_Rez4_Track, 
				0 as REZ1,
				0 as REZ2,
				0 as REZ3,
				0 as REZ4,
				Left(SKz.RefAD, 12) as ODB_ID,
				'' as mena_ID,
				'' as SERLTNUM,
				null as WEIGHT,
				null as TIMEFROM,
				null as TIMETO,
				GETDATE() as LSTMod,
				'' as loginid,
				0 as CZ_Expirace_Track,
				null as EXPIRACE
			FROM StwPh_04535667_2020.dbo.SKz as SKz INNER JOIN StwPh_04535667_2020.dbo.SKzNC as SKzNC ON SKz.ID = SKzNC.RefAg

			END--C1


	END
END --A1


SELECT Count(*) from FASK_ZASOBY

END

GO

/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_Get_Planovani_NacteniUserParams]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 -- =============================================
 -- Author:	Bc. Tadeas Divacky
 -- Create date: 22.01.2020
 -- Description:	Funkce která vraci uživatelske parametry pro filtrovani do načteí obchodniho požadavk pro Planovani
 -- =============================================
 CREATE FUNCTION [dbo].[FASK_Get_Planovani_NacteniUserParams]
 (	)
 RETURNS @Params TABLE
 (
	[ORD] [int] NULL,
 	[UserParam_1] [nvarchar](100) NULL,
	[UserParam_2] [nvarchar](100) NULL,
	[UserParam_3] [nvarchar](100) NULL,
	[UserParam_4] [nvarchar](100) NULL,
	[UserParam_5] [nvarchar](100) NULL
 )
 AS
 BEGIN
 
 
  insert into @Params ([ORD], [UserParam_1], [UserParam_2], [UserParam_3],[UserParam_4], [UserParam_5])
 		SELECT 
		op.ID as [ORD],
		[UserParam_1] = CASE o.VPrZapl    
			WHEN 1 THEN 'Zaplaceno'    
			ELSE 'Nezaplaceno'  
		END ,
		UzivSeznamPol.IDS as [UserParam_2],
		'' as [UserParam_3],
		'' as [UserParam_4],
		'' as [UserParam_5]
 		FROM StwPh_04535667_2020.dbo.OBJ o
		left join StwPh_04535667_2020.dbo.OBJpol op ON op.RefAg = o.ID
 		left join StwPh_04535667_2020.dbo.sVPULpol UzivSeznamPol ON UzivSeznamPol.ID = o.RefVPrDoprava
  	return
 
 END
 
GO


/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[fask_func_planovani_VydejFilterToSI]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 16.12.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[fask_func_planovani_VydejFilterToSI] 
(
	-- Add the parameters for the function here
	@ITEMNMBR nvarchar(40) = null,
	@SKL_ID nvarchar(20) = null,
	@ORD int = null
)
RETURNS bit
AS
BEGIN

	DECLARE @Result bit;
	DECLARE @Typ int;


	SET @Result = 0;

	--Funkce musí byt, ale defaultně by mnela vracet 0, jakožto FALSE, a tym padem to nebude pšenašet do SI žadne řadky


	SELECT distinct @Typ = S.RelSkTyp FROM StwPh_04535667_2020.dbo.SKz as S where S.ID = @ITEMNMBR 
	--AND S.RefSklad = @SKL_ID

--Vsechny = 0,
--Karta = 1,
--Textova = 2,
--Sluzba = 3,
--Komplet = 4,
--Vyrobek = 5,
--Souprava = 6

IF @Typ = 0
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 1
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 2
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 3
	BEGIN
		SET @Result = 1;
	END
ELSE IF  @Typ = 5
	BEGIN
		SET @Result = 0;
	END
ELSE IF  @Typ = 5
	BEGIN
		SET @Result = 0;
	END

return @Result;

END
GO

/**************************************************************************************/

/****** Object:  StoredProcedure [dbo].[fask_proc_DI2SE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Jedná se o proceduru sloužící pro vytvořenípředlohy na Výdej z Typu Dokladu TO
-- =============================================
CREATE PROCEDURE [dbo].[fask_proc_DI2SE] 
	@p1 nvarchar(20),
	@p2 nvarchar(20)
	AS
BEGIN
	SET NOCOUNT ON;

	-- zjistíme jakou akci provádíme
	DECLARE @coSeDeje varchar(12) = (SELECT TOP 1 [DOC_ID] FROM [CZMST_DI] WHERE [CountEntries] = @p2)
	DECLARE @MaxSE int = (SELECT TOP 1 MAX(CountEntries) FROM [CZMST_SE])

	DECLARE @CountEntries int;

	set @CountEntries = ISNULL(@MaxSE,0) + 1;


		if @coSeDeje = 'TO' begin
		-- TO, přegenerovani DI do SE

INSERT INTO CZMST_SE
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ITEMTYPE]
           ,[ITEMDESC]
           ,[VNDDOCNM]
           ,[VNDITNUM]
           ,[ORD]
           ,[CZ_CarKod]
           ,[SKL_ID]
           ,[LOCNCODE]
           ,[MJ]
           ,[QTYSHPPD]
           ,[QTYPACK]
           ,[CZ_DatVyr_Track]
           ,[CZ_DatVyr_Delka]
           ,[CZ_SerNum_Track]
           ,[CZ_SerNum_Delka]
           ,[CZ_SW_Track]
           ,[CZ_SW_Delka]
           ,[CZ_Doslo]
           ,[Note]
           ,[TYPEPAL]
           ,[QTYPAL]
           ,[PRIORITY]
           ,[PRINTED]
           ,[USERID]
           ,[CZ_REZ1_Track]
           ,[CZ_REZ2_Track]
           ,[ITEMCODE]
           ,[WEIGHT])
SELECT
			@CountEntries as CountEntries, 
			DI.DOC_ID + CONVERT(nvarchar(100),DI.CountEntries) as SOPNUMBE,
			DI.ITEMNMBR,
			'' as ITEMTYPE,
			Z.ITEMDESC,
			'' as VNDDOCNM,
			DI.VNDITNUM,
			DI.DEX_ROW_ID as ORD,
			DI.CZ_CarKod,
			DI.SKL_ID,
			DI.LOCNCODE,
			DI.MJ,
			DI.QTYSHPPD,
			DI.QTYPACK,
			0 as CZ_DatVyr_Track,
			0 as CZ_DatVyr_Delka,
			Z.CZ_SerNum_Track,
			Z.CZ_SerNum_Delka,
			0 as CZ_SW_Track,
			0 as CZ_SW_Delka,
			0 as CZ_Doslo,
			'' as Note,
			'' as TYPEPAL,
			0 as QTYPAL,
			0 as PRIORITY,
			0 as PRINTED,
			DI.USER_ID,
			0 as CZ_REZ1_Track,
			0 as CZ_REZ2_Track,
			DI.ITEMCODE,
			DI.WEIGHT
FROM CZMST_DI as DI LEFT JOIN FASK_ZASOBY AS Z ON Z.SKL_ID = DI.SKL_ID AND Z.ITEMNMBR = DI.ITEMNMBR
WHERE DI.CountEntries = @p2


INSERT INTO [CZMST_SE_SN]
           ([CountEntries]
           ,[SOPNUMBE]
           ,[ITEMNMBR]
           ,[ORD]
           ,[SERLNMBR]
           ,[QTY])
SELECT
			@CountEntries as CountEntries, 
			DI.DOC_ID + CONVERT(nvarchar(100),DI.CountEntries) as SOPNUMBE,
			DI.ITEMNMBR,
			DI.DEX_ROW_ID as ORD,
			DI.SERLTNUM,
			DI.QTYSHPPD
FROM CZMST_DI AS DI
WHERE DI.SERLTNUM IS NOT NULL AND DI.SERLTNUM != '' AND DI.CountEntries = @p2

	end
	-- neznámý cosedeje ;)
	else begin
		return -1
	end

END

GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_Get_POHODA_LokMechMapingID]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 -- =============================================
 -- Author:	Bc. Tadeas Divacky
 -- Create date: 22.01.2020
 -- Description:	Funkce která vraci uživatelske parametry pro filtrovani do načteí obchodniho požadavk pro Planovani
 -- =============================================
 CREATE FUNCTION [dbo].[FASK_Get_POHODA_LokMechMapingID]
 (	
 	@ITEMNMBR_Zdroj nvarchar(40) = null,
	@SKL_ID_Zdroj nvarchar(20) = null,
	@SKL_ID_Cil nvarchar(20) = null
 )
 RETURNS @Params TABLE
 (
	[ITEMNMBR] nvarchar(40) NULL,
 	[ITEMDESC] [nvarchar](100) NULL
 )
 AS
 BEGIN
 
 
  insert into @Params ([ITEMNMBR], [ITEMDESC])
	SELECT 
	N.ID as [ITEMNMBR],
	LEFT(N.Nazev,100) as [ITEMDESC] 
	FROM
	(SELECT IDS, EAN, Nazev,MJ, RefAD, RelSKzVC from StwPh_04535667_2020.dbo.Skz 
	where ID = @ITEMNMBR_Zdroj AND RefSklad = @SKL_ID_Zdroj) as O
	LEFT JOIN StwPh_04535667_2020.dbo.Skz as N ON 
	ISNULL(N.IDS,'') = ISNULL(O.IDS,'') 
	AND  ISNULL(N.EAN,'') = ISNULL(O.EAN,'')
	AND ISNULL(N.Nazev,'') = ISNULL(O.Nazev,'')
	AND ISNULL(N.MJ,'') = ISNULL(O.MJ,'')
	AND ISNULL(N.RefAD,'') = ISNULL(O.RefAD,'')
	AND ISNULL(N.RelSKzVC,'') = ISNULL(O.RelSKzVC,'')
	AND N.ID != @ITEMNMBR_Zdroj
	AND N.RefSklad = @SKL_ID_Cil

  	return
 
 END

GO
 
/**************************************************************************************/
/****** Object:  Table [dbo].[CZMST_TASK_VERIFY]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CZMST_TASK_VERIFY](
	[taskname] [nvarchar](50) NOT NULL,
	[verify_pwd] [nvarchar](50) NOT NULL,
	[taskdesc] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/**************************************************************************************/
/****** Object:  StoredProcedure [dbo].[CZMST_Verify_Operation]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 22.4.2020
-- Description:	Autentifikace operace, přeneseno z colorprofi
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Verify_Operation] 
	-- Add the parameters for the stored procedure here
	@operation nvarchar(20),	-- operation to verify
	@pwdhash nvarchar(50),		-- pwd hash
	@verified bit OUTPUT		-- 0 = not verified, 1=verified
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	set @verified = 0

	IF EXISTS(SELECT TOP 1 1 FROM CZMST_TASK_VERIFY WHERE taskname=@operation and verify_pwd=@pwdhash) BEGIN
		set @verified = 1
		return
	end

	Return 0
END

GO
/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_Get_Planovani_Navrhar_FIFO_OBJ] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 -- =============================================
 -- Author:	Bc. Tadeas Divacky
 -- Create date: 22.01.2020
 -- Description:	Funkce která vraci status zda lze položku zaplanovat
 -- =============================================
CREATE FUNCTION [dbo].[FASK_Get_Planovani_Navrhar_FIFO_OBJ]
 (	
 	@ORD int ,
	@ITEMNMBR int,
	@QTY numeric(19,5)
 )
 RETURNS @Params TABLE
 (
	[Flag] [bit] NULL
 )
 AS
 BEGIN
 
DECLARE @CNT_Nalezene int
DECLARE @Skladem numeric(19,5)
DECLARE @Vytvoreno datetime

DECLARE @Zaplacena bit


SELECT @Zaplacena = O.VPrZapl FROM
StwPh_04535910.dbo.OBJ as O 
left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
where 1 = 1
AND pol.RefSKz = @ITEMNMBR
AND pol.ID = @ORD

IF @Zaplacena = 0
	BEGIN
		insert into @Params ([Flag]) SELECT 0 as Flag
		return
	END


SELECT @Vytvoreno = o.DatCreate FROM
StwPh_04535910.dbo.OBJ as O 
left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
where 1 = 1
AND pol.ID = @ORD

SELECT @CNT_Nalezene = count(*) FROM
StwPh_04535910.dbo.OBJ as O 
left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
left join StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = pol.RefSKz 
where 1 = 1
AND pol.RefSKz = @ITEMNMBR
AND O.Vyrizeno = 0
AND O.VPrZapl = 0
AND pol.ID != @ORD
AND o.DatCreate < @Vytvoreno

SELECT @Skladem = StavZ FROM StwPh_04535667_2020.dbo.SKz
WHERE ID = @ITEMNMBR

IF @CNT_Nalezene > 0
	BEGIN

	declare @MN numeric(19,5)

		SELECT @MN = SUM(pol.Mnozstvi - pol.Dodano) FROM
		StwPh_04535910.dbo.OBJ as O 
		left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
		left join StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = pol.RefSKz 
		where 1 = 1
		AND pol.RefSKz = @ITEMNMBR
		AND O.Vyrizeno = 0
		AND O.VPrZapl = 0
		AND pol.ID != @ORD
		AND o.DatCreate < @Vytvoreno
		group by pol.ID

		IF (@QTY + @MN) <= @Skladem
			BEGIN
				insert into @Params ([Flag]) SELECT 1 as Flag
			END
		ELSE
			BEGIN
				insert into @Params ([Flag]) SELECT 0 as Flag
			END

	END
ELSE 
	BEGIN
		insert into @Params ([Flag]) SELECT 1 as Flag
	END

 
 return


 
 END
 
GO


/**************************************************************************************/
/****** Object:  UserDefinedFunction [dbo].[FASK_GetListPolozekPoSluzbe]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeáš Divácký
-- Create date: 4.5.2021
-- Description:	Funkce která vrací list ID služek ktere jsou pod kartou v objednávce
-- =============================================
CREATE FUNCTION [dbo].[FASK_GetListPolozekPoSluzbe]
(
	-- Add the parameters for the function here
	@ORD int 
)
RETURNS @List_ORD TABLE 
(
	ID_Polozky int
)
AS
BEGIN
	

DECLARE @ID int;
DECLARE @RelSkTyp int;


DECLARE curPol CURSOR FOR
SELECT 
o.ID, 
zas.RelSkTyp 
FROM
(SELECT OrderFld, RefAg  FROM StwPh_04535667_2020.dbo.OBJpol as op
left join StwPh_04535667_2020.dbo.SKz as z ON z.ID = op.RefSKz
where 1 = 1 AND op.ID = @ORD) as x
LEFT JOIN StwPh_04535667_2020.dbo.OBJpol as o ON o.RefAg = x.RefAg
LEFT JOIN StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = o.RefSKz
where 1=1
AND o.RefAg = x.RefAg
AND o.OrderFld > x.OrderFld
order by o.OrderFld

OPEN curPol
WHILE (1=1) 
	BEGIN
	
	FETCH NEXT FROM curPol INTO @ID, @RelSkTyp 
	IF @@FETCH_STATUS <> 0
	BEGIN
		BREAK
	END

	IF @RelSkTyp in (3)
		BEGIN
			insert into @List_ORD(ID_Polozky) values (@ID)
		END
	ELSE
		BEGIN
			BREAK
		END

END
	
CLOSE curPol
DEALLOCATE curPol

--SELECT ORD from @List_ORD

	RETURN 
END

GO

/**************************************************************************************/

