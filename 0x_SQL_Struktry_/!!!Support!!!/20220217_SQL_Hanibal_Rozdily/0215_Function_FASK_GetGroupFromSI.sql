/****** Object:  UserDefinedFunction [dbo].[FASK_GetGroupFromSI]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date:		02.11.2021 
-- Description:		Vrací data pro import do pohody
-- =============================================
CREATE FUNCTION [dbo].[FASK_GetGroupFromSI] 
(
	-- Add the parameters for the function here
		@CountEntries nvarchar(25) 
)
RETURNS
 @returnList TABLE 
 (
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[USER_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[Expirace] [datetime] NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL
 )
AS
BEGIN


	DECLARE @RelSKzVC int
	DECLARE @VPrFXTS int
	DECLARE @VPrFVTS int
	DECLARE @CNT int

DECLARE @TMP_Table TABLE
(
 RelSKzVC int, 
 VPrFXTS int, 
 VPrFVTS int
);


INSERT INTO @TMP_Table
(
RelSKzVC,
VPrFXTS,
VPrFVTS
)
select S.RelSKzVC, S.VPrFXTS, S.VPrFVTS from 
(
select ITEMNMBR from CZMST_SI
where CountEntries = @CountEntries
group by ITEMNMBR
) as x 
left join StwPh_04535667_2020.dbo.SKz as S ON S.ID = X.ITEMNMBR
group by S.RelSKzVC, S.VPrFXTS, S.VPrFVTS

select 
@RelSKzVC = RelSKzVC,
@VPrFXTS = VPrFXTS,
@VPrFVTS = VPrFVTS,
@CNT = count(*) 
from @TMP_Table
group by RelSKzVC, VPrFXTS, VPrFVTS

IF @CNT = 1
	BEGIN
	--mame jeden radek


		IF @RelSKzVC > 0
			BEGIN
			 INSERT INTO @returnList 
			 (
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM]
			 )
			 SELECT
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM] 
				FROM CZMST_SI_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM

			END
		ELSE
			BEGIN
			 INSERT INTO @returnList 
			 (
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM]
			 )
			 SELECT
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				null as [Expirace],
				'' as [SERLTNUM] 
				FROM CZMST_SI_NO_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
			END
	END
ELSE
	BEGIN
	-- mame vico radku
	
	declare @cntVicRadku int

	select @cntVicRadku = count(*) from @TMP_Table where RelSKzVC > 0 group by RelSKzVC

	IF @cntVicRadku > 0
		BEGIN
			 INSERT INTO @returnList 
			 (
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM]
			 )
			 SELECT
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM] 
				FROM CZMST_SI_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
		END
	ELSE
		BEGIN
			 INSERT INTO @returnList 
			 (
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				[Expirace],
				[SERLTNUM]
			 )
			 SELECT
				[CountEntries],
				[SOPNUMBE],
				[ITEMNMBR],
				[ORD],
				[VNDITNUM] ,
				[CZ_CarKod],
				[SKL_ID] ,
				[QTYSHPPD],
				[QTYPACK] ,
				[USER_ID] ,
				[DEX_ROW_ID],
				[ID_TERMINAL],
				[MJ],
				[INPUT_MODE],
				[GUID],
				[VNDDOCNM],
				null as [Expirace],
				'' as [SERLTNUM] 
				FROM CZMST_SI_NO_GroupBy WHERE CountEntries = @CountEntries -- Nativni sledovani v IS POHODA, tak grupuju aj s SERLTNUM
		END
	END

return 
END

/**************************************************************************************/