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
 
--DECLARE @CNT_Nalezene int
--DECLARE @Skladem numeric(19,5)
--DECLARE @Vytvoreno datetime

--DECLARE @Zaplacena bit


--SELECT @Zaplacena = O.VPrZapl FROM
--StwPh_04535667_2020.dbo.OBJ as O 
--left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
--where 1 = 1
--AND pol.RefSKz = @ITEMNMBR
--AND pol.ID = @ORD

--IF @Zaplacena = 0
--	BEGIN
--		insert into @Params ([Flag]) SELECT 0 as Flag
--		return
--	END


--SELECT @Vytvoreno = o.DatCreate FROM
--StwPh_04535667_2020.dbo.OBJ as O 
--left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
--where 1 = 1
--AND pol.ID = @ORD

--SELECT @CNT_Nalezene = count(*) FROM
--StwPh_04535667_2020.dbo.OBJ as O 
--left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
--left join StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = pol.RefSKz 
--where 1 = 1
--AND pol.RefSKz = @ITEMNMBR
--AND O.Vyrizeno = 0
--AND O.VPrZapl = 0
--AND pol.ID != @ORD
--AND o.DatCreate < @Vytvoreno

--SELECT @Skladem = StavZ FROM StwPh_04535667_2020.dbo.SKz
--WHERE ID = @ITEMNMBR

--IF @CNT_Nalezene > 0
--	BEGIN

--	declare @MN numeric(19,5)

--		SELECT @MN = SUM(pol.Mnozstvi - pol.Dodano) FROM
--		StwPh_04535667_2020.dbo.OBJ as O 
--		left join StwPh_04535667_2020.dbo.OBJpol as pol ON O.ID = pol.RefAg
--		left join StwPh_04535667_2020.dbo.SKz as zas ON zas.ID = pol.RefSKz 
--		where 1 = 1
--		AND pol.RefSKz = @ITEMNMBR
--		AND O.Vyrizeno = 0
--		AND O.VPrZapl = 0
--		AND pol.ID != @ORD
--		AND o.DatCreate < @Vytvoreno
--		group by pol.ID

--		IF (@QTY + @MN) <= @Skladem
--			BEGIN
--				insert into @Params ([Flag]) SELECT 1 as Flag
--			END
--		ELSE
--			BEGIN
--				insert into @Params ([Flag]) SELECT 0 as Flag
--			END

--	END
--ELSE 
--	BEGIN
--		insert into @Params ([Flag]) SELECT 1 as Flag
--	END

 
 return


 
 END
 
GO


/**************************************************************************************/