/****** Object:  View [dbo].[fask_view_pohyby_aktualni]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_pohyby_aktualni]
AS
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_vydej
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prodej
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_lokacni_mechanismus_pohyby
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_stav_naplneni_inventury
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], 
                         [GUID]
FROM            fask_view_prijem
GO
/**************************************************************************************/