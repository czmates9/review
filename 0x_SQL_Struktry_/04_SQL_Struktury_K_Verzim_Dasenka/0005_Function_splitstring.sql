/****** Object:  UserDefinedFunction [dbo].[splitstring]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit NVARCHAR(MAX) , @delimiter CHAR(1)=',')
RETURNS
 @returnList TABLE 
 (
 [ID] int,
 [Name] [nvarchar] (500)
 )
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @ID int 
 DECLARE @pos INT
 
    SET @ID = 0;

 WHILE CHARINDEX(@delimiter, @stringToSplit) > 0
 BEGIN
 
  SELECT @pos  = CHARINDEX(@delimiter, @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @ID, @name 
  
    SET @ID = @ID + 1;

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @ID, @stringToSplit
 
 RETURN
END

GO
/**************************************************************************************/