/****** Object:  View [dbo].[fask_view_vydej_history]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* View do vydeje */
CREATE VIEW [dbo].[fask_view_vydej_history]
AS
SELECT     
CONVERT(nvarchar(2), 'VP') as [Type], 
se.ITEMDESC as ITEMDESC, 
si.SOPNUMBE as [DOCUMENT_NUMBER], 
si.ITEMNMBR, 
-si.QTYSHPPD as QTYSHPPD, 
si.SKL_ID, 
si.LOCNCODE, 
si.SERLTNUM, 
si.CountEntries, 
si.CZ_CarKod, 
USER_ID, ID_TERMINAL, 
dbo.fask_func_convert_to_datetime(TIMEDONE, DATEDONE) as dateeve,
si.ITEMCODE,
si.VNDITNUM, 
si.WEIGHT,
si.GUID,
si.VNDDOCNM
FROM         dbo.CZMST_SI_history si
left join CZMST_SE_history se on se.ITEMNMBR = si.itemnmbr and se.ORD = si.ORD and se.CountEntries = si.CountEntries
;
GO
/**************************************************************************************/