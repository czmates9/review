/****** Object:  StoredProcedure [dbo].[CZMST_Next_CountEntries]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 18.4.2016
-- Description:	Generuje nasledujici cislo davky pro modul transakcne
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Next_CountEntries]
 @module nvarchar(20),
 @CountEntries int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
		DECLARE @ce int;
				
		select @ce = CountEntries
		from CZMST_CountEntries
		where TBL = @module

		IF @ce is NULL 
		BEGIN
			SET @CountEntries = 1
			INSERT INTO CZMST_CountEntries (TBL, CountEntries) VALUES (@module, @CountEntries)
		END
		ELSE
		BEGIN
			SET @CountEntries = @ce + 1
			UPDATE CZMST_CountEntries
			SET    CountEntries = @CountEntries
			WHERE  TBL = @module
		END
END

GO
/**************************************************************************************/