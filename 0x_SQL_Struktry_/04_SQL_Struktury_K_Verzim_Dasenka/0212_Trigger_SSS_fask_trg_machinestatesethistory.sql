/****** Object:  Table [dbo].[fask_trg_machinestatesethistory]    ******/

-- =============================================
-- Author:		Ing. Jiří Skřivánek
-- Create date: 20.5.2014
-- Description:	Trigger pro ukládání historie stavu strojů
-- =============================================
CREATE TRIGGER [dbo].[fask_trg_machinestatesethistory] 
   ON  [dbo].[MachineStateSet] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--inserted
	IF EXISTS(SELECT * FROM inserted)
	BEGIN
		INSERT INTO MachineStateSetHistory 
			SELECT * FROM inserted
	END        

END

GO

/**************************************************************************************/