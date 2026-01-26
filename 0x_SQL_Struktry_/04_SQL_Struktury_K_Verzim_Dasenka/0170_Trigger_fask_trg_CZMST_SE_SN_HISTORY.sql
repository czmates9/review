/****** Object:  Trigger [dbo].[fask_trg_CZMST_SE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat predlohy seriovych cisel pro modul vydej *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_SE_SN_HISTORY] 
   ON  [dbo].[CZMST_SE_SN] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_se_sn_history select * from deleted
END

GO
/**************************************************************************************/