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
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO,
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO,
	vpp.BarcodeT,
	vpp.CZ_REZ1_Track,
	vpp.CZ_REZ2_Track,
        vpp.CZ_REZ3_Track,
	vpp.CZ_REZ4_Track,
	vpp.CZ_REZ5_Track,
	vpp.WEIGHT_TARA,
	vpp.WEIGHT_NETTO,
	vpp.WEIGHT_TOL_PLUS,
	vpp.WEIGHT_TOL_MINUS
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO , MAX(dateeve) as LSTMod, BarcodeP
	FROM   dbo.Production
	WHERE  dateeve > (Select TOP 1 LSTMod from CZPRO_VPP as vpp2 where dbo.Production.CountEntries = vpp2.CountEntries and dbo.Production.SOPNUMBE = vpp2.SOPNUMBE AND dbo.Production.ITEMNMBR = vpp2.ITEMNMBR and dbo.Production.BarcodeP = vpp2.BarcodeP)
	GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP
) AS psum ON psum.CountEntries = vpp.CountEntries and psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR AND psum.BarcodeP = vpp.BarcodeP 
LEFT JOIN CZPRO_VPH AS vph ON vph.CountEntries=vpp.CountEntries and vph.SOPNUMBE=vpp.SOPNUMBE
WHERE ISNULL(vph.Active,1)=1

GO
/**************************************************************************************/