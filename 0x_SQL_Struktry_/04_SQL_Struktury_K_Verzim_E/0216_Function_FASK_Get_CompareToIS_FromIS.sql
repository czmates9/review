/****** Object:  UserDefinedFunction [dbo].[FASK_Get_CompareToIS_FromIS]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	Vrací data pro import do pohody
-- =============================================
CREATE FUNCTION [dbo].[FASK_Get_CompareToIS_FromIS] 
(
	-- Add the parameters for the function here
		@SKL_ID nvarchar(25) ,
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
	[QTYSHPPD_Pohoda] [numeric](19, 5) NOT NULL,
	[QTYSHPPD_LokMech] [numeric](19, 5) NULL,
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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
		x.ITEMNMBR,
		SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
		SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
		x.SKL_ID,
		x.SERLTNUM,
		x.EXPIRATION as Expirace,
		0 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	--where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
		x.ITEMNMBR,
		x.SKL_ID,
		x.SERLTNUM,
		x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having LOK.ITEMNMBR is null

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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  1 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) < 0
	AND SUM(x.QTYSHPPD) > 0

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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  2 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) > 0
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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  3 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) = 0
	AND SUM(x.QTYSHPPD) > 0
	AND SUM(LOK.QTYSHPPD) > 0

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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  4 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) < 0
	AND SUM(x.QTYSHPPD) = 0

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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  5 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) > 0
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
	[QTYSHPPD_Pohoda],
	[QTYSHPPD_LokMech],
	[SKL_ID] ,
	[SERLTNUM],
	[Expirace],
	[status]
	)
	SELECT
	  x.ITEMNMBR,
	  SUM(x.QTYSHPPD) as QTYSHPPD_Pohoda,
	  SUM(LOK.QTYSHPPD) as QTYSHPPD_LokMech,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION as Expirace,
	  6 as [status]
	FROM
	(
	SELECT
	Z.ID as ITEMNMBR,
	Z.StavZ as QTYSHPPD,
	ISNULL(S.VCislo,'') as SERLTNUM,
	Z.RefSklad as SKL_ID,
	S.DatExp as EXPIRATION
	FROM StwPh_04535667_2020.dbo.SKz AS Z
	LEFT JOIN StwPh_04535667_2020.dbo.SKzVC as S ON S.RefAg = Z.ID
	) as x
	--LEFT JOIN CZMST_SkladLokace_Stav as LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	LEFT JOIN (select SUM(QTYSHPPD) as QTYSHPPD,ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION FROM CZMST_SkladLokace_Stav Group by ITEMNMBR, SERLTNUM, SKL_ID, EXPIRATION) LOK ON LOK.ITEMNMBR = x.ITEMNMBR AND LOK.SERLTNUM = x.SERLTNUM
	--where x.SKL_ID = @SKL_ID AND x.QTYSHPPD > 0
	where x.SKL_ID = @SKL_ID 
	AND ISNULL(x.SERLTNUM,'') = ISNULL(LOK.SERLTNUM,'')
	group by
	  x.ITEMNMBR,
	  x.SKL_ID,
	  x.SERLTNUM,
	  x.EXPIRATION
	, LOK.ITEMNMBR
	, LOK.SKL_ID
	, LOK.SERLTNUM
	, LOK.EXPIRATION
	having (SUM(x.QTYSHPPD) - SUM(LOK.QTYSHPPD)) = 0
	AND SUM(x.QTYSHPPD) = 0
	AND SUM(LOK.QTYSHPPD) = 0

	END

----------------------------------------
----------------------------------------
----------------------------------------



return 
END

GO

/**************************************************************************************/