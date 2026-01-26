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
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, --Pocet kusu odvedenych automaticky terminaly a pripadnym rucnim odvodem ze systemu
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO	--Slouzi k informaci, zda zapocitat pripravny cas do dalsiho vystupu(>0 => nezapocitat pripravny cas)
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO , MAX(dateeve) as LSTMod
	FROM   dbo.Production
	WHERE  dateeve > (Select TOP 1 LSTMod from CZPRO_VPP as vpp2 where dbo.Production.CountEntries = vpp2.CountEntries and dbo.Production.SOPNUMBE = vpp2.SOPNUMBE AND dbo.Production.ITEMNMBR = vpp2.ITEMNMBR)
	GROUP BY CountEntries, SOPNUMBE, ITEMNMBR
) AS psum ON psum.CountEntries = vpp.CountEntries and psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR
LEFT JOIN CZPRO_VPH AS vph ON vph.CountEntries=vpp.CountEntries and vph.SOPNUMBE=vpp.SOPNUMBE
WHERE ISNULL(vph.Active,1)=1

GO
/**************************************************************************************/