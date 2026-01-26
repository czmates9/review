/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Polozky_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie polozek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Polozky_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Polozky] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Polozky_HISTORY select * from deleted
END

GO
/**************************************************************************************/