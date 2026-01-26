/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_STREDISKA]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_STREDISKA] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST091

INSERT INTO [dbo].[CZMST091]
           ([str_id]
           ,[str_desc]
           ,[str_typ]
           ,[str_carcode])
		   select 
LEFT(ID,30) as str_id,
LEFT(COALESCE(sText,''),40) as str_desc,
LEFT(COALESCE(IDS, ''), 3) as str_typ,
LEFT(ID,30) as str_carcode
from StwPh_04535667_2020.dbo.sSTR

END

/**************************************************************************************/