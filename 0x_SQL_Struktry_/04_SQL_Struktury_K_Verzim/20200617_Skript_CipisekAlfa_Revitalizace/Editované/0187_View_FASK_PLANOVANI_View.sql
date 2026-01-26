/****** Object:  View [dbo].[FASK_PLANOVANI_View]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[FASK_PLANOVANI_View]
AS
SELECT        N.[DESC] AS ColumnName_Lokalizace, P.Value, P.GUID_PLANOVANI, P.ColumnName AS ColumnName_Original
FROM            dbo.FASK_PLANOVANI_PARAMS AS P LEFT OUTER JOIN
                         dbo.FASK_PLANOVANI_PARAMS_Name AS N ON N.Column_Name = P.ColumnName
GO

/**************************************************************************************/

