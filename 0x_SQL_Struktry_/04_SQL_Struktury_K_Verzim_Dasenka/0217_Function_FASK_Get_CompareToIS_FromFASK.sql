/****** Object:  UserDefinedFunction [dbo].[FASK_Get_CompareToIS_FromFASK]    Script Date: 22.02.2022 11:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data pro import do pohody
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_CompareToIS_FromFASK] 
(
	-- Add the parameters for the function here
		@SKL_ID nvarchar(25),
		@V_0 bit,
		@V_1 bit,
		@V_2 bit,
		@V_3 bit,
		@V_4 bit,
		@V_5 bit,
		@V_6 bit
)
RETURNS
 @returnList TABLE 
 (
	[ITEMNMBR] [nvarchar](40) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[Expirace] [datetime] NULL,
	[status] int not null
 )
AS
BEGIN

----------------------------------------
-- Varianta 0  -------------------------
----------------------------------------

IF @V_0 = 1
	BEGIN 

			INSERT INTO @returnList 
			(
			[ITEMNMBR],
			[QTYSHPPD],
			[SKL_ID] ,
			[SERLTNUM],
			[Expirace],
			[status]
			)
			SELECT
		 LOK.ITEMNMBR
		, SUM(LOK.QTYSHPPD) as QTYSHPPD
		, LOK.SKL_ID
		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
		, LOK.EXPIRATION as [Expirace]
		, 0 as [status]
		FROM
(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
		LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
		LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
		where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
		--where LOK.SKL_ID = @SKL_ID 
		group by
		  Z.ID,
		  Z.RefSklad,
		  S.VCislo,
		  S.DatExp,
		 LOK.ITEMNMBR
		, LOK.SKL_ID
		, LOK.SERLTNUM
		, LOK.EXPIRATION
		having Z.ID is null

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 1  -------------------------
----------------------------------------

IF @V_1 = 1
	BEGIN 

			INSERT INTO @returnList 
			(
			[ITEMNMBR],
			[QTYSHPPD],
			[SKL_ID] ,
			[SERLTNUM],
			[Expirace],
			[status]
			)
			SELECT
		 LOK.ITEMNMBR
		, SUM(LOK.QTYSHPPD) as QTYSHPPD
		, LOK.SKL_ID
		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
		, LOK.EXPIRATION as [Expirace]
		, 1 as [status]
		FROM
		(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
		LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
		LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
		--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
		where LOK.SKL_ID = @SKL_ID 
		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
		group by
		  Z.ID,
		  Z.RefSklad,
		  S.VCislo,
		  S.DatExp,
		 LOK.ITEMNMBR
		, LOK.SKL_ID
		, LOK.SERLTNUM
		, LOK.EXPIRATION
		having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) < 0
		AND SUM(Z.StavZ) > 0

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 2  -------------------------
----------------------------------------

IF @V_2 = 1
	BEGIN 

			INSERT INTO @returnList 
			(
			[ITEMNMBR],
			[QTYSHPPD],
			[SKL_ID] ,
			[SERLTNUM],
			[Expirace],
			[status]
			)
			SELECT
		 LOK.ITEMNMBR
		, SUM(LOK.QTYSHPPD) as QTYSHPPD
		, LOK.SKL_ID
		, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
		, LOK.EXPIRATION as [Expirace]
		, 2 as [status]
		FROM
		(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
		LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
		LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
		--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
		where LOK.SKL_ID = @SKL_ID 
		AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
		group by
		  Z.ID,
		  Z.RefSklad,
		  S.VCislo,
		  S.DatExp,
		 LOK.ITEMNMBR
		, LOK.SKL_ID
		, LOK.SERLTNUM
		, LOK.EXPIRATION
		having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) > 0
		AND SUM(LOK.QTYSHPPD) > 0

	END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 3  -------------------------
----------------------------------------

IF @V_3 = 1
	BEGIN 

	INSERT INTO @returnList 
	(
	[ITEMNMBR],
	[QTYSHPPD],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
 LOK.ITEMNMBR
, SUM(LOK.QTYSHPPD) as QTYSHPPD
, LOK.SKL_ID
, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
, LOK.EXPIRATION as [Expirace]
, 3 as [status]
FROM
(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
where LOK.SKL_ID = @SKL_ID 
AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
group by
  Z.ID,
  Z.RefSklad,
  S.VCislo,
  S.DatExp,
 LOK.ITEMNMBR
, LOK.SKL_ID
, LOK.SERLTNUM
, LOK.EXPIRATION
having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) = 0
AND SUM(LOK.QTYSHPPD) > 0
AND SUM(Z.StavZ) > 0

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 4  -------------------------
----------------------------------------

IF @V_4 = 1
	BEGIN 

	INSERT INTO @returnList 
	(
	[ITEMNMBR],
	[QTYSHPPD],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
 LOK.ITEMNMBR
, SUM(LOK.QTYSHPPD) as QTYSHPPD
, LOK.SKL_ID
, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
, LOK.EXPIRATION as [Expirace]
, 4 as [status]
FROM
(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
where LOK.SKL_ID = @SKL_ID 
AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
group by
  Z.ID,
  Z.RefSklad,
  S.VCislo,
  S.DatExp,
 LOK.ITEMNMBR
, LOK.SKL_ID
, LOK.SERLTNUM
, LOK.EXPIRATION
having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) < 0
AND SUM(Z.StavZ) = 0

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 5  -------------------------
----------------------------------------

IF @V_5 = 1
	BEGIN 

	INSERT INTO @returnList 
	(
	[ITEMNMBR],
	[QTYSHPPD],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
 LOK.ITEMNMBR
, SUM(LOK.QTYSHPPD) as QTYSHPPD
, LOK.SKL_ID
, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
, LOK.EXPIRATION as [Expirace]
, 5 as [status]
FROM
(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
where LOK.SKL_ID = @SKL_ID 
AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
group by
  Z.ID,
  Z.RefSklad,
  S.VCislo,
  S.DatExp,
 LOK.ITEMNMBR
, LOK.SKL_ID
, LOK.SERLTNUM
, LOK.EXPIRATION
having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) > 0
AND SUM(LOK.QTYSHPPD) = 0

END

----------------------------------------
----------------------------------------
----------------------------------------


----------------------------------------
-- Varianta 6  -------------------------
----------------------------------------

IF @V_6 = 1
	BEGIN 

	INSERT INTO @returnList 
	(
	[ITEMNMBR],
	[QTYSHPPD],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
 LOK.ITEMNMBR
, SUM(LOK.QTYSHPPD) as QTYSHPPD
, LOK.SKL_ID
, ISNULL(LOK.SERLTNUM,'') as SERLTNUM
, LOK.EXPIRATION as [Expirace]
, 6 as [status]
FROM
(
	SELECT
	SUM(QTYSHPPD) as QTYSHPPD,
	ITEMNMBR, 
	SERLTNUM, 
	SKL_ID, 
	EXPIRATION
FROM
CZMST_SkladLokace_Stav 
Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION)
as LOK 
LEFT JOIN StwPh_04535667_2020.dbo.SKz AS Z ON Z.ID = LOK.ITEMNMBR
LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
--where LOK.SKL_ID = @SKL_ID AND LOK.QTYSHPPD > 0
where LOK.SKL_ID = @SKL_ID 
AND ISNULL(S.VCislo,'') = ISNULL(LOK.SERLTNUM,'')
group by
  Z.ID,
  Z.RefSklad,
  S.VCislo,
  S.DatExp,
 LOK.ITEMNMBR
, LOK.SKL_ID
, LOK.SERLTNUM
, LOK.EXPIRATION
having (SUM(Z.StavZ) - SUM(LOK.QTYSHPPD)) = 0
AND SUM(LOK.QTYSHPPD) = 0
AND SUM(Z.StavZ) = 0

END

----------------------------------------
----------------------------------------
----------------------------------------


return 
END

GO

/**************************************************************************************/