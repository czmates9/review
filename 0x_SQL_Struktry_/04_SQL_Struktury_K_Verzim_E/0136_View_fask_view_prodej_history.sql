/****** Object:  View [dbo].[fask_view_prodej_history]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* View do prodeje */
CREATE VIEW [dbo].[fask_view_prodej_history]
AS
SELECT     
CONVERT(nvarchar(2), 'R') as [Type], 
zbozi.ITEMDESC as ITEMDESC, 
CONVERT(nvarchar(30), '') as [DOCUMENT_NUMBER], 
di.ITEMNMBR, 
di.QTYSHPPD, 
di.SKL_ID, 
di.LOCNCODE, 
di.SERLTNUM, 
di.CountEntries, 
di.CZ_CarKod, 
di.USER_ID, 
di.ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
di.ITEMCODE,
di.VNDITNUM,
di.WEIGHT as WEIGHT,
--null as WEIGHT,
di.GUID,
convert(nvarchar(21),null) as VNDDOCNM
FROM         CZMST_DI_HISTORY di
LEFT JOIN FASK_ZASOBY zbozi on zbozi.itemnmbr = di.itemnmbr
;

GO
/**************************************************************************************/