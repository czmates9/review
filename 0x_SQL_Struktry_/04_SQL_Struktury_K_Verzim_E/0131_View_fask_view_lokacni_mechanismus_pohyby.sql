/****** Object:  View [dbo].[fask_view_lokacni_mechanismus_pohyby]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_lokacni_mechanismus_pohyby]
AS
SELECT 
pohyb.POHYB_TYPE AS Type, 
zbozi.ITEMDESC, 
pohyb.DOCUMENT_NUMBER, 
pohyb.ITEMNMBR, 
pohyb.QTYSHPPD, 
pohyb.SKL_ID_SRC AS SKL_ID, 
pohyb.LOCNCODE_SRC AS LOCNCODE, 
pohyb.SERLTNUM, 
pohyb.CountEntries, 
zbozi.CZ_CarKod AS CZ_CarKod, 
pohyb.UserID AS USER_ID, 
pohyb.TermID AS ID_TERMINAL, 
pohyb.dateeveT AS dateeve, 
zbozi.ITEMCODE, 
zbozi.VNDITNUM, 
zbozi.WEIGHT, 
pohyb.guid, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_StavPohyb AS pohyb LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = pohyb.ITEMNMBR
GO
/**************************************************************************************/