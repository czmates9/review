/****** Object:  View [dbo].[FASK_Inventura_I123_Compare]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[FASK_Inventura_I123_Compare]
AS
SELECT        I1.CountEntries, I1.ITEMNMBR, Z.ITEMDESC, I1.ITEMCODE, I3.VNDITNUM, I1.SKL_ID, S.skl_desc, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN SUM(I1.QUANTITY) 
	WHEN I1.CZ_SerNum_Track = 1 THEN SUM(I2.QTY) 
	WHEN I1.CZ_SerNum_Track = 2 THEN SUM(I2.QTY) 
END AS QUANTITY, 
I3.MJ, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN NULL 
	WHEN I1.CZ_SerNum_Track = 1 THEN I2.SERLNMBR 
	WHEN I1.CZ_SerNum_Track = 2 THEN I2.SERLNMBR 
END AS SERLNMBR, 
CASE 
	WHEN I1.CZ_SerNum_Track = 0 THEN NULL
	WHEN I1.CZ_SerNum_Track = 1 THEN I2.Expirace 
	WHEN I1.CZ_SerNum_Track = 2 THEN I2.Expirace 
END AS Expirace
FROM dbo.CZMST_I1 AS I1 
LEFT OUTER JOIN dbo.CZMST_I3 AS I3 ON I3.CountEntries = I1.CountEntries AND I3.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST_I2 AS I2 ON I2.CountEntries = I1.CountEntries AND I2.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.FASK_ZASOBY AS Z ON Z.ITEMNMBR = I1.ITEMNMBR 
LEFT OUTER JOIN dbo.CZMST093 AS S ON S.skl_id = I1.SKL_ID
GROUP BY I1.CountEntries, I1.CZ_SerNum_Track, I1.ITEMNMBR, Z.ITEMDESC, I1.ITEMCODE, I3.VNDITNUM, I1.SKL_ID, S.skl_desc, I2.SERLNMBR, I2.Expirace, I3.MJ

GO


/**************************************************************************************/