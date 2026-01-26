/****** Object:  StoredProcedure [dbo].[FASK_GeneratorLokaci]    Script Date: 02.03.2020 10:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[FASK_GeneratorLokaci]
	-- Add the parameters for the stored procedure here
	@SKL_ID nvarchar(20), 
	@TYPE nvarchar(2),
	@OD int ,
	@Pocet int,
	@N int ,
	@prefix nvarchar(50) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO CZMST_SkladLokace_Mapa
([SKL_ID]
,[LOCNCODE]
,[TYPE]
,[Barcode]
,[Description])
SELECT
@SKL_ID as [SKL_ID], 
[Value] as [LOCNCODE],
@TYPE as [TYPE],
[Value] as [Barcode],
[Value] as [Description]
 FROM [dbo].[FASK_GeneratorRad] ( @OD ,@Pocet ,@N ,@prefix)

END
