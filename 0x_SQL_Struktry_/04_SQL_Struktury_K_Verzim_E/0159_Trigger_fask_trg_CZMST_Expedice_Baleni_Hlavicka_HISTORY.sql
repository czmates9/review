/****** Object:  Trigger [dbo].[fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie hlavicek expedice pri mazani dat *****/
CREATE TRIGGER [dbo].[fask_trg_CZMST_Expedice_Baleni_Hlavicka_HISTORY] 
   ON  [dbo].[CZMST_Expedice_Baleni_Hlavicka] 
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	insert into CZMST_Expedice_Baleni_Hlavicka_HISTORY select * from deleted
END

GO
/**************************************************************************************/