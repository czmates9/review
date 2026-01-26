/****** Object:  View [dbo].[CZPRO_VPH_View]    *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CZPRO_VPH_View]
AS
SELECT
	   vph.[CountEntries]
      ,vph.[SOPNUMBE]
      ,vph.[SOPTYPE]
      ,vph.[SOPDESC]
      ,vph.[VNDDOCNMH]
      ,vph.[BarcodeH]
      ,vph.[LOCNCODE]
      ,vph.[DateProd]
      ,vph.[Rez1]
      ,vph.[Rez2]
      ,vph.[TermID]
      ,vph.[LSTMod]
      ,vph.[DEX_ROW_ID]
FROM         
	CZPRO_VPH vph
WHERE ISNULL(vph.Active,1)=1

GO
/**************************************************************************************/