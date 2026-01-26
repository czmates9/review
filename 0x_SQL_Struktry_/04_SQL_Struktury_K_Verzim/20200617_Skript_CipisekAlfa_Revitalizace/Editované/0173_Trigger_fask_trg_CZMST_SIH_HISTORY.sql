/****** Object:  Trigger [dbo].[fask_trg_CZMST_SIH_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie vystupnich dat pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SIH_HISTORY] 
   ON  [dbo].[CZMST_SIH] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_sih_history select * from deleted
END

GO
/**************************************************************************************/