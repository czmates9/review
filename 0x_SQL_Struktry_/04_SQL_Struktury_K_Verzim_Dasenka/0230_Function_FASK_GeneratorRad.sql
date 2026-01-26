/****** Object:  UserDefinedFunction [dbo].[FASK_GeneratorRad] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Tadeas Divacky
-- Create date: 21.2.2020
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[FASK_GeneratorRad]
(	
	-- Add the parameters for the function here
	@OD int ,
	@Pocet int,
	@N int ,
	@prefix nvarchar(50) 
)
RETURNS @Rada TABLE
(
	[ID] int,
	[Value] [nvarchar](100) not NULL

)
AS
BEGIN

	IF @OD  < 0 OR @Pocet <= 0 OR @N <= 0
		BEGIN
			insert into @Rada ([ID],[Value])
			SELECT -1, 'ERR'
		END
	ELSE
		BEGIN
			DECLARE @cnt INT = 0;

			WHILE @cnt < @Pocet
			BEGIN
				INSERT INTO @Rada ([ID],[Value])
				SELECT @cnt as ID, (@prefix + REPLACE(STR(@OD, @N, 0), ' ', '0')) as [Value]

			   SET @cnt = @cnt + 1;
			   SET @OD = @OD + 1;
			END
		END
 	return
END

GO
/**************************************************************************************/





