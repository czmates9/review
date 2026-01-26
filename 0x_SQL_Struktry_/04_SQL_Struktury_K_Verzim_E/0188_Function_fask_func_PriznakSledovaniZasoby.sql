/****** Object:  UserDefinedFunction [dbo].[fask_func_PriznakSledovaniZasoby]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 16.12.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[fask_func_PriznakSledovaniZasoby] 
(
	-- Add the parameters for the function here
	@RelSKzVC int = null,
	@ID int,
	@EvidenceSarzi bit,
	@EvidenceVyrobnichCisel bit,
	@PohodaE1 bit
)
RETURNS int
AS
BEGIN
	
	DECLARE @VPrFXTS int ;
	DECLARE @VPrFDTS int ;
	DECLARE @RefVPrFXTS int ;
	DECLARE @RefVPrFDTS int ;

	DECLARE @VPrCZSerNumTrIS int ;

	DECLARE @Result int;
	SET @Result = 0;

	SET @Result = ISNULL(@RelSKzVC, 0);
	
	IF @Result = 0
		BEGIN

			IF @PohodaE1 = 1
				BEGIN
					SELECT distinct 
					@VPrFXTS = VPrFXTS,
					@VPrFDTS = VPrFDTS,
					@RefVPrFXTS = RefVPrFXTS,
					@RefVPrFDTS = RefVPrFDTS
					 FROM StwPh_04535667_2020.dbo.SKz where ID = @ID

					IF ISNULL(@VPrFXTS, 0) = 1
						BEGIN
							SET @Result = ISNULL(@RefVPrFXTS,1) - 1;

								IF @Result = 1
									BEGIN
										IF @EvidenceVyrobnichCisel = 0
											BEGIN
												SET @Result = 0;
											END
									END
								IF @Result = 2
									BEGIN
										IF @EvidenceSarzi = 0
											BEGIN
												SET @Result = 0;
											END
									END
						END
					ELSE
						BEGIN
							IF ISNULL(@VPrFDTS, 0) = 1
								BEGIN
									SET @Result = ISNULL(@RefVPrFDTS,1) - 1;

										IF @Result = 1
											BEGIN
												IF @EvidenceVyrobnichCisel = 0
													BEGIN
														SET @Result = 0;
													END
											END
										IF @Result = 2
											BEGIN
												IF @EvidenceSarzi = 0
													BEGIN
														SET @Result = 0;
													END
											END
								END
						END
				END
		END
	ELSE IF @Result = 1
	  BEGIN
		IF @PohodaE1 = 1
			BEGIN
			
				SELECT distinct 
				@VPrCZSerNumTrIS = VPrCZSerNumTrIS
				FROM StwPh_04535667_2020.dbo.SKz where ID = @ID

				IF ISNULL(@VPrCZSerNumTrIS, 0) = 1
					BEGIN
						SET @Result = 10;
					END
			END
	  END


	RETURN @Result

END
GO
/**************************************************************************************/