/****** Object:  Trigger [dbo].[fask_trg_CZMST_PI_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie prijmu pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PI_HISTORY] 
   ON  [dbo].[CZMST_PI] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pi_history select * from deleted

END

GO
/**************************************************************************************/