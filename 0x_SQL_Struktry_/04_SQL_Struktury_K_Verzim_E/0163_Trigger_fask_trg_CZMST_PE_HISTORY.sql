/****** Object:  Trigger [dbo].[fask_trg_CZMST_PE_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie dat davky prijmu *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_PE_HISTORY] 
   ON  [dbo].[CZMST_PE] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	insert into czmst_pe_history select * from deleted
END

GO
/**************************************************************************************/