/****** Object:  UserDefinedFunction [dbo].[CZMST_get_sscc_func]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Vydej, natvrdo v kodu
-- =============================================
CREATE FUNCTION [dbo].[CZMST_get_sscc_func] 
(
	@seq_id int,
	@sscc_count int 
)
RETURNS nvarchar(20)
AS
BEGIN

		DECLARE @LV numeric(1,0);
		declare @GCP numeric(9,0);
		declare @GCP_count numeric(9,0);
				
		select @LV = par.LV, @GCP = par.GCP , @GCP_count = par.GCP_count
		from CZMST_SSCC_PARAMETERS as par
		where par.ID_SSCC = @seq_id
		
		declare @sscc nvarchar(20)
					
		set @sscc = '00'
		set @sscc = @sscc + convert(nvarchar(1),@LV)
		set @sscc = @sscc + RIGHT('000000000' + convert(nvarchar(9),@GCP), @GCP_count)
		--set @sscc = @sscc + convert(nvarchar(9),@sscc_count)
		set @sscc = @sscc + RIGHT('000000000' + CONVERT(nvarchar(20), @sscc_count), (20-1-LEN(@sscc)))
		
		set @sscc = @sscc + convert(nvarchar(1),(select CheckDigit from CalculateCheckDigitModulo10(@sscc)))
		
		RETURN @sscc
		
END

GO
/**************************************************************************************/