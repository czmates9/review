/****** Object:  UserDefinedFunction [dbo].[FASK_Get_PohybyFromPOHODA]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data pro pohyby v IS POHODA
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_PohybyFromPOHODA] 
(
	-- Add the parameters for the function here
)
RETURNS
 @returnList TABLE 
 (
	[ITEMNMBR] [nvarchar](100) NULL,
	[TypPohybu] [nvarchar](100) NULL,
	[ZdrojPohybu] [nvarchar](100) NULL,
	[DatumPohybu]datetime null,
	[PocetNaPohybu][numeric](19,5) null,
	[StavPoPohybu] [numeric](19,5) null,
	[CisloDokladu][nvarchar](100) null,
	[KdoVytvoril] [nvarchar](100) null,
	[Ucetni] [nvarchar](100) null,
	[DatumVytvoreni] datetime null,
	[DatumUlozeni] datetime null
 )
AS
BEGIN


----------------------------------------------
------------------Faktura Vydana -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Faktura Vydaná' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.FA as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 2

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Faktura Prijata -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Faktura Přijatá' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.FA as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 3

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Prijemka -------------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Příjemka' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.SKPP as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 6

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Výdejka -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Výdejka' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.SKPV as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 7

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Výroba -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Výroba' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.SKMV as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 9

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Prevodka -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Převodka' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.SKMP as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 8

----------------------------------------------
----------------------------------------------
----------------------------------------------

----------------------------------------------
------------------Prodejka -------------
----------------------------------------------


INSERT INTO @returnList 
(
	[ITEMNMBR],
	[TypPohybu],
	[ZdrojPohybu],
	[DatumPohybu],
	[PocetNaPohybu],
	[StavPoPohybu],
	[CisloDokladu],
	[KdoVytvoril],
	[Ucetni],
	[DatumVytvoreni],
	[DatumUlozeni] 
)
select 
poh.RefSKz as ITEMNMBR,
CASE	
	WHEN poh.RelOP = 1 THEN 'Příjem'
	WHEN poh.RelOP = 2 THEN 'Výdej'
	ELSE '-'
END as TypPohybu,
'Prodejka' as ZdrojPohybu,
poh.Datum as DatumPohybu,
poh.PohPMJ as PocetNaPohybu,
poh.StavZ as  StavPoPohybu,
poh.Cislo as CisloDokladu,
AGENDA.Creator as KdoVytvoril,
AGENDA.Ucetni as Ucetni,
AGENDA.DatCreate as DatumVytvoreni,
AGENDA.DatSave as DatumUlozeni
from StwPh_04535667_2020.dbo.SKzPoh as poh
left join StwPh_04535667_2020.dbo.PH as AGENDA ON AGENDA.Cislo = poh.Cislo
where RelAg = 10

----------------------------------------------
----------------------------------------------
----------------------------------------------

return 
END
GO


/**************************************************************************************/
