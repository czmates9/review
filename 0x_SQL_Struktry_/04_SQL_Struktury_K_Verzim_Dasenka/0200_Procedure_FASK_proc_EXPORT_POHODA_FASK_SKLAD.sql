/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_POHODA_FASK_SKLAD]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_POHODA_FASK_SKLADY] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST093

INSERT INTO [dbo].[CZMST093]
           ([skl_id]
           ,[skl_desc]
           ,[skl_typ]
           ,[skl_carcode])
		select 
		LEFT(ID, 20) as skl_id,
		LEFT(COALESCE(IDS,SText, ''), 40) as skl_desc,
		'' as skl_typ,
		LEFT(COALESCE(IDS,''), 21) as skl_carcode
		from StwPh_04535667_2020.dbo.sSklad

END

/**************************************************************************************/