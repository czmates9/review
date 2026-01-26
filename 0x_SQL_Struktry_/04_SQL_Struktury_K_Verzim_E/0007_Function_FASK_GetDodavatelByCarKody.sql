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