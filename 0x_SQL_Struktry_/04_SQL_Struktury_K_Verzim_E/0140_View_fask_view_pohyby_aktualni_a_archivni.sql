/****** Object:  View [dbo].[fask_view_pohyby_aktualni_a_archivni]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_pohyby_aktualni_a_archivni]
AS
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_aktualni
UNION
SELECT        [Type], [DOCUMENT_NUMBER], ITEMDESC, ITEMNMBR, QTYSHPPD, SKL_ID, LOCNCODE, SERLTNUM, CountEntries, CZ_CarKod, USER_ID, ID_TERMINAL, dateeve, [ITEMCODE], [VNDITNUM], [VNDDOCNM], [WEIGHT], [GUID]
FROM            fask_view_pohyby_archivni
GO
/**************************************************************************************/