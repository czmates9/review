/****** Object:  StoredProcedure [dbo].[FASK_proc_EXPORT_SQL_FASK_ZASOBY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ing. Rathouzský Matouš
-- Create date: 4.4.2023
-- Description:	Procedura pro naplneni číselník pracovníků
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_EXPORT_SQL_FASK_PRACOVNICI] 
	-- Add the parameters for the stored procedure here

 @ExportPracTypFilter nvarchar(3)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE  FROM CZMST096


INSERT INTO [CZMST096]
           ([prac_id],
	    [prac_desc],
	    [prac_typ],
	    [prac_carcode])
		SELECT *
                 FROM XXX

SELECT Count(*) from CZMST096

END

/**************************************************************************************/
