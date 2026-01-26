/****** Object:  View [dbo].[fask_view_stav_naplneni_inventury]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_stav_naplneni_inventury]
AS
SELECT
CONVERT(nvarchar(2), 'I') AS Type, 
zbozi.ITEMDESC, 
CONVERT(nvarchar(30), '') AS DOCUMENT_NUMBER, 
stav.ITEMNMBR, 
stav.QTYSHPPD_DEF AS QTYSHPPD, 
stav.SKL_ID, 
stav.LOCNCODE, 
stav.SERLTNUM, 
NULL AS CountEntries, 
i4.CZ_CarKod, 
i4.USERID AS USER_ID, 
i4.ID_TERMINAL, 
stav.QTYSHPPD_DEF_DATE AS dateeve, 
zbozi.ITEMCODE, 
i4.VNDITNUM, 
i4.WEIGHT, 
i4.GUID, 
CONVERT(nvarchar(21), NULL) AS VNDDOCNM
FROM dbo.CZMST_SkladLokace_Stav AS stav LEFT OUTER JOIN
dbo.FASK_ZASOBY AS zbozi ON zbozi.ITEMNMBR = stav.ITEMNMBR LEFT OUTER JOIN
dbo.CZMST_I4 AS i4 ON stav.ITEMNMBR = i4.ITEMNMBR AND stav.LOCNCODE = i4.LOCNCODE
GO
/**************************************************************************************/