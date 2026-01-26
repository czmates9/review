/****** Object:  View [dbo].[fask_view_prijem_history]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[fask_view_prijem_history]
AS
SELECT
CONVERT(nvarchar(2), 'PP') AS Type, 
pe.ITEMDESC, 
pi.PONUMBER AS DOCUMENT_NUMBER, 
pi.ITEMNMBR, 
pi.QTYSHPPD, 
pi.SKL_ID, 
pi.LOCNCODE, 
pi.SERLTNUM, 
pi.CountEntries, 
pi.CZ_CarKod, 
pi.USER_ID, 
pi.ID_TERMINAL,
dbo.fask_func_convert_to_datetime(pi.TIMEDONE, pi.DATEDONE) AS dateeve, 
pi.ITEMCODE, 
pi.VNDITNUM, 
pi.WEIGHT, 
pi.GUID, 
pi.VNDDOCNM
FROM
dbo.CZMST_PI_HISTORY AS pi LEFT OUTER JOIN
dbo.CZMST_PE_HISTORY AS pe ON pe.ITEMNMBR = pi.ITEMNMBR AND pe.ORD = pi.ORD AND pe.CountEntries = pi.CountEntries
GO
/**************************************************************************************/