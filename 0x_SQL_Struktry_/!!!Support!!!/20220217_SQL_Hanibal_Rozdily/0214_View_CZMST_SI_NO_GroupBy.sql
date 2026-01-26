/****** Object:  View [dbo].[CZMST_SI_NO_GroupBy]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CZMST_SI_NO_GroupBy]
AS
SELECT        CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, 0 AS INPUT_MODE, 
                         CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, VNDDOCNM
FROM            dbo.CZMST_SI
GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM
GO


/**************************************************************************************/