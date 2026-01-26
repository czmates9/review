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