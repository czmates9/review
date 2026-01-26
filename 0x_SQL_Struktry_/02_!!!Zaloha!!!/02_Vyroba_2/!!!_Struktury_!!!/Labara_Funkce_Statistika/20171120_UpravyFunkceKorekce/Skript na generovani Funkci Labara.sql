-- =============================================
-- Author:		TaD
-- Create date: 20.11.2017
-- Description:	Korekce ke schvaleni
-- =============================================
CREATE FUNCTION [dbo].[fask_FASK_Filter_korekce_KeSchvaleni]
(
	-- Add the parameters for the function here
	@in_DatumOd datetime,
	@in_DatumDo datetime,
	@in_Osoba nvarchar(20),
	@in_Stroj nvarchar(20),
	@in_Zakazka nvarchar(20)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result float

	if((@in_Stroj is null or @in_Stroj = '') and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
		select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 0
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 

	end 
	else if(@in_Stroj is not null  and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 0
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by [ZAM ID] 

		--RETURN @Result
	end	
	else if((@in_Stroj is null or @in_Stroj = '') and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 0
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 

		--RETURN @Result
	end
	else if(@in_Stroj is not null and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 0
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 

		--RETURN @Result
	end

	-- Return the result of the function
	RETURN isnull(@Result,0)
END


GO
/****** Object:  UserDefinedFunction [dbo].[fask_FASK_Filter_korekce_NeSchvalene]    Script Date: 20.11.2017 12:47:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TaD
-- Create date: 20.11.2017
-- Description:	Korekce neschvalene
-- =============================================
CREATE FUNCTION [dbo].[fask_FASK_Filter_korekce_NeSchvalene]
(
	-- Add the parameters for the function here
	@in_DatumOd datetime,
	@in_DatumDo datetime,
	@in_Osoba nvarchar(20),
	@in_Stroj nvarchar(20),
	@in_Zakazka nvarchar(20)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result float

	if((@in_Stroj is null or @in_Stroj = '') and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
			select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 2
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	
	end 
	else if(@in_Stroj is not null  and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 2
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end
	else if((@in_Stroj is null or @in_Stroj = '') and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 2
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end
	else if(@in_Stroj is not null and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 2
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end


	-- Return the result of the function
	RETURN isnull(@Result,0)
END

GO
/****** Object:  UserDefinedFunction [dbo].[fask_FASK_Filter_korekce_Schvalene]    Script Date: 20.11.2017 12:47:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TaD
-- Create date: 20.11.2017
-- Description:	Korekce schvalene
-- =============================================
CREATE FUNCTION [dbo].[fask_FASK_Filter_korekce_Schvalene]
(
	-- Add the parameters for the function here
	@in_DatumOd datetime,
	@in_DatumDo datetime,
	@in_Osoba nvarchar(20),
	@in_Stroj nvarchar(20),
	@in_Zakazka nvarchar(20)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result float

		if((@in_Stroj is null or @in_Stroj = '') and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
			select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 1
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
  end else if(@in_Stroj is not null  and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 1
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end
	else if((@in_Stroj is null or @in_Stroj = '') and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 1
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end
	else if(@in_Stroj is not null and @in_Zakazka is not null )
	begin
	 	select @Result = SUM(zak.[CAS])-- as [PocetNormohodin]
		from dbo.[_117 ZAK] as zak
		left join dbo.GENERAL G on G.KEY1 = zak.KOD
		where
			zak.DAT between @in_DatumOd and @in_DatumDo
			and zak.[ZAM ID] = @in_Osoba
			and zak.[MACHINE ID] = @in_Stroj
			and zak.[ZAKAZKA ID] = @in_Zakazka
			and zak.[FLAG KOREKCE] = 1
			and zak.[KOREKCE STAV] = 1
			and G.INT1 = 0
		--group by  [firstname], [surname],
		group by zak.[ZAM ID] 
	end


	-- Return the result of the function
	RETURN isnull(@Result,0)
END

GO
/****** Object:  UserDefinedFunction [dbo].[fask_FASK_Filter_PocetNormohodin]    Script Date: 20.11.2017 12:47:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JiS
-- Create date: 7.11.2017
-- Description:	Pocet normohodin vyroby
-- =============================================
CREATE FUNCTION [dbo].[fask_FASK_Filter_PocetNormohodin] 
(
	-- Add the parameters for the function here
	@in_DatumOd datetime,
	@in_DatumDo datetime,
	@in_Osoba nvarchar(20),
	@in_Stroj nvarchar(20),
	@in_Zakazka nvarchar(20)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result float


	if((@in_Stroj is null or @in_Stroj = '') and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
		select @Result = SUM([CAS NORMA])-- as [PocetNormohodin]
		from dbo.[_117 ZAK]
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [FLAG KOREKCE] = 0
			--and [KOREKCE STAV] is null
		--group by  [firstname], [surname],
		group by [ZAM ID] 
	end else if(@in_Stroj is not null  and (@in_Zakazka is null or @in_Zakazka = ''))
	begin
		select @Result = SUM([CAS NORMA])-- as [PocetNormohodin]
		from dbo.[_117 ZAK]
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [MACHINE ID] = @in_Stroj
			and [FLAG KOREKCE] = 0
			--and [KOREKCE STAV] is null
		--group by  [firstname], [surname],
		group by [ZAM ID] 
	end
	else if((@in_Stroj is null or @in_Stroj = '') and @in_Zakazka is not null )
	begin
		select @Result = SUM([CAS NORMA])-- as [PocetNormohodin]
		from dbo.[_117 ZAK]
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [ZAKAZKA ID] = @in_Zakazka
			and [FLAG KOREKCE] = 0
			--and [KOREKCE STAV] is null
		--group by  [firstname], [surname],
		group by [ZAM ID] 
	end
	else if(@in_Stroj is not null and @in_Zakazka is not null )
	begin
		select @Result = SUM([CAS NORMA])-- as [PocetNormohodin]
		from dbo.[_117 ZAK]
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [MACHINE ID] = @in_Stroj
			and [ZAKAZKA ID] = @in_Zakazka
			and [FLAG KOREKCE] = 0
			--and [KOREKCE STAV] is null
		--group by  [firstname], [surname],
		group by [ZAM ID] 
	end

	-- Return the result of the function
	RETURN isnull(@Result,0)
END


GO
/****** Object:  UserDefinedFunction [dbo].[FASK_Filter]    Script Date: 20.11.2017 12:47:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 20.10.2017
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[FASK_Filter]
(	
	-- Add the parameters for the function here
	@in_DatumOd datetime ,
	@in_DatumDo datetime,
	@in_NedokonceneZakazky bit,
	@in_Osoba nchar(20),
	@in_Stroj nchar(20),
	@in_Zakazka nchar(20)
)
RETURNS @t TABLE
(
	[DatumOd] [datetime] NULL,
	[DatumDo] [datetime] NULL,
	[PocetNormohodin] [nchar](20) NULL,
	[korekce_KeSchvaleni] [nchar](20) NULL,
	[korekce_NeSchvalene] [nchar](20) NULL,
	[korekce_Schvalene] [nchar](20) NULL,
	--[ID] [nchar](10) NULL,
	[Firstname] [nvarchar](20) NULL,
	[Surname] [nvarchar](20) NULL
)
AS
BEGIN


declare @FLAG_KOREKCE bit = 'false'

	--declare
	--	@_DatumOd datetime ,
	--	@_DatumDo datetime,
	--	@_NedokonceneZakazky bit,
	--	@_Osoba nchar(20),
	--	@_Stroj nchar(20),
	--	@_Zakazka nchar(20)	
	
	--set @_DatumOd = @in_DatumOd
	--set @_DatumDo = @in_DatumDo
	--set @_NedokonceneZakazky = @in_NedokonceneZakazky
	--set @_Osoba = @in_Osoba
	--set @_Stroj = @in_Stroj
	--set @_Zakazka = @in_Zakazka

		declare
		@local_PocetNormohodin nchar(20),
	    @local_korekce_KeSchvaleni nchar(20),
	    @local_korekce_NeSchvalene nchar(20),
	    @local_korekce_Schvalene nchar(20)
	
	if ((YEAR(@in_DatumDo) > 3000 OR @in_DatumDo is null or @in_DatumDo = '' )  and (YEAR(@in_DatumOd) < 1900 OR @in_DatumOd is null or @in_DatumDo = ''))
	begin
		set @in_DatumOd = DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) --minimanlni den v tomto mesici z getdate() pomoci adddays...
		set @in_DatumDo = DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) --maximanlni den v tomto mesici z getdate() pomoci adddays...		
	end 
	
	else if(YEAR(@in_DatumDo) > 3000 OR @in_DatumDo is null or @in_DatumDo = '')
	begin	
	 set @in_DatumDo = DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, @in_DatumOd) + 1, 0)) -- maximalny den

	end
	
	else if (YEAR(@in_DatumOd) < 1900 OR @in_DatumOd is null or @in_DatumOd = '')
	begin
		set @in_DatumOd =  DATEADD(mm, DATEDIFF(mm, 0, @in_DatumDo), 0); -- minimalny den		
	end


	if(@in_Osoba is null or @in_Osoba = '' )
	begin
			insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		select 
			 'unknown' as [Firstname]
			,'unknown' as [Surname]
			,DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) as [DatumDo]
			, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) as [DatumOd]
			,0 as PocetNormohodin--SELECT fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) --SUM([CAS NORMA]) as [PocetNormohodin]
			,0 as korekce_KeSchvaleni--case when [KOREKCE STAV]=0 then SUM(CAS)else SUM(0) end as [korekce_KeSchvaleni]
			,0 as korekce_Schvalene--case when [KOREKCE STAV]=1 then SUM(CAS)else SUM(0) end as [korekce_Schvalene]
			,0 as korekce_NeSchvalene--case when [KOREKCE STAV]=2 then SUM(CAS)else SUM(0) end as [korekce_NeSchvalene]

	end 
	
	else if(@in_Osoba is not null and (@in_Stroj is null or @in_Stroj = '') and (@in_Zakazka is null or @in_Zakazka = ''))
	 begin
	 	set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_KeSchvaleni = dbo.fask_FASK_Filter_korekce_KeSchvaleni(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_KeSchvaleni= dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_korekce_NeSchvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	set @local_korekce_Schvalene = dbo.fask_FASK_Filter_korekce_Schvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_Schvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		select 
			 l.firstname Firstname
			,l.surname Surname
			,MAX(DAT) as [DatumDo]
			,MIN(DAT) as [DatumOd]
			,@local_PocetNormohodin as [PocetNormohodin] --SELECT fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) --SUM([CAS NORMA]) as [PocetNormohodin]
			,@local_korekce_KeSchvaleni as korekce_KeSchvaleni--case when [KOREKCE STAV]=0 then SUM(CAS)else SUM(0) end as [korekce_KeSchvaleni]
			,@local_korekce_Schvalene as korekce_Schvalene--case when [KOREKCE STAV]=1 then SUM(CAS)else SUM(0) end as [korekce_Schvalene]
			,@local_korekce_NeSchvalene as korekce_NeSchvalene--case when [KOREKCE STAV]=2 then SUM(CAS)else SUM(0) end as [korekce_NeSchvalene]
		from dbo.[_117 ZAK]
		left join Logins l on l.id = @in_Osoba
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			Group by firstname, surname
	end

		else if(@in_Osoba is not null and (@in_Stroj is null or @in_Stroj = '') and @in_Zakazka is not null )
	 begin
	 	set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_KeSchvaleni = dbo.fask_FASK_Filter_korekce_KeSchvaleni(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_KeSchvaleni= dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_korekce_NeSchvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	set @local_korekce_Schvalene = dbo.fask_FASK_Filter_korekce_Schvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_Schvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		select 
			 l.firstname Firstname
			,l.surname Surname
			,MAX(DAT) as [DatumDo]
			,MIN(DAT) as [DatumOd]
			,@local_PocetNormohodin as [PocetNormohodin] --SELECT fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) --SUM([CAS NORMA]) as [PocetNormohodin]
			,@local_korekce_KeSchvaleni as korekce_KeSchvaleni--case when [KOREKCE STAV]=0 then SUM(CAS)else SUM(0) end as [korekce_KeSchvaleni]
			,@local_korekce_Schvalene as korekce_Schvalene--case when [KOREKCE STAV]=1 then SUM(CAS)else SUM(0) end as [korekce_Schvalene]
			,@local_korekce_NeSchvalene as korekce_NeSchvalene--case when [KOREKCE STAV]=2 then SUM(CAS)else SUM(0) end as [korekce_NeSchvalene]
		from dbo.[_117 ZAK]
		left join Logins l on l.id = @in_Osoba
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [ZAKAZKA ID] = @in_Zakazka
			Group by firstname, surname
	end


	else if(@in_Osoba is not null and @in_Stroj is not null  and (@in_Zakazka is null or @in_Zakazka = ''))
	 begin
	 	set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_KeSchvaleni = dbo.fask_FASK_Filter_korekce_KeSchvaleni(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_KeSchvaleni= dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_korekce_NeSchvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	set @local_korekce_Schvalene = dbo.fask_FASK_Filter_korekce_Schvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_Schvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		select 
			 l.firstname Firstname
			,l.surname Surname
			,MAX(DAT) as [DatumDo]
			,MIN(DAT) as [DatumOd]
			,@local_PocetNormohodin as [PocetNormohodin] --SELECT fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) --SUM([CAS NORMA]) as [PocetNormohodin]
			,@local_korekce_KeSchvaleni as korekce_KeSchvaleni--case when [KOREKCE STAV]=0 then SUM(CAS)else SUM(0) end as [korekce_KeSchvaleni]
			,@local_korekce_Schvalene as korekce_Schvalene--case when [KOREKCE STAV]=1 then SUM(CAS)else SUM(0) end as [korekce_Schvalene]
			,@local_korekce_NeSchvalene as korekce_NeSchvalene--case when [KOREKCE STAV]=2 then SUM(CAS)else SUM(0) end as [korekce_NeSchvalene]
		from dbo.[_117 ZAK]
		left join Logins l on l.id = @in_Osoba
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [MACHINE ID] = @in_Stroj
			Group by firstname, surname
	end
		else if(@in_Osoba is not null and @in_Stroj is not null  and @in_Zakazka is not null )
	 begin
	 	set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_PocetNormohodin = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_KeSchvaleni = dbo.fask_FASK_Filter_korekce_KeSchvaleni(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_KeSchvaleni= dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_korekce_NeSchvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_NeSchvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	set @local_korekce_Schvalene = dbo.fask_FASK_Filter_korekce_Schvalene(@in_DatumOd, @in_DatumDo, @in_Osoba, @in_Stroj, @in_Zakazka);
	--set @local_korekce_Schvalene = dbo.fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) 
	
	
	insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		select 
			 l.firstname Firstname
			,l.surname Surname
			,MAX(DAT) as [DatumDo]
			,MIN(DAT) as [DatumOd]
			,@local_PocetNormohodin as [PocetNormohodin] --SELECT fask_FASK_Filter_PocetNormohodin('2017-1-10' ,'2017-1-11','100',null ,null) --SUM([CAS NORMA]) as [PocetNormohodin]
			,@local_korekce_KeSchvaleni as korekce_KeSchvaleni--case when [KOREKCE STAV]=0 then SUM(CAS)else SUM(0) end as [korekce_KeSchvaleni]
			,@local_korekce_Schvalene as korekce_Schvalene--case when [KOREKCE STAV]=1 then SUM(CAS)else SUM(0) end as [korekce_Schvalene]
			,@local_korekce_NeSchvalene as korekce_NeSchvalene--case when [KOREKCE STAV]=2 then SUM(CAS)else SUM(0) end as [korekce_NeSchvalene]
		from dbo.[_117 ZAK]
		left join Logins l on l.id = @in_Osoba
		where
			DAT between @in_DatumOd and @in_DatumDo
			and [ZAM ID] = @in_Osoba
			and [MACHINE ID] = @in_Stroj
			and [ZAKAZKA ID] = @in_Zakazka
			Group by firstname, surname
	end
			


	else 
	begin
		insert into @t (Firstname, Surname, DatumDo, DatumOd, PocetNormohodin, korekce_KeSchvaleni, korekce_Schvalene, korekce_NeSchvalene)
		Select 
			'firstname' Firstname,
			'surname' Surname,
			NULL DatumDo,
			NULL DatumOd,
			NULL PocetNormohodin,
			NULL korekce_KeSchvaleni,
			NULL korekce_NeSchvalene,
			NULL korekce_Schvalene 		
	end
	return
	
END





GO
