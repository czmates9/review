/****** Object:  StoredProcedure [dbo].[CZMST_Verify_Operation]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 22.4.2020
-- Description:	Autentifikace operace, přeneseno z colorprofi
-- =============================================
CREATE PROCEDURE [dbo].[CZMST_Verify_Operation] 
	-- Add the parameters for the stored procedure here
	@operation nvarchar(20),	-- operation to verify
	@pwdhash nvarchar(50),		-- pwd hash
	@verified bit OUTPUT		-- 0 = not verified, 1=verified
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	set @verified = 0

	IF EXISTS(SELECT TOP 1 1 FROM CZMST_TASK_VERIFY WHERE taskname=@operation and verify_pwd=@pwdhash) BEGIN
		set @verified = 1
		return
	end

	Return 0
END

GO
/**************************************************************************************/