/****** Object:  View [dbo].[FASK_Inventura_I4_Compare]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[FASK_Inventura_I4_Compare]
AS
SELECT I4.CountEntries, I4.ITEMNMBR, Z.ITEMDESC, I4.ITEMCODE, I4.VNDITNUM, I4.SKL_ID, S.skl_desc, SUM(I4.QUANTITY) AS QUANTITY, I4.MJ, I4.SERLNMBR, I4.Expirace
FROM dbo.CZMST_I4 AS I4
LEFT OUTER JOIN dbo.FASK_ZASOBY AS Z ON Z.ITEMNMBR = I4.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST093 AS S ON S.skl_id = I4.SKL_ID
GROUP BY I4.CountEntries, I4.ITEMNMBR, Z.ITEMDESC, I4.ITEMCODE, I4.VNDITNUM, I4.SKL_ID, S.skl_desc, I4.SERLNMBR, I4.Expirace, I4.MJ

GO


/**************************************************************************************/