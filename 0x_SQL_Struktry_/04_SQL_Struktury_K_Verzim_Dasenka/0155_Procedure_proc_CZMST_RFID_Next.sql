/****** Object:  StoredProcedure [dbo].[proc_CZMST_RFID_Next]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
	CREATE PROCEDURE  [dbo].[proc_CZMST_RFID_Next]
	 @itemnmbr nvarchar(40),
	 @countnumbers int,
	 @minsequencenmbr int OUTPUT,
	 @maxsequencenmbr int OUTPUT
	AS
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
			DECLARE @id int;
			DECLARE @sequencenmbr int;
				
			select @id = ID
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr

			IF @id is NULL 
			BEGIN
				INSERT INTO CZMST_RFID_ITEMS (ITEMNMBR) VALUES (@itemnmbr)
			END

			select @id = ID, @sequencenmbr = SEQUENCENMBR
			from CZMST_RFID_ITEMS
			where ITEMNMBR = @itemnmbr
		
			SET @minsequencenmbr = @sequencenmbr + 1;
			SET @maxsequencenmbr = @sequencenmbr + @countnumbers;
		
			UPDATE CZMST_RFID_ITEMS
			SET    SEQUENCENMBR = @maxsequencenmbr
			WHERE  ID = @id

			-- vraci pocet vracenych cisel, melo by byt stejne jako je @countnumbers
			RETURN (@maxsequencenmbr - @minsequencenmbr + 1);
	END

GO
/**************************************************************************************/