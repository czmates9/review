/****** Object:  StoredProcedure [dbo].[CZMST_get_sscc_sequence_proc]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_get_sscc_sequence_proc] 
 @sequence int  ,
 @count int , 
 @terminal int ,
 @endSSCC int OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE		
	BEGIN TRANSACTION
	BEGIN TRY
	
		DECLARE @sequence_count int;
		
		--DECLARE @LV numeric(1,0);
		--declare @GCP numeric(9,0);
		--declare @GCP_count numeric(9,0);
		-- return an error if sequence does not exist
		-- so we will know if someone truncates the table
		
		-- set @sequence_count = -1
		
		select @sequence_count = seq.sequence_count  
		from CZMST_SSCC_SEQUENCE as seq  
		where seq_id = @sequence
		
		IF @sequence_count is NULL 
		BEGIN
			set @sequence_count = 0
			insert into CZMST_SSCC_SEQUENCE (seq_id, sequence_count) values (@sequence, @sequence_count)
		END		
		
		--select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		--from CZMST_SSCC_PARAMETERS as par
		--where par.ID_SSCC = @sequence
		
		--SET @endSSCC = '00' + convert(nvarchar(1),@LV) + convert(nvarchar(9),@GCP) + convert(nvarchar(9),@sequence_count) ;
		SET @endSSCC = @sequence_count + @count
	
		UPDATE CZMST_SSCC_SEQUENCE
		SET    sequence_count = @endSSCC
		WHERE  seq_id = @sequence

		COMMIT TRANSACTION

	RETURN @endSSCC		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @endSSCC = NULL
		RETURN @endSSCC
	END CATCH
END

GO
/**************************************************************************************/