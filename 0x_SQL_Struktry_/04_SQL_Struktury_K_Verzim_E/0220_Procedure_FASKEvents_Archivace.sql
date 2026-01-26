/****** Object:  StoredProcedure [dbo].[FASKEvents_Archivace]    Script Date: 28.03.2022 14:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matous Rathouzsky
-- Create date: 24.1.2022
-- Description:	Vraci pocet archivovanych zaznamu se statusem 0
-- Info:		
-- =============================================
create procedure [dbo].[FASKEvents_Archivace]
(
	-- Add the parameters for the procedure here
	--@cisloLinky int,
	@faskID uniqueidentifier
)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @pocet int
	
    declare @description_1 nvarchar(30)
    declare @description_2 nvarchar(30)
	declare @description_3 nvarchar(30)
	declare @dateevepro nvarchar(20);
    declare @ido_pro nchar(10);
	--declare @description_4 nvarchar(30)
	    
	Set @description_1 = 'automaticke ulozeni paleta'
	Set @description_2 = 'posledni paleta 1'
	Set @description_3 = 'posledni paleta 3'
	set @dateevepro = CONVERT(nvarchar, getdate(), 120)
    set @ido_pro = 'R:' +
				SUBSTRING(@dateevepro, 6, 2) + 
				SUBSTRING(@dateevepro, 9, 2) +
				SUBSTRING(@dateevepro, 12, 2) + 
				--SUBSTRING(@dateevepro, 15, 2) + 
				SUBSTRING(@dateevepro, 18, 2)
	--Set @description_4 = 'automaticke ulozeni paleta'

	--najdu kolik je zaznamu a ulozim do promenne pocet 
 set @pocet = (
  SELECT COUNT(status)
  FROM [FASK_Events]
  where productionGuid is not null
  --and machineid = @cisloLinky
  and faskGUID = @faskID
  and (description like @description_1 or description like @description_2 or description like @description_3)
  and (status = 0)
  )

  --zaznamy archivuji
UPDATE [FASK_Events]
   SET [status] = 900
      ,[IDO] = @ido_pro
 where productionGuid is not null
  --and machineid = @cisloLinky
   and faskGUID = @faskID
  and (description like @description_1 or description like @description_2 or description like @description_3)
  and (status = 0)
                


	-- Return the result of the function
	RETURN @pocet

END

GO

/**************************************************************************************/

