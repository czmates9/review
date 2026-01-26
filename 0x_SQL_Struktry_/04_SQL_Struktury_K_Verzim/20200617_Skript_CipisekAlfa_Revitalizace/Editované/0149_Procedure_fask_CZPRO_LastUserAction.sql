/****** Object:  StoredProcedure [dbo].[fask_CZPRO_LastUserAction]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 7.7.2014
-- Description:	Last user production
-- =============================================
CREATE PROCEDURE [dbo].[fask_CZPRO_LastUserAction] 
	-- Add the parameters for the stored procedure here
	@loginid nvarchar(20), 
	@machineid nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 * from Production
	where loginid=@loginid and machineid=@machineid
	order by dateeve desc
END

GO
/**************************************************************************************/