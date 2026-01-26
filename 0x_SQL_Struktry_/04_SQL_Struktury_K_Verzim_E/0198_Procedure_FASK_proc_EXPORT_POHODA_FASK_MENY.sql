/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_MENY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_MENY] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST097

INSERT INTO [dbo].[CZMST097]
           ([mena_ID]
           ,[mena_text])
SELECT 
ID as mena_ID,
LEFT(Kod,30) as mena_text
FROM StwPh_04535667_2020.dbo.sCMeny WHERE (Pouzit = 1)

END

/**************************************************************************************/