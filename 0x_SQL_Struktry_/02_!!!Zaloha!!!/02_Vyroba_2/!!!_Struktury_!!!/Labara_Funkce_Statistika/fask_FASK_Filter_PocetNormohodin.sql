/****** Object:  UserDefinedFunction [dbo].[fask_FASK_Filter_PocetNormohodin]    Script Date: 8.11.2017 11:20:24 ******/
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


